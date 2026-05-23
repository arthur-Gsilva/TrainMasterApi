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

public record TrainingWorkoutResponse(
    Guid Id,
    Guid TrainingId,
    Guid WorkouId,
    WorkoutResponse Workout,
    int Order,
    int Series,
    int Reps,
    int Weight
);

public record CreateTrainingWorkout(
    Guid TrainingId,
    Guid WorkoutId,
    int Order,
    int Series,
    int Reps,
    int Weight
);

public record UpdateTrainingWorkout(
    int Order,
    int Series,
    int Reps,
    int Weight,
    Guid WorkoutId
);

public record TrainingSessionResponse(
    Guid Id,
    Guid UserId,
    TrainingResponse Training,
    DateTime Date,
    int Duration
);

public record CreateTrainingSession(
    Guid TrainingId,
    Guid UserId,
    DateTime Date,
    int Duration
);
public record UpdateTrainingSession(
    Guid TrainingId,
    DateTime Date,
    int Duration
);