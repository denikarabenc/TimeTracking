using DatabaseLayer.Entities;
using DatabaseLayer.Entities.Interfaces;
using DatabaseLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private TimeTrackingDbContext context = new TimeTrackingDbContext();
        //private IEnumerable<IEntityRepository<TimeTrackingSchema>> repositories;
        private IEntityRepository<TrackedTime>? trackedTimeRepository;

        public IEntityRepository<TrackedTime> TrackedTimeRepository
        {
            get => trackedTimeRepository ??= new GenericRepository<TrackedTime>(context);
        }

        public UnitOfWork()
        {
            context.Database.EnsureCreated();
        }

        //public void Register(IEntityRepository<TimeTrackingSchema> repository)
        //{

        //}

        public void Save()
        {
            context.SaveChanges();
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
    }
}
