using TrainMaster.Application.Common;
using TrainMaster.Application.DTOs;

namespace TrainMaster.Application.Interfaces;

public interface ITrainingService
{
    Task<ServiceResult<IEnumerable<TrainingResponse>>> GetAllAsync(CancellationToken ct = default);
    Task<ServiceResult<TrainingResponse>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ServiceResult<TrainingResponse>> CreateAsync(CreateTrainingResponse request, CancellationToken ct = default);
    Task<ServiceResult> UpdateAsync(Guid id, UpdateTrainingResponse request, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default);
}