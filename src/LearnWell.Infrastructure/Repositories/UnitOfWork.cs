using LearnWell.Application.Common.Interfaces;
using LearnWell.Infrastructure.Data;
using LearnWell.Infrastructure.Transactions;

namespace LearnWell.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LearnWellDbContext _db;
        public UnitOfWork(LearnWellDbContext db)
        {
            _db = db;
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<IAppTransaction> BeginTransactionAsync()
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            return new AppTransaction(transaction);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_db); // if we're not using a DI container to resolve repositories
        }
    }
}
