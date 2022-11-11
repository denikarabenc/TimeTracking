using DatabaseLayer.Entities;
using DatabaseLayer.Repositories;
using DatabaseLayer.Repositories.Interfaces;

namespace Services
{
    public class TrackedTimeService
    {

        private IUnitOfWork unitOfWork;

        public TrackedTimeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<TrackedTime> GetTrackedTimes()
        {
            return unitOfWork.TrackedTimeRepository.GetAll();
        }

        public TrackedTime StartTrackingTime()
        {
            TrackedTime tt = unitOfWork.TrackedTimeRepository.Insert(new TrackedTime());
            unitOfWork.Save();
            return tt;
        }

        public TrackedTime StopTrackingTime(TrackedTime trackedTimeToStop)
        {
            trackedTimeToStop.StoppedTrackingAt = DateTime.UtcNow;
            TrackedTime savedTrackedTime = unitOfWork.TrackedTimeRepository.Update(trackedTimeToStop);
            unitOfWork.Save();
            return savedTrackedTime;
        }
    }
}