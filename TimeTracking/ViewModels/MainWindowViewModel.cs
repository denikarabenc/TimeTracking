using Common.WPFCommand;
using DatabaseLayer.Repositories;
using Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TimeTracking.DTOs;
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

        private Guid? currentlyTrackedTime;

        private ICommand? trackingButtonClick;

        private ObservableCollection<TrackedTime> trackedTimes;

        public MainWindowViewModel()
        {
            TrackedTimeService service = new TrackedTimeService(new UnitOfWork());

            trackedTimes = new ObservableCollection<TrackedTime>(service.GetTrackedTimes().Select(tt => TrackedTimeMapper.getTrackedTime(tt)));
            currentlyTrackedTime = getCurrentlyTrackedGuid();

            if (currentlyTrackedTime != null)
            {
                trackingTime = true;
                startStopTrackingText = stopTrackingTimeTextForLocalization;
            }
            else
            {
                trackingTime = false;
                startStopTrackingText = startTrackingTimeTextForLocalization;
            }

            trackingTimeTableText = trackingTimeTableTextForLocalization;
        }

        private Guid? getCurrentlyTrackedGuid()
        {
            return trackedTimes.FirstOrDefault(tt => tt.StoppedTrackingAt == null)?.Id;
        }

        private void StartTrackingTime()
        {
            TrackedTimeService service = new TrackedTimeService(new UnitOfWork()); //simulating scope dependency injection
            DatabaseLayer.Entities.TrackedTime savedTrackedTime = service.StartTrackingTime();
            TrackedTimes.Add(TrackedTimeMapper.getTrackedTime(savedTrackedTime));
        }

        private void StopTrackingTime()
        {
            TrackedTime tt = trackedTimes.First(tt => tt.StoppedTrackingAt == null);
            TrackedTimeService service = new TrackedTimeService(new UnitOfWork()); //simulating scope dependency injection
            DatabaseLayer.Entities.TrackedTime savedTrackedTime = service.StopTrackingTime(TrackedTimeMapper.getTrackedTimeDTO(tt));
            tt.StoppedTrackingAt = TrackedTimeMapper.getTrackedTime(savedTrackedTime).StoppedTrackingAt;
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
            get => trackingButtonClick ??= new RelayCommand(param => TrackTime());
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
