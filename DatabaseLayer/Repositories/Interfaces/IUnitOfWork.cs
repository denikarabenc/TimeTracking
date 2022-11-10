using DatabaseLayer.Entities;
using DatabaseLayer.Entities.Interfaces;

namespace DatabaseLayer.Repositories.Interfaces
{
    //TODO improve so everything is injected, so repositories UoW should not have to change for adding another repository
    public interface IUnitOfWork
    {
        IEntityRepository<TrackedTime> TrackedTimeRepository { get; }
        void Save();
    }
}
