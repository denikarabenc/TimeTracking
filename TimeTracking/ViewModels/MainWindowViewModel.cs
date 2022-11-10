﻿using Common.WPFCommand;
using DatabaseLayer.Entities.Interfaces;
using DatabaseLayer.Repositories;
using DatabaseLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        //This is fine to be aquired via database layer because we have no more layers in between, it will not help in current implementation
        private IUnitOfWork unitOfWork;

        public MainWindowViewModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            trackedTimes = new ObservableCollection<TrackedTime>(unitOfWork.TrackedTimeRepository.GetAll().Select(tt => TrackedTimeMapper.getTrackedTime(tt)));
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
            IUnitOfWork unitOfWork = new UnitOfWork();
            TrackedTime tt = new TrackedTime(DateTime.UtcNow);
            DatabaseLayer.Entities.TrackedTime savedTrackedTime = unitOfWork.TrackedTimeRepository.Insert(TrackedTimeMapper.getTrackedTimeDTO(tt));
            TrackedTimes.Add(TrackedTimeMapper.getTrackedTime(savedTrackedTime));
            unitOfWork.Save();
        }

        private void StopTrackingTime()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            TrackedTime tt = trackedTimes.First(tt => tt.StoppedTrackingAt == null);
            tt.StoppedTrackingAt = DateTime.UtcNow;
            DatabaseLayer.Entities.TrackedTime savedTrackedTime = unitOfWork.TrackedTimeRepository.Update(TrackedTimeMapper.getTrackedTimeDTO(tt));
            unitOfWork.Save();
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
