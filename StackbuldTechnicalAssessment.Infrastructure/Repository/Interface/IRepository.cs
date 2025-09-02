using Microsoft.EntityFrameworkCore.Storage;
using StackbuldTechnicalAssessment.Domain.Entities;
using System.Linq.Expressions;


namespace StackbuldTechnicalAssessment.Infrastructure.Repository.Interface
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetAsync(Guid id);
        Task<T> GetBySpec(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAllBySpec(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetQueryableBySpec(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetQueryable(Func<IQueryable<T>, IQueryable<T>> queryShaper);
        IQueryable<T> Query();

        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<T> DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task<bool> Exists(Guid id);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task SaveChanges();
    }
}
