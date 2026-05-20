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