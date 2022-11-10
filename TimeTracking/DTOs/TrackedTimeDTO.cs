using DatabaseLayer.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracking.Models;

namespace TimeTracking.DTOs
{
    class TrackedTimeDTO : ITrackedTime
    {
        public Guid? Id { get; set; }
        public TrackedTimeDTO(TrackedTime tt)
        {
            StartedTrackingAt = tt.StartedTrackingAt;
            StoppedTrackingAt = tt.StoppedTrackingAt;
            Id = tt.Id;
        }

        public TrackedTimeDTO(DatabaseLayer.Entities.TrackedTime tt)
        {
            StartedTrackingAt = tt.StartedTrackingAt;
            StoppedTrackingAt = tt.StoppedTrackingAt;
            Id = tt.Id;
        }

        public DateTime? StartedTrackingAt
        {
            get;
        }
        public DateTime? StoppedTrackingAt
        {
            get;
        }
    }
}

