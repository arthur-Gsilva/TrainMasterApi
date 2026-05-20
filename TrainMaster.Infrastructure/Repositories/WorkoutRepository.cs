using TrainMaster.Domain.Entities;
using TrainMaster.Domain.Interfaces;
using TrainMaster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TrainMaster.Infrastructure.Repositories;

public class WorkoutRepository(AppDbContext context)
    : Repository<Workout>(context), IWorkoutRepository
{
    public async Task<Workout?> GetByNameAsync(string name, CancellationToken ct = default) =>
        await _dbSet.FirstOrDefaultAsync(w => w.Name == name, ct);

    public async Task<bool> NameExistsAsync(string name, CancellationToken ct = default) =>
        await _dbSet.AnyAsync(w => w.Name == name, ct);
}