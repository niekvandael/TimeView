﻿#region

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using TimeView.data;
using TimeView.wpf.Dialogs;
using TimeView.wpf.Extensions;
using TimeView.wpf.Messages;
using TimeView.wpf.Services;
using TimeView.wpf.Utility;

#endregion

namespace TimeView.wpf.ViewModel
{
    public class FollowingListViewModel : INotifyPropertyChanged
    {
        private readonly IEmployeeDataService _employeeDataService;
        private readonly IViewDialog _scheduleListViewDialog;
        private readonly IViewDialog _openDataListViewDialog;
        private Employee _currentUser;
        private ObservableCollection<Employee> _employees;
        private Employee _selectedEmployee;

        public FollowingListViewModel(IEmployeeDataService employeeDataService, IViewDialog scheduleListViewDialog, IViewDialog openDataListViewDialog)
        {
            // Register to events
            Messenger.Default.Register<LoginMessage>(this, OnLoginReceived);

            // Dialogs
            _scheduleListViewDialog = scheduleListViewDialog;
            _openDataListViewDialog = openDataListViewDialog;

            // set services
            _employeeDataService = employeeDataService;

            // Load data & commands
            LoadCommands();
        }

        public ICommand OpenCommand { get; set; }
        public ICommand OpenMyCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        public ICommand OpenDataCommand { get; set; }

        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                RaisePropertyChanged("Employees");
            }
        }

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                RaisePropertyChanged("SelectedEmployee");
            }
        }

        public Employee CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnLoginReceived(LoginMessage loginMessage)
        {
            CurrentUser = loginMessage.Employee;
            LoadData();
        }

        private void LoadCommands()
        {
            OpenCommand = new CustomCommand(OpenSchedule, CanOpenShedule);
            OpenMyCommand = new CustomCommand(OpenMySchedule, CanOpenMyShedule);

            OpenDataCommand = new CustomCommand(OpenOpenData, CanAlwaysOpen);
            AboutCommand = new CustomCommand(OpenAbout, CanAlwaysOpen);
        }

        private void OpenAbout(object obj)
        {
            MessageBox.Show("Created by Niek Vandael for PXL");
        }

        private bool CanAlwaysOpen(object obj)
        {
            return true;
        }

        private void OpenOpenData(object obj)
        {
            _openDataListViewDialog.ShowDialog("Open Data");
        }

        private void OpenSchedule(object obj)
        {
            _scheduleListViewDialog.ShowDialog(_selectedEmployee.Name);
            Messenger.Default.Send(new LoadScheduleList {Employee = _selectedEmployee, MySchedule = false});
        }

        private bool CanOpenShedule(object obj)
        {
            if (_selectedEmployee != null)
            {
                return true;
            }
            return false;
        }

        private void OpenMySchedule(object obj)
        {
            _scheduleListViewDialog.ShowDialog("My schedule");
            Messenger.Default.Send(new LoadScheduleList {Employee = _currentUser, MySchedule = true});
        }

        private bool CanOpenMyShedule(object obj)
        {
            return true;
        }

        public async void LoadData()
        {
            CurrentUser = await _employeeDataService.GetEmployee(_currentUser);
            Employees = CurrentUser.Following.ToObservableCollection();
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