using TrainMaster.Application.Common;
using TrainMaster.Application.Interfaces;
using TrainMaster.Domain.Entities;
using TrainMaster.Domain.Interfaces;
using TrainMaster.Application.DTOs;

using TrainMaster.Application.Mappings;


namespace TrainMaster.Application.Services;

public class TrainingService(
    IUnitOfWork unitOfWork
)
{
    public async Task<ServiceResult<IEnumerable<TrainingResponse>>> GetAllAsync(CancellationToken ct = default)
    {
        var trainings = await unitOfWork.Trainings.GetAllAsync(ct);
        var response = trainings.Select(t => t.ToResponse());
        return ServiceResult<IEnumerable<TrainingResponse>>.Success(response);
    }

    public async Task<ServiceResult<TrainingResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var training = await unitOfWork.Trainings.GetByIdAsync(id, ct);
        if (training == null)
            return ServiceResult<TrainingResponse>.Failure("Treino não encontrado", 404);

        var response = training.ToResponse();
        return ServiceResult<TrainingResponse>.Success(response);
    }

    public async Task<ServiceResult<TrainingResponse>> CreateAsync(CreateTrainingResponse request, CancellationToken ct = default)
    {
        var training = Training.Create(request.Name, request.Description, request.UserId);
        await unitOfWork.Trainings.AddAsync(training, ct);
        await unitOfWork.CommitAsync(ct);

        var response = training.ToResponse();
        return ServiceResult<TrainingResponse>.Success(response);
    }

    public async Task<ServiceResult<TrainingResponse>> UpdateAsync(Guid id, UpdateTrainingResponse request, CancellationToken ct = default)
    {
        var training = await unitOfWork.Trainings.GetByIdAsync(id, ct);
        if (training == null)
            return ServiceResult<TrainingResponse>.Failure("Treino não encontrado", 404);

        training.Update(request.Name, request.Description);
        await unitOfWork.CommitAsync(ct);
        var response = training.ToResponse();
        return ServiceResult<TrainingResponse>.Success(response);
    }
}