using Core.Interfaces.Repositories;
using Core.Interfaces.Specification;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly AppDbContext context;
        private readonly DbSet<T> dbSet;
        public Repository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        // Add Entity
        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }
        public List<T> AddRangeAsync(List<T> entities)
        {
            dbSet.AddRangeAsync(entities);
            return entities;
        }
        //Delete Entity
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
        public void DeleteRange(IList<T> values)
        {
            dbSet.RemoveRange(values);
        }
        //Retrive All
        public async Task<bool> AllAsync(ISpecification<T> spec) =>
            await dbSet.AllAsync(spec.Criteria[0]);
        public async Task<List<T>> GetAllAsync() =>
            await dbSet.ToListAsync();

        public async Task<List<T>> GetAllWithSpecAsync(ISpecification<T> spec) =>
            await ApplySpecification(spec).ToListAsync();
        public async Task<List<T>> GetAllByProAsync(Expression<Func<T, bool>> expression)=>
             await dbSet.Where(expression).ToListAsync();
        // Count
        public async Task<int> CountAsync() =>
           await dbSet.CountAsync();
        public async Task<int> CountAsync(ISpecification<T> spec)=>
            await ApplySpecification(spec).CountAsync();
         
        public async Task<int> CountAsync(Expression<Func<T, bool>>? selector)
        {
            if (selector == null)
                return await dbSet.CountAsync();
            else
                return await dbSet.CountAsync(selector);

        }
        // Gen One Entity By Id
        public async Task<T?> GetByIdAsync(ISpecification<T> spec)
        {
            var entities = ApplySpecification(spec);
            return await entities.FirstOrDefaultAsync();
        }
        public async Task<T?> GetByIdAsync(Guid id) =>
            await dbSet.FindAsync(id);

        public async Task<T?> GetByIdAsync(int id) =>
            await dbSet.FindAsync(id);

        // Get One Entity By Prop
        public async Task<T?> GetByPropAsync(Expression<Func<T, bool>> expression) =>
           await dbSet.FirstOrDefaultAsync(expression);
        public async Task<T?> GetByPropAsync(ISpecification<T> spec)
        {
            var entities = ApplySpecification(spec);
            return await entities.FirstOrDefaultAsync();
        }

        public async Task<IQueryable<IGrouping<object, T>>> GroupBy(ISpecification<T> spec)
        {
            var entities = ApplySpecification(spec);
            return entities.GroupBy(spec.GroupBy);
        }
        public async Task<bool> AnyAsync(ISpecification<T> spec) =>
             await dbSet.AnyAsync(spec.Criteria[0]);

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            var query = dbSet.AsQueryable();
            if (specification.Criteria != null)
            {
                foreach (var item in specification.Criteria)
                {
                    query = query.Where(item);

                }
            }
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
            //query = query.Distinct();
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            if (specification.OrderByDesc != null)
            {
                query = query.OrderByDescending(specification.OrderByDesc);
            }
            /*if (specification.GroupBy != null)
            {
                query = (IQueryable<T>)query.GroupBy(specification.GroupBy);
            }*/
            if (specification.Take != 0 && specification.Skip != 0)
                query = query.Skip((specification.Skip - 1) * specification.Take).
                    Take(specification.Take);
           
            return query;
        }

        public List<T> UpdateRangeAsync(List<T> entities)
        {
            dbSet.UpdateRange(entities);
            return entities;
        }
    }
}