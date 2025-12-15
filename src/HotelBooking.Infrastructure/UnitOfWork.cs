using System.Data.Common;
using Core.Database;
using HotelBooking.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace HotelBooking.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationWriteDbContext _dbContext;

    public UnitOfWork(ApplicationWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _dbContext.SaveChangesAsync(ct);
}