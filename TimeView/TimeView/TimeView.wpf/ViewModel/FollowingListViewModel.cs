using System;
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
    public class FollowingListViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IEmployeeDataService employeeDataService;

        public ICommand OpenCommand { get; set; }
        public ICommand OpenMyCommand { get; set; }

        ScheduleListViewDialog scheduleListViewDialog;

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return this.employees;
            }
            set
            {
                this.employees = value;
                RaisePropertyChanged("Employees");
            }
        }

        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get
            {
                return selectedEmployee;
            }
            set
            {
                selectedEmployee = value;
                RaisePropertyChanged("SelectedEmployee");
            }
        }

        private Employee currentUser;
        public Employee CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        public FollowingListViewModel(IEmployeeDataService employeeDataService)
        {
            // Register to events
            Messenger.Default.Register<Employee>(this, OnEmployeeReceived);

            // Dialogs
            scheduleListViewDialog = new ScheduleListViewDialog();

            // set services
            this.employeeDataService = employeeDataService;

            // Load data & commands
            LoadCommands();
        }

        private void OnEmployeeReceived(Employee empl)
        {
            this.CurrentUser = empl;
            LoadData();
        }

        private void LoadCommands()
        {
            OpenCommand = new CustomCommand(OpenSchedule, CanOpenShedule);
            OpenMyCommand = new CustomCommand(OpenMySchedule, CanOpenMyShedule);
        }

        private void OpenSchedule(object obj)
        {
            MessageBox.Show("Open schedule");
        }

        private bool CanOpenShedule(object obj)
        {
            if (this.selectedEmployee != null)
            {
                return true;
            }
            return false;
        }

        private void OpenMySchedule(object obj)
        {
            scheduleListViewDialog.showDialog();
            Messenger.Default.Send<Employee>(this.currentUser);
        }

        private bool CanOpenMyShedule(object obj)
        {
            return true;
        }

        private async void LoadData()
        {
            CurrentUser = await employeeDataService.GetEmployee(currentUser);
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
