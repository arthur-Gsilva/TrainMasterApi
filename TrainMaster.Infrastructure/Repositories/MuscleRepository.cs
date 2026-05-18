using TrainMaster.Domain.Entities;
using TrainMaster.Domain.Interfaces;
using TrainMaster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TrainMaster.Infrastructure.Repositories;

public class MuscleRepository(AppDbContext context)
    : Repository<Muscle>(context), IMuscleRepository
{
    public async Task<Muscle?> GetSubGroupsAsync(Guid id) =>
        await _dbSet
        .Include(m => m.SubGroups)
        .FirstOrDefaultAsync(m => m.Id == id);
}
