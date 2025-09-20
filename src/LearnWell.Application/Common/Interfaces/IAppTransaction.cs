namespace LearnWell.Application.Common.Interfaces
{
    public interface IAppTransaction : IDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
