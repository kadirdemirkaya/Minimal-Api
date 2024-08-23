using Microsoft.EntityFrameworkCore;
using MinimalApi2.Aws.Entities.Base;
using System.Linq.Expressions;

namespace MinimalApi2.Aws.Abstractions
{
    public interface IRepository<T>
       where T : class, IBaseEntity
    {
        DbSet<T> Table { get; }
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, params Expression<Func<T, object>>[] includeEntity);
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null, bool tracking = true, params Expression<Func<T, object>>[] includeEntity);
        Task<T> GetByAsync(int id, bool tracking = true);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool tracking = true);
        Task<int> CountAsync(Expression<Func<T, bool>> expression = null, bool tracking = true);
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entity);
        bool Update(T entity);
        bool Delete(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Guid id);
        bool DeleteRange(List<T> entity);
        Task<bool> SaveChangesAsync();
    }
}
