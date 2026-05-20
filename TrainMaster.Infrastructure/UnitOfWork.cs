using TrainMaster.Domain.Interfaces;
using TrainMaster.Infrastructure.Data;
using TrainMaster.Infrastructure.Repositories;

namespace TrainMaster.Infrastructure;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IUserRepository? _users;
    private IMuscleRepository? _muscles;
    private ISubGroupRepository? _subGroups;
    private IWorkoutRepository? _workouts;
    private ITrainingRepository? _trainings;
    private ITrainingWorkoutRepository? _trainingWorkouts;
    private ITrainingSessionRepository? _trainingSessions;
    public IUserRepository Users =>
        _users ??= new UserRepository(context);
    public IMuscleRepository Muscles =>
        _muscles ??= new MuscleRepository(context);
    public ISubGroupRepository SubGroups =>
        _subGroups ??= new SubGroupRepository(context);
    
    public IWorkoutRepository Workouts =>
        _workouts ??= new WorkoutRepository(context);

    public ITrainingRepository Trainings =>
        _trainings ??= new TrainingRepository(context);
    public ITrainingWorkoutRepository TrainingWorkouts =>
        _trainingWorkouts ??= new TrainingWorkoutRepository(context);
    public ITrainingSessionRepository TrainingSessions =>
        _trainingSessions ??= new TrainingSessionRepository(context);

    public async Task<int> CommitAsync(CancellationToken ct = default) =>
        await context.SaveChangesAsync(ct);

    public void Dispose() => context.Dispose();
}
