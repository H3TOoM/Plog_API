using Blog_API.Data;
using Blog_API.Repoistries.Base;

namespace Blog_API.Repoistries
{
    public class UnitOfWork : IUnitOfWork
    {

        // Store repositories for each entity type to avoid creating them multiple times
        private readonly Dictionary<Type, object> _repositories = new();


        // Inject the DbContext
        private readonly AppDbContext _context;
        public UnitOfWork( AppDbContext context )
        {
            _context = context;
        }


        public IMainRepoistory<T> GetRepository<T>() where T : class
        {
            var type = typeof( T );

            // If repository for this entity doesn't exist, create and store it
            if (!_repositories.ContainsKey( type ))
            {
                var repoInstance = new MainRepoistory<T>( _context );
                _repositories[type] = repoInstance;
            }

            return (IMainRepoistory<T>)_repositories[type];
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        void IDisposable.Dispose()
        {
            _context.Dispose();
        }
    }
}
