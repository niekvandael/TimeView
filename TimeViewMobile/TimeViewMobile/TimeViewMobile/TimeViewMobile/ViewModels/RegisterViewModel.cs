using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeView.data;
using TimeView.wpf.Services;
using TimeViewMobile.Extensions;
using TimeViewMobile.Messages;
using Xamarin.Forms;

namespace TimeViewMobile.ViewModels
{
    public class RegisterViewModel: INotifyPropertyChanged
    {
        private IEmployeeDataService _IEmployeeDataService;
        private Employee _employee = new Employee();
        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                RaisePropertyChanged("Employee");
            }
        }

        private String _message;
        public String Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        private String _password2;
        public String Password2
        {
            get { return _password2; }
            set
            {
                _password2 = value;
                RaisePropertyChanged("Password2");
            }
        }

        public ICommand RegisterCommand { get; set; }

        public RegisterViewModel(IEmployeeDataService IEmployeeDataService)
        {
            // Set Data
            this._IEmployeeDataService = IEmployeeDataService;
            this.Employee.Id = -1;

            // Set Commands
            RegisterCommand = new Command(RegisterAction);
        }

        private async void RegisterAction() {
            if (this._employee.Username == "" || this._employee.Password == "" || _password2 == "") {
                Message = "Please complete the form...";
                return;
            }

            if (this._employee.Password != this._password2) {
                Message = "Passwords do not match!";
                return;
            }

            if (this._employee.Password.Length < 6)
            {
                Message = "Password shoudl be at least 6 characters long!";
                return;
            }

            if (!Utilities.IsValidEmail(this._employee.Username)) {
                Message = "Emailaddres not correct!";
                return;
            }

            this._employee = await _IEmployeeDataService.CreateEmployee(this._employee);

            if (this._employee != null && this._employee.Id != -1)
            {
                MessagingCenter.Send<LoadFollowersList, Employee>(new LoadFollowersList(), "LoadFollowersList", this._employee);
            }
            else {
                Message = "Username already in use";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
