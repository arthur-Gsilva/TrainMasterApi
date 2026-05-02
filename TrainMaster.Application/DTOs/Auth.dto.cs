namespace TrainMaster.Application.DTOs;
using TrainMaster.Domain.Enums;


public record RegisterRequest(
    string Name,
    string Email,
    string Password,
    DateTime Birthday,
    UserGoal Goal
);

public record LoginRequest(
    string Email,
    string Password
);

// Retornado no body — só o access token (curta duração)
// O refresh token vai num HttpOnly cookie — nunca no body da resposta pública
public record AuthResponse(
    Guid UserId,
    string Name,
    string Email,
    string AccessToken,
    DateTime AccessTokenExpiresAt
);

/// <summary>
/// Resultado interno — carrega o refresh token puro para o controller
/// setar no HttpOnly cookie. Nunca é serializado para o cliente.
/// </summary>
public record AuthInternalResult(
    AuthResponse Response,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt
);
