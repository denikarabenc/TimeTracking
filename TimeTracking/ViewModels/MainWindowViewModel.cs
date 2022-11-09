using Common.WPFCommand;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeTracking.Models;

namespace TimeTracking.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private static readonly string startTrackingTimeTextForLocalization = "Start tracking time";
        private static readonly string stopTrackingTimeTextForLocalization = "Stop tracking time";
        private static readonly string trackingTimeTableTextForLocalization = "Tracked time: ";

        private bool trackingTime;

        private string startStopTrackingText;
        private string trackingTimeTableText;

        private ICommand? trackingButtonClick;

        private ObservableCollection<TrackedTime> trackedTimes;

        public MainWindowViewModel()
        {
            trackingTime = false;
            startStopTrackingText = startTrackingTimeTextForLocalization;
            trackedTimes = new ObservableCollection<TrackedTime>();
            trackingTimeTableText = trackingTimeTableTextForLocalization;
        }

        private void StartTrackingTime()
        {
            TrackedTimes.Add(new TrackedTime(DateTime.UtcNow));
        }

        private void StopTrackingTime()
        {
            TrackedTimes.Last().StoppedTrackingAt = DateTime.UtcNow;
        }
        private void TrackTime()
        {
            TrackingTime = !TrackingTime;
            setTrackingText();

            if (TrackingTime)
            {
                StartTrackingTime();
            }
            else
            {
                StopTrackingTime();
            }
        }

        private void setTrackingText()
        {
            if (trackingTime)
            {
                StartStopTrackingText = stopTrackingTimeTextForLocalization;
            }
            else
            {
                StartStopTrackingText = startTrackingTimeTextForLocalization;
            }
        }

        public string TrackingTimeTableText { get => trackingTimeTableText; }

        public ICommand TrackingButtonClick
        {
            get => trackingButtonClick ?? (trackingButtonClick = new RelayCommand(param => TrackTime()));
        }

        public string StartStopTrackingText
        {
            get => startStopTrackingText; set
            {
                startStopTrackingText = value;
                OnPropertyChanged(nameof(StartStopTrackingText));
            }
        }

        public bool TrackingTime
        {
            get => trackingTime;
            private set => trackingTime = value;
        }
        public ObservableCollection<TrackedTime> TrackedTimes { get => trackedTimes; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
