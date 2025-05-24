using Core.Interfaces.Specification;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Core.Interfaces.Repositories
{
    public interface IRepository<T>
    { 
        // add Entity
        Task<T> AddAsync(T entity);
        List<T> UpdateRangeAsync(List<T> entities);
        List<T> AddRangeAsync(List<T> entities);
        //Delete Entity
        void Delete(T enity);
        void DeleteRange(IList<T> values);
        //Retrive All
        Task<bool> AllAsync(ISpecification<T> spec);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllWithSpecAsync(ISpecification<T> sepc);
        Task<List<T>> GetAllByProAsync(Expression<Func<T, bool>> expression);
        // Count
        Task<int> CountAsync();
        Task<int> CountAsync(ISpecification<T> spec);
        Task<int> CountAsync(Expression<Func<T, bool>>? selector);
        // Gen One Entity By Id
        Task<T?> GetByIdAsync(ISpecification<T> spec);
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByIdAsync(int id);
        // Get One Entity By Prop
        Task<T?> GetByPropAsync(ISpecification<T> spec);
        Task<T?> GetByPropAsync(Expression<Func<T,bool>> expression);

        Task<bool> AnyAsync(ISpecification<T> spec);

    }
}