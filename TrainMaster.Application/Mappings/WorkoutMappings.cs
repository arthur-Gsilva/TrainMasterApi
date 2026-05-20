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
            
            workout.Muscle.Name,
            workout.SubGroup.Name,
            workout.Level,
            workout.Type,
            workout.Url_video,
            workout.Url_image,
            workout.CreatedAt,
            workout.UpdatedAt
        );

    public static IEnumerable<WorkoutResponse> ToResponse(this IEnumerable<Workout> workout) =>
        workout.Select(s => s.ToResponse());
}