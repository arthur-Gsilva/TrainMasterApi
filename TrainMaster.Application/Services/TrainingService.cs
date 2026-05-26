using TrainMaster.Application.Common;
using TrainMaster.Application.Interfaces;
using TrainMaster.Domain.Entities;
using TrainMaster.Domain.Interfaces;
using TrainMaster.Application.DTOs;

using TrainMaster.Application.Mappings;


namespace TrainMaster.Application.Services;

public class TrainingService(
    IUnitOfWork unitOfWork
) : ITrainingService
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

    public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var training = await unitOfWork.Trainings.GetByIdAsync(id, ct);
        if (training is null)
            return ServiceResult.NotFound($"Treino com com '{id}' Não encontrado.");

        unitOfWork.Trainings.Delete(training);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult.Success(204);
    }
}


public class TrainingWorkoutService(
    IUnitOfWork unitOfWork
) : ITrainingWorkoutService
{
    public async Task<ServiceResult<IEnumerable<TrainingWorkoutResponse>>> GetAllAsync(CancellationToken ct = default)
    {
        var trainings = await unitOfWork.TrainingWorkouts.GetAllAsync(ct);
        var response = trainings.Select(t => t.ToResponse());
        return ServiceResult<IEnumerable<TrainingWorkoutResponse>>.Success(response);
    }

    public async Task<ServiceResult<TrainingWorkoutResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var training = await unitOfWork.TrainingWorkouts.GetByIdAsync(id, ct);
        if (training == null)
            return ServiceResult<TrainingWorkoutResponse>.Failure("Treino não encontrado", 404);

        var response = training.ToResponse();
        return ServiceResult<TrainingWorkoutResponse>.Success(response);
    }

    public async Task<ServiceResult<TrainingWorkoutResponse>> CreateAsync(CreateTrainingWorkout request, CancellationToken ct = default)
    {
        var training = TrainingWorkout.Create(request.Order, request.Series, request.Reps, request.Weight, request.TrainingId, request.WorkoutId);
        await unitOfWork.TrainingWorkouts.AddAsync(training, ct);
        await unitOfWork.CommitAsync(ct);

        var created = await unitOfWork.TrainingWorkouts.GetByIdAsync(training.Id, ct);
        if (created == null)
            return ServiceResult<TrainingWorkoutResponse>.Failure("TrainingWorkout not found after create", 500);

        var response = created.ToResponse();
        return ServiceResult<TrainingWorkoutResponse>.Success(response);
    }

    public async Task<ServiceResult<TrainingWorkoutResponse>> UpdateAsync(Guid id, UpdateTrainingWorkout request, CancellationToken ct = default)
    {
        var training = await unitOfWork.TrainingWorkouts.GetByIdAsync(id, ct);
        if (training == null)
            return ServiceResult<TrainingWorkoutResponse>.Failure("Treino não encontrado", 404);

        training.Update(request.Order, request.Series, request.Reps, request.Weight, request.WorkoutId);
        await unitOfWork.CommitAsync(ct);
        var response = training.ToResponse();
        return ServiceResult<TrainingWorkoutResponse>.Success(response);
    }

    public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var TW = await unitOfWork.TrainingWorkouts.GetByIdAsync(id, ct);
        if (TW is null)
            return ServiceResult.NotFound($"Treino com com '{id}' Não encontrado.");

        unitOfWork.TrainingWorkouts.Delete(TW);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult.Success(204);
    }
}

public class TrainingSessionService(
    IUnitOfWork unitOfWork
) : ITrainingSessionService
{
    public async Task<ServiceResult<IEnumerable<TrainingSessionResponse>>> GetAllAsync(CancellationToken ct = default)
    {
        var trainings = await unitOfWork.TrainingSessions.GetAllAsync(ct);
        var response = trainings.Select(t => t.ToResponse());
        return ServiceResult<IEnumerable<TrainingSessionResponse>>.Success(response);
    }

    public async Task<ServiceResult<TrainingSessionResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var training = await unitOfWork.TrainingSessions.GetByIdAsync(id, ct);
        if (training == null)
            return ServiceResult<TrainingSessionResponse>.Failure("Treino não encontrado", 404);

        var response = training.ToResponse();
        return ServiceResult<TrainingSessionResponse>.Success(response);
    }

    public async Task<ServiceResult<TrainingSessionResponse>> CreateAsync(CreateTrainingSession request, CancellationToken ct = default)
    {
        var session = TrainingSession.Create(request.Date, request.Duration, request.UserId, request.TrainingId);
        await unitOfWork.TrainingSessions.AddAsync(session, ct);
        await unitOfWork.CommitAsync(ct);

        var created = await unitOfWork.TrainingSessions
            .GetByIdAsync(session.Id, ct);

        return ServiceResult<TrainingSessionResponse>
            .Success(created!.ToResponse(), 201);
    }

    public async Task<ServiceResult<TrainingSessionResponse>> UpdateAsync(Guid id, UpdateTrainingSession request, CancellationToken ct = default)
    {
        var training = await unitOfWork.TrainingSessions.GetByIdAsync(id, ct);
        if (training == null)
            return ServiceResult<TrainingSessionResponse>.Failure("Treino não encontrado", 404);

        training.Update(request.Date, request.Duration, request.TrainingId);
        await unitOfWork.CommitAsync(ct);
        var response = training.ToResponse();
        return ServiceResult<TrainingSessionResponse>.Success(response);
    }

    public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var TS = await unitOfWork.TrainingSessions.GetByIdAsync(id, ct);
        if (TS is null)
            return ServiceResult.NotFound($"Treino com com '{id}' Não encontrado.");

        unitOfWork.TrainingSessions.Delete(TS);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult.Success(204);
    }
}