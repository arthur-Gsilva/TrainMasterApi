using TrainMaster.Application.Common;
using TrainMaster.Application.DTOs;

namespace TrainMaster.Application.Interfaces;

public interface ITrainingService
{
    Task<ServiceResult<IEnumerable<TrainingResponse>>> GetAllAsync(CancellationToken ct = default);
    Task<ServiceResult<TrainingResponse>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ServiceResult<TrainingResponse>> CreateAsync(CreateTrainingResponse request, CancellationToken ct = default);
    Task<ServiceResult<TrainingResponse>> UpdateAsync(Guid id, UpdateTrainingResponse request, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default);
}

public interface ITrainingWorkoutService
{
    Task<ServiceResult<IEnumerable<TrainingWorkoutResponse>>> GetAllAsync(CancellationToken ct = default);
    Task<ServiceResult<TrainingWorkoutResponse>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ServiceResult<TrainingWorkoutResponse>> CreateAsync(CreateTrainingWorkout request, CancellationToken ct = default);
    Task<ServiceResult<TrainingWorkoutResponse>> UpdateAsync(Guid id, UpdateTrainingWorkout request, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default);
}

public interface ITrainingSessionService
{
    Task<ServiceResult<IEnumerable<TrainingSessionResponse>>> GetAllAsync(CancellationToken ct = default);
    Task<ServiceResult<TrainingSessionResponse>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ServiceResult<TrainingSessionResponse>> CreateAsync(CreateTrainingSession request, CancellationToken ct = default);
    Task<ServiceResult<TrainingSessionResponse>> UpdateAsync(Guid id, UpdateTrainingSession request, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default);
}