using TrainMaster.Domain.Entities;

namespace TrainMaster.Application.Interfaces;

public record TokenResult(
    string AccessToken,
    DateTime AccessTokenExpiresAt,
    string RefreshToken,           // valor puro — enviado ao cliente via cookie
    string RefreshTokenHash,       // hash SHA-256 — armazenado no banco
    DateTime RefreshTokenExpiresAt
);

public interface IJwtService
{
    TokenResult GenerateTokens(User user);

    /// <summary>
    /// Retorna o UserId contido no access token SEM validar expiração.
    /// Usado no /refresh para identificar o usuário mesmo com token expirado.
    /// </summary>
    Guid? GetUserIdFromExpiredToken(string accessToken);
}
