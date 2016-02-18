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
using TimeView.wpf.Dialogs;
using TimeView.wpf.Extensions;
using TimeView.wpf.Messages;
using TimeView.wpf.Services;
using TimeView.wpf.Utility;


namespace TimeView.wpf.ViewModel
{
    public class FollowingListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IEmployeeDataService employeeDataService;

        public ICommand OpenCommand { get; set; }
        public ICommand OpenMyCommand { get; set; }

        private IViewDialog scheduleListViewDialog;

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

        public FollowingListViewModel(IEmployeeDataService employeeDataService, IViewDialog scheduleListViewDialog)
        {
            // Register to events
            Messenger.Default.Register<LoginMessage>(this, OnLoginReceived);

            // Dialogs
            this.scheduleListViewDialog = scheduleListViewDialog;

            // set services
            this.employeeDataService = employeeDataService;

            // Load data & commands
            LoadCommands();
        }

        private void OnLoginReceived(LoginMessage loginMessage)
        {
            this.CurrentUser = loginMessage.Employee;
            LoadData();
        }

        private void LoadCommands()
        {
            OpenCommand = new CustomCommand(OpenSchedule, CanOpenShedule);
            OpenMyCommand = new CustomCommand(OpenMySchedule, CanOpenMyShedule);
        }

        private void OpenSchedule(object obj)
        {
            scheduleListViewDialog.ShowDialog(selectedEmployee.Name);
            Messenger.Default.Send<LoadScheduleList>(new LoadScheduleList { Employee = selectedEmployee, MySchedule = false });
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
            scheduleListViewDialog.ShowDialog("My schedule");
            Messenger.Default.Send<LoadScheduleList>(new LoadScheduleList { Employee = currentUser, MySchedule = true});
        }

        private bool CanOpenMyShedule(object obj)
        {
            return true;
        }

        public async void LoadData()
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
