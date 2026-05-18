namespace TrainMaster.Application.DTOs;

public record SubGroupResponse (
    Guid Id,
    string Name,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record SubGroupUniqueResponse(
    Guid Id,
    string Name,
    string Muscle, 
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateSubGroupRequest(
    string Name,
    Guid MuscleId
);
public record UpdateSubGroupRequest(
    string Name,
    Guid MuscleId
);