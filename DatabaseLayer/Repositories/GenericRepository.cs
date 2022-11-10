using DatabaseLayer.Entities;
using DatabaseLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace DatabaseLayer.Repositories
{
    public class GenericRepository<TEntity> : IDisposable, IEntityRepository<TEntity> where TEntity : TimeTrackingSchema

    {
        private TimeTrackingDbContext context;
        private DbSet<TEntity> dbSet;
        public GenericRepository(TimeTrackingDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToArray();
        }

        public IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>>? filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity? GetById(Guid id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetByIds(IEnumerable<Guid?> guids)
        {
            return dbSet.Where(e => guids.Contains(e.Id));
        }

        public TEntity Insert(TEntity entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public IEnumerable<TEntity> InsertMany(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
            return entities;
        }

        public void Delete(Guid id)
        {
            TEntity? entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity? entity)
        {
            if (entity == null)
            {
                return;
            }
            dbSet.Remove(entity);
        }

        public void DeleteMany(IEnumerable<Guid?> entityGuids)
        {
            IEnumerable<TEntity> entities = GetByIds(entityGuids);
            DeleteMany(entities);
        }
        public void DeleteMany(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public TEntity Update(TEntity entity)
        {
            dbSet.Update(entity);
            return entity;
        }

        public IEnumerable<TEntity> UpdateMany(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            return entities;
        }
    }
}
