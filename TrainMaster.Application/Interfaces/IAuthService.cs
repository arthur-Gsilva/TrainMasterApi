using TrainMaster.Application.Common;
using TrainMaster.Application.DTOs;

namespace TrainMaster.Application.Interfaces;

public interface IAuthService
{
    Task<ServiceResult<AuthInternalResult>> RegisterAsync(RegisterRequest request, CancellationToken ct = default);
    Task<ServiceResult<AuthInternalResult>> LoginAsync(LoginRequest request, CancellationToken ct = default);
    Task<ServiceResult<AuthInternalResult>> RefreshAsync(string expiredAccessToken, string refreshToken, CancellationToken ct = default);
    Task<ServiceResult> LogoutAsync(Guid userId, CancellationToken ct = default);
}
