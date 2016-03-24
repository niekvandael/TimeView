using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using TimeView.data;
using TimeView.data.Services;
using TimeView.wpf.Dialogs;
using TimeView.wpf.Messages;
using TimeView.wpf.Services;
using TimeView.wpf.Utility;

namespace TimeView.wpf.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private Employee _employee = new Employee();
        private readonly IEmployeeDataService _employeeDataService;
        private readonly IViewDialog _followingListViewDialog;
        private readonly IViewDialog _loginViewDialog;
        private string _message;
        private string _loading = "Hidden";

        public LoginViewModel(IEmployeeDataService employeeDataService, IViewDialog followingListViewDialog,
            IViewDialog loginViewDialog)
        {
            _employeeDataService = employeeDataService;
            _followingListViewDialog = followingListViewDialog;
            _loginViewDialog = loginViewDialog;

            LoadCommands();
        }

        public ICommand LoginCommand { get; set; }
        public ICommand CreateAccount { get; set; }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        public string Loading
        {
            get { return _loading; }
            set
            {
                _loading = value;
                RaisePropertyChanged("Loading");
            }
        }

        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                RaisePropertyChanged("Employee");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void LoadCommands()
        {
            LoginCommand = new CustomCommand(LoginAction, CanLogin);
            CreateAccount = new CustomCommand(CreateAccountAction, CanCreateAccount);
        }

        private void CreateAccountAction(object obj)
        {
            Message = "Send email to niek.vandael@student.pxl.be";
        }

        private bool CanCreateAccount(object obj)
        {
            return true;
        }

        private void LoginAction(object obj)
        {
            Loading = "Visible";
            DoLogin(Employee.Username, Employee.Password);
        }

        public async void DoLogin(string username, string password)
        {
            var empl = await _employeeDataService.GetEmployee(username, password);

            if (empl == null)
            {
                Message = "Incorrect username or password";
            }
            else
            {
                Employee = empl;
                _loginViewDialog.CloseDialog();
                _followingListViewDialog.ShowDialog("People you are following");

                Messenger.Default.Send(new LoginMessage {Employee = _employee});
            }
        }

        private bool CanLogin(object obj)
        {
            if (!string.IsNullOrEmpty(Employee.Username) &&
                !String.IsNullOrEmpty(Employee.Password))
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