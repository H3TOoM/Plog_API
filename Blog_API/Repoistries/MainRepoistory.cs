using Blog_API.Data;
using Blog_API.Repoistries.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog_API.Repoistries
{
    public class MainRepoistory<T> : IMainRepoistory<T> where T : class
    {
        // Inject the DbContext 
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public MainRepoistory( AppDbContext context )
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        // Get all entities
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        // Get entity by ID
        public async Task<T> GetByIdAsync( int id ) => await _dbSet.FindAsync( id ) ?? throw new KeyNotFoundException( $"Entity with ID {id} not found." );



        // Add a new entity
        public async Task<T> AddAsync( T entity )
        {
            await _dbSet.AddAsync( entity );
            return entity;
        }


        // Update an existing entity
        public async Task<T> UpdateAsync( int id, T entity )
        {
            if (id <= 0)
            {
                throw new ArgumentException( "ID must be greater than zero.", nameof( id ) );
            }
            var existingEntity = await GetByIdAsync( id );
            if (existingEntity == null)
            {
                throw new KeyNotFoundException( $"Entity with ID {id} not found." );
            }
            _context.Entry( existingEntity ).CurrentValues.SetValues( entity );
            return existingEntity;
        }

        // Delete an entity by ID
        public async Task<string> DeleteAsync( int id )
        {
            var entity = await GetByIdAsync( id );
            if (entity == null)
            {
                throw new KeyNotFoundException( $"Entity with ID {id} not found." );
            }
            _dbSet.Remove( entity );
            return "Deleted Successfully";
        }





    }
}
