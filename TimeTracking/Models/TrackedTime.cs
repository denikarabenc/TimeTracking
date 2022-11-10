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
        private Guid? id;
        private DateTime? startedTrackingAt;
        private DateTime? stoppedTrackingAt;
        private string interval;

        public TrackedTime()
        {
            interval = string.Empty;

        }
        public TrackedTime(DateTime? startedTrackingAt) : this()
        {
            this.startedTrackingAt = startedTrackingAt;
        }

        public TrackedTime(DateTime? startedTrackingAt, DateTime? stoppedTrackingAt) : this(startedTrackingAt)
        {
            this.stoppedTrackingAt = stoppedTrackingAt;
        }

        public TrackedTime(DateTime? startedTrackingAt, DateTime? stoppedTrackingAt, Guid? id) : this(startedTrackingAt, stoppedTrackingAt)
        {
            this.id = id;
        }

        public DateTime? StartedTrackingAt
        {
            get => startedTrackingAt;
            set { startedTrackingAt = value; OnPropertyChanged(nameof(StartedTrackingAt)); }
        }
        public DateTime? StoppedTrackingAt
        {
            get => stoppedTrackingAt;
            set
            {
                stoppedTrackingAt = value; OnPropertyChanged(nameof(StoppedTrackingAt));
                Interval = GetInterval();
            }
        }

        public Guid? Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Interval
        {
            get => interval;
            private set { interval = value; OnPropertyChanged(nameof(Interval)); }
        }

        private string GetInterval()
        {
            TimeSpan? interval = StoppedTrackingAt - StartedTrackingAt;
            if (interval == null)
            {
                return string.Empty;
            }

            return string.Format("{0} hours and {1} minutes", interval.Value.Hours, interval.Value.Minutes);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
