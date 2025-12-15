using System.Data.Common;

namespace Core.Database;

public interface IUnitOfWork
{
    Task<DbTransaction> BeginTransaction(CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken ct = default);
}