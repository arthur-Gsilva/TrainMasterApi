using TrainMaster.Domain.Entities;

namespace TrainMaster.Domain.Interfaces;

public interface ISubGroupRepository : IRepository<SubGroup>
{
    Task<SubGroup?> GetByIdWithMuscleAsync(Guid id, CancellationToken ct = default);
}
