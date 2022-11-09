using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracking.Models
{
    public class TrackedTime : INotifyPropertyChanged
    {
        private DateTime? startedTrackingAt;
        private DateTime? stoppedTrackingAt;

        public TrackedTime()
        {

        }
        public TrackedTime(DateTime? startedTrackingAt)
        {
            this.StartedTrackingAt = startedTrackingAt;
        }

        public TrackedTime(DateTime? startedTrackingAt, DateTime? stoppedTrackingAt)
        {
            this.StartedTrackingAt = startedTrackingAt;
            this.StoppedTrackingAt = stoppedTrackingAt;
        }

        public DateTime? StartedTrackingAt
        {
            get => startedTrackingAt;
            set { startedTrackingAt = value; OnPropertyChanged(nameof(StartedTrackingAt)); }
        }
        public DateTime? StoppedTrackingAt
        {
            get => stoppedTrackingAt;
            set { stoppedTrackingAt = value; OnPropertyChanged(nameof(StoppedTrackingAt)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
