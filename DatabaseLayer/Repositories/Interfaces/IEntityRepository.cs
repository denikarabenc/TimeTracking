using DatabaseLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Repositories.Interfaces
{
    public interface IEntityRepository<T> where T : TimeTrackingSchema
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");
        T? GetById(Guid id);
        IEnumerable<T> GetByIds(IEnumerable<Guid?> id);
        T Insert(T entity);
        IEnumerable<T> InsertMany(IEnumerable<T> entities);
        void Delete(Guid id);
        void DeleteMany(IEnumerable<Guid?> entityIds);
        void Delete(T id);
        void DeleteMany(IEnumerable<T> entities);
        T Update(T entity);
        IEnumerable<T> UpdateMany(IEnumerable<T> entities);
    }
}
