
using TrainMaster.Domain.Common;
using TrainMaster.Domain.Enums;


namespace TrainMaster.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string Password { get; private set; } = string.Empty;
    public DateTime Birthday { get; private set; }
    public UserGoal  Goal { get; private set; }

    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiresAt { get; private set; }


    public static User Create(string name, string email, string password, DateTime birthday, UserGoal goal)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        if (birthday == default)
            throw new ArgumentException("Birthday is required");

        if (birthday > DateTime.UtcNow)
            throw new ArgumentException("Birthday cannot be in the future");

        return new User
        {
            Name = name,
            Email = email,
            Password = password,
            Birthday = birthday,
            Goal = goal
        };
    }

    public void SetRefreshToken(string refreshTokenHash, DateTime expiresAt)
    {
        RefreshToken = refreshTokenHash;
        RefreshTokenExpiresAt = expiresAt;
        SetUpdatedAt();
    }

    public void RevokeRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiresAt = null;
        SetUpdatedAt();
    }

    public bool HasValidRefreshToken() =>
        RefreshToken is not null &&
        RefreshTokenExpiresAt.HasValue &&
        RefreshTokenExpiresAt.Value > DateTime.UtcNow;
}