namespace TrainMaster.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IMuscleRepository Muscles { get; }
    ISubGroupRepository SubGroups { get; }
    Task<int> CommitAsync(CancellationToken ct = default);
}
