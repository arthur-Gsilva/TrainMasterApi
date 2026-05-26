using TrainMaster.Application.Common;
using TrainMaster.Application.Interfaces;
using TrainMaster.Domain.Entities;
using TrainMaster.Domain.Interfaces;
using TrainMaster.Application.DTOs;

using TrainMaster.Application.Mappings;


namespace TrainMaster.Application.Services;

public class WorkoutService(
    IUnitOfWork unitOfWork
) : IWorkoutService
{
    public async Task<ServiceResult<IEnumerable<WorkoutResponse>>> GetAllAsync(CancellationToken ct = default)
    {
        var workouts = await unitOfWork.Workouts.GetAllAsync(ct);
        var response = workouts.Select(w => w.ToResponse());
        return ServiceResult<IEnumerable<WorkoutResponse>>.Success(response);
    }

    public async Task<ServiceResult<WorkoutResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var workout = await unitOfWork.Workouts.GetByIdAsync(id, ct);
        if (workout == null)
            return ServiceResult<WorkoutResponse>.Failure("Workout not found", 404);

        var response = workout.ToResponse();
        return ServiceResult<WorkoutResponse>.Success(response);
    }

    public async Task<ServiceResult<WorkoutResponse>> CreateAsync(CreateWorkoutRequest request, CancellationToken ct = default)
    {
        if (await unitOfWork.Workouts.NameExistsAsync(request.Name, ct))
            return ServiceResult<WorkoutResponse>.Failure("A workout with the same name already exists", 400);

        var muscleExists = await unitOfWork.Muscles.ExistsAsync(m => m.Id == request.MuscleId, ct);
        if (!muscleExists)
            return ServiceResult<WorkoutResponse>.Failure("Muscle not found.", 404);

        var subGroupExists = await unitOfWork.SubGroups.ExistsAsync(s => s.Id == request.SubGroupId, ct);
        if (!subGroupExists)
            return ServiceResult<WorkoutResponse>.Failure("Subgroup not found.", 404);

        var workout = Workout.Create(request.Name, request.Description, request.MuscleId, request.SubGroupId, request.Type, request.Url_video, request.Url_image, request.Level);
        
        await unitOfWork.Workouts.AddAsync(workout, ct);
        await unitOfWork.CommitAsync(ct);

        var createdWorkout = await unitOfWork.Workouts.GetByIdAsync(workout.Id, ct);
        if (createdWorkout == null)
            return ServiceResult<WorkoutResponse>.Failure("Workout not found after create", 500);

        var response = createdWorkout.ToResponse();
        return ServiceResult<WorkoutResponse>.Success(response);
    }

    public async Task<ServiceResult<WorkoutResponse>> UpdateAsync(Guid id, UpdateWorkoutRequest request, CancellationToken ct = default)
    {
        var workout = await unitOfWork.Workouts.GetByIdAsync(id, ct);
        if (workout == null)
            return ServiceResult<WorkoutResponse>.Failure("Workout not found", 404);

        if (workout.Name != request.Name && await unitOfWork.Workouts.NameExistsAsync(request.Name, ct))
            return ServiceResult<WorkoutResponse>.Failure("A workout with the same name already exists", 400);

        workout.Update(request.Name, request.Description, request.MuscleId, request.SubGroupId, request.Type, request.Url_video, request.Url_image, request.Level);
        await unitOfWork.CommitAsync(ct);
        var response = workout.ToResponse();
        return ServiceResult<WorkoutResponse>.Success(response);
    }

    public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var workout = await unitOfWork.Workouts.GetByIdAsync(id, ct);
        if (workout == null)
            return ServiceResult.Failure("Workout not found", 404);

        unitOfWork.Workouts.Delete(workout);
        await unitOfWork.CommitAsync(ct);
        return ServiceResult.Success();
    }

}