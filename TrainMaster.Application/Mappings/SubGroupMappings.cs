using TrainMaster.Application.DTOs;
using TrainMaster.Domain.Entities;

namespace TrainMaster.Application.Mappings;

public static class SubGroupMappings
{
    public static SubGroupResponse ToResponse(this SubGroup subGroup) =>
        new(
            subGroup.Id,
            subGroup.Name,
            subGroup.CreatedAt,
            subGroup.UpdatedAt
        );

    public static SubGroupUniqueResponse ToUniqueResponse(this SubGroup subGroup) =>
        new(
            subGroup.Id,
            subGroup.Name,
            subGroup.Muscle.Name,
            subGroup.CreatedAt,
            subGroup.UpdatedAt
        );

    public static IEnumerable<SubGroupResponse> ToResponse(this IEnumerable<SubGroup> subGroup) =>
        subGroup.Select(s => s.ToResponse());
}