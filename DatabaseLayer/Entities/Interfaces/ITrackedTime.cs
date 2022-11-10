using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Entities.Interfaces
{
    public interface ITrackedTime
    {
        DateTime? StartedTrackingAt
        {
            get;
        }
        DateTime? StoppedTrackingAt
        {
            get;
        }
    }
}
