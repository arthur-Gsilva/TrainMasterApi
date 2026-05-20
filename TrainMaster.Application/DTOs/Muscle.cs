namespace TrainMaster.Application.DTOs;

public record MuscleResponse (
    Guid Id,
    string Name,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateMuscleRequest(
    string Name
);
public record UpdateMuscleRequest(
    string Name
);