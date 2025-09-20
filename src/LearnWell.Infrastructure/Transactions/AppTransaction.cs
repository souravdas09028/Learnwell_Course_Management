using LearnWell.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace LearnWell.Infrastructure.Transactions
{
    public class AppTransaction : IAppTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public AppTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task CommitAsync() => await _transaction.CommitAsync();
        public async Task RollbackAsync() => await _transaction.RollbackAsync();
        public void Dispose() => _transaction.Dispose();
    }
}
