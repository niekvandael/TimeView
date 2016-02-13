﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TimeView.data;
using TimeView.wpf.Extensions;
using TimeView.wpf.Services;
using TimeView.wpf.Utility;

namespace TimeView.wpf.ViewModel
{
    public class ScheduleListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IScheduleDataService scheduleDataService;

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        private ObservableCollection<Schedule> schedules;
        public ObservableCollection<Schedule> Schedules
        {
            get
            {
                return this.schedules;
            }
            set
            {
                this.schedules = value;
                RaisePropertyChanged("Schedules");
            }
        }

        private Schedule selectedSchedule;
        public Schedule SelectedSchedule
        {
            get
            {
                return selectedSchedule;
            }
             set
            {
                selectedSchedule = value;
                RaisePropertyChanged("SelectedSchedule");
            }
        }

        public ScheduleListViewModel(IScheduleDataService scheduleDataService)
        {
            this.scheduleDataService = scheduleDataService;

            LoadData();
            LoadCommands();
        }

        private void LoadCommands()
        {
            NewCommand = new CustomCommand(NewSchedule, CanNewSchedule);
            SaveCommand = new CustomCommand(SaveSchedule, CanSaveSchedule);
        }

        private void NewSchedule(object obj)
        {
            MessageBox.Show("new schedule");
        }

        private bool CanNewSchedule(object obj)
        {
            return true;
        }

        private void SaveSchedule(object obj)
        {
            MessageBox.Show("save schedule");
        }

        private bool CanSaveSchedule(object obj)
        {
            // TODO CHECK IF EVERYTHING IS OKIDOKI
            return true;
        }

        private void LoadData()
        {
            Schedules = scheduleDataService.getScheduleForEmployee(1).ToObservableCollection();
        }


        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
