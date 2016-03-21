using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeView.data;
using TimeView.wpf.Services;
using TimeViewMobile.Messages;
using Xamarin.Forms;

namespace TimeViewMobile.ViewModels
{
    public class LoginViewModel: INotifyPropertyChanged
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

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public LoginViewModel(IEmployeeDataService IEmployeeDataService)
        {
            // Set Data
            this._IEmployeeDataService = IEmployeeDataService;
            this.Employee.Id = -1;

            // Set Commands
            LoginCommand = new Command(LoginAction);
            RegisterCommand = new Command(RegisterAction);
        }

        private async void LoginAction() {
            this._employee = await _IEmployeeDataService.GetEmployee(this._employee.Username, this._employee.Password);

            if (this._employee != null && this._employee.Id != -1)
            {
                MessagingCenter.Send<LoadFollowersList, Employee>(new LoadFollowersList(), "LoadFollowersList", this._employee);
            }
            else {
                Message = "Incorrect credentials";
            }
        }

        private void RegisterAction() {
            // Show empty register page
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
