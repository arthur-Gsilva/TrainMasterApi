namespace TrainMaster.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IMuscleRepository Muscles { get; }
    ISubGroupRepository SubGroups { get; }
    IWorkoutRepository Workouts { get; }
    ITrainingRepository Trainings { get; }
    ITrainingWorkoutRepository TrainingWorkouts { get; }
    ITrainingSessionRepository TrainingSessions { get; }
    Task<int> CommitAsync(CancellationToken ct = default);
}
