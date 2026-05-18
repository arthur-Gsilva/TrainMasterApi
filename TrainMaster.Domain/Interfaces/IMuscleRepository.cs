using TrainMaster.Domain.Entities;

namespace TrainMaster.Domain.Interfaces;

public interface IMuscleRepository : IRepository<Muscle>
{
    Task<Muscle?> GetSubGroupsAsync(Guid id);
}
