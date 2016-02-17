using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TimeView.data;
using TimeView.wpf.Messages;
using TimeView.wpf.Services;
using TimeView.wpf.Utility;

namespace TimeView.wpf.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IEmployeeDataService employeeDataService;

        public ICommand LoginCommand { get; set; }
        public ICommand CreateAccount { get; set; }
        private FollowingListViewDialog followingListViewDialog;

        private String message;
        public String Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                RaisePropertyChanged("Message");
            }
        }


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
            CreateAccount = new CustomCommand(CreateAccountAction, CanCreateAccount);
        }

        private void CreateAccountAction(object obj)
        {
            this.Message = "Send email to niek.vandael@student.pxl.be";
        }

        private bool CanCreateAccount(object obj)
        {
            return true;
        }

        private async void LoginAction(object obj)
        {
            Employee _empl = await employeeDataService.GetEmployee(this.Employee.Username, this.Employee.Password);

            if (_empl != null)
            {
                this.Employee = _empl;
                Application.Current.MainWindow.Hide();
                followingListViewDialog.showDialog();

                Messenger.Default.Send<LoginMessage>(new LoginMessage { Employee = this.employee });
            }
            else {
                this.Message = "Incorrect username or password";
            }
        }

        private bool CanLogin(object obj)
        {
            if (Employee.Username != null && Employee.Username != "" && Employee.Password != null && Employee.Password != "")
            {
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