using TrainMaster.Application.DTOs;
using TrainMaster.Domain.Entities;

namespace TrainMaster.Application.Mappings;

public static class TrainingMappings
{
    public static TrainingResponse ToResponse(this Training training) => 
        new (
            training.Id,
            training.Name,
            training.Description,
            training.UserId,
            training.CreatedAt,
            training.UpdatedAt
        );

    public static IEnumerable<TrainingResponse> ToResponse(this IEnumerable<Training> training) =>
        training.Select(s => s.ToResponse());
}

public static class TrainingWorkoutMappings
{
    public static TrainingWorkoutResponse ToResponse(this TrainingWorkout TW) => 
        new (
            TW.Id,
            TW.TrainingId,
            TW.WorkoutId,
            TW.Workout.ToResponse(),
            TW.Order,
            TW.Series,
            TW.Reps,
            TW.Weight
        );

    public static IEnumerable<TrainingWorkoutResponse> ToResponse(this IEnumerable<TrainingWorkout> TW) =>
        TW.Select(s => s.ToResponse());
}

public static class TrainingSessionMappings
{
    public static TrainingSessionResponse ToResponse(this TrainingSession TS) =>
        new(
            TS.Id,
            TS.UserId,
            TS.Training.ToResponse(),
            TS.Date,
            TS.Duration
        );
    
    public static IEnumerable<TrainingSessionResponse> ToResponse(this IEnumerable<TrainingSession> TS) =>
        TS.Select(s => s.ToResponse());
}