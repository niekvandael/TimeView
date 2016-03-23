using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeView.data;
using TimeView.wpf.Services;
using TimeViewMobile.Extensions;
using TimeViewMobile.Messages;
using TimeViewMobile.Views;
using Xamarin.Forms;

namespace TimeViewMobile.ViewModels
{
    public class FollowingListViewModel : INotifyPropertyChanged
    {
        private readonly IEmployeeDataService _employeeDataService;
        public FollowingListViewModel(IEmployeeDataService employeeDataService)
        {
            // Register to events
            MessagingCenter.Subscribe<LoadFollowersList, Employee>(this, "LoadFollowersList", (sender, arg) => {
                this._currentUser = arg;
                LoadData();
            });

            // Dialogs

            // set services
            _employeeDataService = employeeDataService;

            // Load data & commands
            this.Employees = new ObservableCollection<Employee>();
            NewCommand = new Command(ShowAddFollower);
        }

        public ICommand NewCommand { get; set; }

        private String _title = "Select Schedule";
        public String Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        private ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                RaisePropertyChanged("Employees");
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                RaisePropertyChanged("SelectedEmployee");

                OpenDetail(value);
            }
        }

        private Employee _currentUser;
        public Employee CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _selectedEmployee = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        private void LoadCommands()
        {
        }

        public void OpenDetail(Employee item)
        {
            if (item != null)
            {
                // Send a message to update detail
                Boolean mySchedule = false;
                if (item.Id == this.CurrentUser.Id) {
                    mySchedule = true;
                }
                MessagingCenter.Send<DetailMessage, Employee>(new DetailMessage { MySchedule = mySchedule }, "LoadScheduleForUser", item);
            }
        }

        private void OpenEmployeeSchedule(object obj)
        {
            
        }

        private bool CanOpenEmployeeSchedule(object obj)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadData()
        {

            Employee copyCurrentUser = _currentUser;
            copyCurrentUser.ImageSource = "people.png";
            copyCurrentUser.Name = "My Schedule";

            var employees = new ObservableCollection<Employee>();
            employees.Add(copyCurrentUser);
            if (CurrentUser.Following != null)
            {
                foreach (Employee emp in CurrentUser.Following)
                {
                    emp.ImageSource = "reminder.png";
                    employees.Add(emp);
                }
            }
            Employees = employees.ToObservableCollection();
        }


        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ShowAddFollower() {
            MessagingCenter.Send<ShowAddFollower, Employee>(new ShowAddFollower(), "ShowAddFollower", this._currentUser);
        }
    }
}
