using TrainMaster.Application.Common;
using TrainMaster.Application.DTOs;
using TrainMaster.Application.Interfaces;
using TrainMaster.Domain.Entities;
using TrainMaster.Domain.Interfaces;
using FluentValidation;
using System.Security.Cryptography;
using System.Text;

namespace TrainMaster.Application.Services;

public class AuthService(
    IUnitOfWork unitOfWork,
    IJwtService jwtService,
    IValidator<RegisterRequest> registerValidator,
    IValidator<LoginRequest> loginValidator) : IAuthService
{
    public async Task<ServiceResult<AuthInternalResult>> RegisterAsync(RegisterRequest request, CancellationToken ct = default)
    {
        var validation = await registerValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
            return ServiceResult<AuthInternalResult>.Failure(validation.Errors[0].ErrorMessage);

        var emailExists = await unitOfWork.Users.EmailExistsAsync(request.Email, ct);
        if (emailExists)
            return ServiceResult<AuthInternalResult>.Failure("Email already in use.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = User.Create(request.Name, request.Email, passwordHash, request.Birthday, request.Goal);

        var tokens = jwtService.GenerateTokens(user);
        user.SetRefreshToken(HashToken(tokens.RefreshToken), tokens.RefreshTokenExpiresAt);

        await unitOfWork.Users.AddAsync(user, ct);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult<AuthInternalResult>.Success(BuildResult(user, tokens), 201);
    }

    public async Task<ServiceResult<AuthInternalResult>> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var validation = await loginValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
            return ServiceResult<AuthInternalResult>.Failure(validation.Errors[0].ErrorMessage);

        var user = await unitOfWork.Users.GetByEmailAsync(request.Email, ct);

        // Compara mesmo se user == null para evitar timing attacks
        var passwordValid = user is not null &&
                            BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

        if (!passwordValid)
            return ServiceResult<AuthInternalResult>.Failure("Invalid email or password.", 401);

        var tokens = jwtService.GenerateTokens(user!);
        user!.SetRefreshToken(HashToken(tokens.RefreshToken), tokens.RefreshTokenExpiresAt);

        unitOfWork.Users.Update(user);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult<AuthInternalResult>.Success(BuildResult(user, tokens));
    }

    public async Task<ServiceResult<AuthInternalResult>> RefreshAsync(
        string expiredAccessToken, string refreshToken, CancellationToken ct = default)
    {
        var userId = jwtService.GetUserIdFromExpiredToken(expiredAccessToken);
        if (userId is null)
            return ServiceResult<AuthInternalResult>.Failure("Invalid access token.", 401);

        var user = await unitOfWork.Users.GetByIdAsync(userId.Value, ct);
        if (user is null || !user.HasValidRefreshToken())
            return ServiceResult<AuthInternalResult>.Failure("Invalid or expired refresh token.", 401);

        var refreshTokenHash = HashToken(refreshToken);
        if (user.RefreshToken != refreshTokenHash)
            return ServiceResult<AuthInternalResult>.Failure("Invalid or expired refresh token.", 401);

        // Rotação: gera novo par a cada refresh — token anterior é invalidado
        var tokens = jwtService.GenerateTokens(user);
        user.SetRefreshToken(HashToken(tokens.RefreshToken), tokens.RefreshTokenExpiresAt);

        unitOfWork.Users.Update(user);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult<AuthInternalResult>.Success(BuildResult(user, tokens));
    }

    public async Task<ServiceResult> LogoutAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await unitOfWork.Users.GetByIdAsync(userId, ct);
        if (user is null)
            return ServiceResult.NotFound();

        user.RevokeRefreshToken();
        unitOfWork.Users.Update(user);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult.Success(204);
    }

    // ── Helpers ─────────────────────────────────────────────────────────────

    private static AuthInternalResult BuildResult(User user, TokenResult tokens) =>
        new(
            new AuthResponse(user.Id, user.Name, user.Email, user.Role,
                tokens.AccessToken, tokens.AccessTokenExpiresAt),
            tokens.RefreshToken,
            tokens.RefreshTokenExpiresAt
        );

    private static string HashToken(string token)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(bytes);
    }
}
