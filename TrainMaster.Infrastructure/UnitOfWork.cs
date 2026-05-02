using TrainMaster.Domain.Interfaces;
using TrainMaster.Infrastructure.Data;
using TrainMaster.Infrastructure.Repositories;

namespace TrainMaster.Infrastructure;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IUserRepository? _users;


    public IUserRepository Users =>
        _users ??= new UserRepository(context);

    public async Task<int> CommitAsync(CancellationToken ct = default) =>
        await context.SaveChangesAsync(ct);

    public void Dispose() => context.Dispose();
}
