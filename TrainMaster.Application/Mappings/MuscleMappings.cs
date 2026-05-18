using TrainMaster.Application.DTOs;
using TrainMaster.Domain.Entities;

namespace TrainMaster.Application.Mappings;

public static class MuscleMappings
{
    public static MuscleResponse ToResponse(this Muscle muscle) =>
        new(
            muscle.Id,
            muscle.Name,
            muscle.CreatedAt,
            muscle.UpdatedAt
        );

    public static IEnumerable<MuscleResponse> ToResponse(this IEnumerable<Muscle> muscle) =>
        muscle.Select(m => m.ToResponse());
}