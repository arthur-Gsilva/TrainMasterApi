using TrainMaster.Domain.Entities;

namespace TrainMaster.Domain.Interfaces;

public interface IWorkoutRepository : IRepository<Workout>
{
    Task<Workout?> GetByNameAsync(string name, CancellationToken ct = default);
    Task<bool> NameExistsAsync(string name, CancellationToken ct = default);
}
