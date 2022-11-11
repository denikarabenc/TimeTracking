using DatabaseLayer.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Entities
{
    [Table("TrackedTime")]
    public class TrackedTime : TimeTrackingSchema, ITrackedTime
    {
        public TrackedTime()
        {
            StartedTrackingAt = DateTime.UtcNow;
        }
        public DateTime? StartedTrackingAt
        {
            get; set;
        }
        public DateTime? StoppedTrackingAt
        {
            get; set;
        }
    }

}
