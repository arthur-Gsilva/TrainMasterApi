using TrainMaster.Domain.Entities;
using TrainMaster.Domain.Interfaces;
using TrainMaster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TrainMaster.Infrastructure.Repositories;

public class TrainingRepository(AppDbContext context)
    : Repository<Training>(context), ITrainingRepository {}

public class TrainingWorkoutRepository(AppDbContext context)
    : Repository<TrainingWorkout>(context), ITrainingWorkoutRepository
{
    public override async Task<IEnumerable<TrainingWorkout>> GetAllAsync(CancellationToken ct = default) =>
        await _dbSet
            .Include(tw => tw.Workout)
                .ThenInclude(w => w.Muscle)
            .Include(tw => tw.Workout)
                .ThenInclude(w => w.SubGroup)
            .AsNoTracking()
            .ToListAsync(ct);

    public override async Task<TrainingWorkout?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _dbSet
            .Include(tw => tw.Workout)
                .ThenInclude(w => w.Muscle)
            .Include(tw => tw.Workout)
                .ThenInclude(w => w.SubGroup)
            .FirstOrDefaultAsync(tw => tw.Id == id, ct);
}

public class TrainingSessionRepository(AppDbContext context)
    : Repository<TrainingSession>(context), ITrainingSessionRepository {
        public override async Task<IEnumerable<TrainingSession>> GetAllAsync(CancellationToken ct = default) =>
        await _dbSet
            .Include(TS => TS.User)
            .Include(ts => ts.Training)
            .AsNoTracking()
            .ToListAsync(ct);

    public override async Task<TrainingSession?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _dbSet
            .Include(ts => ts.User)
            .Include(ts => ts.Training)
            .FirstOrDefaultAsync(ts => ts.Id == id, ct);
    }

