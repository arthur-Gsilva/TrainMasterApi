using TrainMaster.Application.Common;
using TrainMaster.Application.DTOs;

namespace TrainMaster.Application.Interfaces;

public interface IMuscleService
{
    Task<ServiceResult<IEnumerable<MuscleResponse>>> GetAllAsync(CancellationToken ct = default);
    Task<ServiceResult<MuscleResponse>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ServiceResult<MuscleResponse>> CreateAsync(CreateMuscleRequest request, CancellationToken ct = default);
    Task<ServiceResult<MuscleResponse>> UpdateAsync(Guid id, UpdateMuscleRequest request, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default);
}