using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Models;

namespace TimeTracking.DTOs
{
    public static class TrackedTimeMapper
    {
        public static DatabaseLayer.Entities.TrackedTime getTrackedTimeDTO(TrackedTime trackedTime)
        {
            DatabaseLayer.Entities.TrackedTime tt = new DatabaseLayer.Entities.TrackedTime();
            tt.Id = trackedTime.Id;
            tt.StoppedTrackingAt = trackedTime.StoppedTrackingAt;
            tt.StartedTrackingAt = trackedTime.StartedTrackingAt;
            return tt;
        }

        public static TrackedTime getTrackedTime(DatabaseLayer.Entities.TrackedTime trackedTime)
        {
            return new TrackedTime(trackedTime.StartedTrackingAt, trackedTime.StoppedTrackingAt, trackedTime.Id);
        }
    }
}
