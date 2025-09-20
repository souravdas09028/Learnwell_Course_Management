namespace LearnWell.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveAsync();
        Task<IAppTransaction> BeginTransactionAsync();
        IRepository<T> GetRepository<T>() where T : class;
    }
}
