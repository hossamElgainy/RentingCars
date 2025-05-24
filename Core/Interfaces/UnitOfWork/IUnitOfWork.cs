
using Core.Interfaces.Repositories;

namespace Core.Interfaces.Specifications
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Complete();
        public void Rollback();
    }
}
