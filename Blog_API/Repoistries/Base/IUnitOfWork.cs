namespace Blog_API.Repoistries.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IMainRepoistory<T> GetRepository<T>() where T : class;
        Task<int> SaveChangesAsync();
        
    }
}
