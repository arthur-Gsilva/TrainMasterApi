namespace TrainMaster.Application.DTOs;
using TrainMaster.Domain.Enums;

public record WorkoutResponse (
    Guid Id,
    string Name,
    string Description,
    string Muscle,
    string SubGroup,
    WorkoutLevels Level,
    WorkoutTypes Type,
    string? Url_video,
    string? Url_image,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record CreateWorkoutRequest(
    string Name,
    string Description,
    Guid MuscleId,
    Guid SubGroupId,
    WorkoutTypes Type,
    string? Url_video,
    string? Url_image,
    WorkoutLevels Level
);

public record UpdateWorkoutRequest(
    string Name,
    string Description,
    Guid MuscleId,
    Guid SubGroupId,
    WorkoutTypes Type,
    string? Url_video,
    string? Url_image,
    WorkoutLevels Level
);