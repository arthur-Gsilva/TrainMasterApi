using TrainMaster.Application.Common;
using TrainMaster.Application.DTOs;

namespace TrainMaster.Application.Interfaces;

public interface IWorkoutService
{
    Task<ServiceResult<IEnumerable<WorkoutResponse>>> GetAllAsync(CancellationToken ct = default);
    Task<ServiceResult<WorkoutResponse>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ServiceResult<WorkoutResponse>> CreateAsync(CreateWorkoutRequest request, CancellationToken ct = default);
    Task<ServiceResult<WorkoutResponse>> UpdateAsync(Guid id, UpdateWorkoutRequest request, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default);
}