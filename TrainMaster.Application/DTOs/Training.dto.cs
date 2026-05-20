namespace TrainMaster.Application.DTOs;

public record TrainingResponse (
    Guid Id,
    string Name,
    string Description,
    Guid UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateTrainingResponse(
    string Name,
    string Description,
    Guid UserId
);

public record UpdateTrainingResponse(
    string Name,
    string Description
);