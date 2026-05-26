using TrainMaster.Application.DTOs;
using TrainMaster.Domain.Entities;

namespace TrainMaster.Application.Mappings;

public static class WorkoutMappings
{
    public static WorkoutResponse ToResponse(this Workout workout) => 
        new(
            workout.Id,
            workout.Name,
            workout.Description,
            
            workout.Muscle?.Name ?? string.Empty,
            workout.SubGroup?.Name ?? string.Empty,
            workout.Level,
            workout.Type,
            workout.Url_video ?? string.Empty,
            workout.Url_image ?? string.Empty,
            workout.CreatedAt,
            workout.UpdatedAt
        );

    public static IEnumerable<WorkoutResponse> ToResponse(this IEnumerable<Workout> workout) =>
        workout.Select(s => s.ToResponse());
}