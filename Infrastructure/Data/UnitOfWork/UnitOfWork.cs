using Core.Interfaces.IServices.SystemIServices;
using Core.Interfaces.Repositories;
using Core.Interfaces.Specifications;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Net;
namespace Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly AppDbContext _context;
        private Hashtable _repositories;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private Dictionary<object, DbSet<object>> addedEntities = new Dictionary<object, DbSet<object>>();
        private Dictionary<object, DbSet<object>> modifiedEntities = new Dictionary<object, DbSet<object>>();
        private List<object> deletedEntities = new List<object>();

        public UnitOfWork(AppDbContext context, IServiceScopeFactory serviceScopeFactory)
        {
            _context = context;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new Repository<TEntity>(_context);
                _repositories.Add(type, repository);
            }

            return _repositories[type] as IRepository<TEntity>;
        }

        public async Task<int> Complete()
        {
            try
            {
                foreach (var entity in deletedEntities)
                {
                    _context.Entry(entity).State = EntityState.Deleted;
                }
                var result = await _context.SaveChangesAsync();

                ClearTracking();

                return result;
            }
            catch (Exception ex)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {

                    var logger = scope.ServiceProvider.GetRequiredService<ILoggerService>();

                    logger.LogError("Error Saving Changes", ex);

                }
                throw new Exception(ex.Message);
            }
        }

        public void Rollback()
        {
            ClearTracking();
        }

        public async ValueTask DisposeAsync()
            => await _context.DisposeAsync();

        public void RegisterRemoved<TEntity>(TEntity entity) where TEntity : class
        {
            var set = _context.Set<TEntity>();

            if (modifiedEntities.ContainsKey(entity))
            {
                modifiedEntities.Remove(entity);
                return;
            }

            deletedEntities.Add(entity);
        }

        private void ClearTracking()
        {
            addedEntities.Clear();
            modifiedEntities.Clear();
            deletedEntities.Clear();
        }
    }


}