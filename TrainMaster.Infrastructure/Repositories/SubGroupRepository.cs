using TrainMaster.Domain.Entities;
using TrainMaster.Domain.Interfaces;
using TrainMaster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TrainMaster.Infrastructure.Repositories;

public class SubGroupRepository(AppDbContext context)
    : Repository<SubGroup>(context), ISubGroupRepository
{
    public async Task<SubGroup?> GetByIdWithMuscleAsync(Guid id, CancellationToken ct = default)  =>
    await _dbSet
        .AsNoTracking()
        .Include(s => s.Muscle)
        .FirstOrDefaultAsync(s => s.Id == id);
}
