using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Entities
{
    public abstract class TimeTrackingSchema
    {
        public Guid? Id { get; set; }
    }
}
