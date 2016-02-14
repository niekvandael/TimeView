using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TimeView.data;
using TimeView.wpf.Services;
using TimeView.wpf.Utility;

namespace TimeView.wpf.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IEmployeeDataService employeeDataService;

        public ICommand LoginCommand { get; set; }

        private FollowingListViewDialog followingListViewDialog;

        private Employee employee = new Employee();
        public Employee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                RaisePropertyChanged("Employee");
            }
        }

        public LoginViewModel(IEmployeeDataService employeeDataService)
        {
            this.employeeDataService = employeeDataService;

            followingListViewDialog = new FollowingListViewDialog();

            LoadCommands();
        }

        private void LoadCommands()
        {
            LoginCommand = new CustomCommand(LoginAction, CanLogin);
        }

        private async void LoginAction(object obj)
        {
            Employee _empl = await employeeDataService.GetEmployee(this.Employee.Username, this.Employee.Password);

            if (_empl != null)
            {
                this.Employee = _empl;

                followingListViewDialog.showDialog();

                Messenger.Default.Send<Employee>(this.employee);
            }
            else {
                MessageBox.Show("Login failed");
            }
        }

        private bool CanLogin(object obj)
        {
            if (Employee.Username != null && Employee.Username != "" && Employee.Password != null && Employee.Password != "") {
                return true;
            }
            return false;
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