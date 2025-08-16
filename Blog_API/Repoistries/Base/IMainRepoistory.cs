using System.Linq.Expressions;

namespace Blog_API.Repoistries.Base
{
    public interface IMainRepoistory<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task<T> GetByConditionAsync( Expression<Func<T, bool>> predicate );

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(int id,T entity);

        Task<string> DeleteAsync(int id);
    }
}
