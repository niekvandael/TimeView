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
    public class AddFollowerViewModel: INotifyPropertyChanged
    {

        public ICommand NewCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private readonly IEmployeeDataService _employeeDataService;
        private Employee _currentUser;
        public Employee CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        private Employee _following = new Employee { Id = -1 };
        public Employee Following
        {
            get { return _following; }
            set
            {
                _following = value;
                RaisePropertyChanged("Following");
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

        private String _emailToFollow;
        public String EmailToFollow
        {
            get { return _emailToFollow; }
            set
            {
                _emailToFollow = value;
                RaisePropertyChanged("EmailToFollow");
            }
        }


        public ICommand LoginCommand { get; set; }

        public AddFollowerViewModel(IEmployeeDataService IEmployeeDataService)
        {
            this._employeeDataService = IEmployeeDataService;

            // Register to events
            MessagingCenter.Subscribe<ShowAddFollower, Employee>(this, "ShowAddFollower", (sender, arg) => {
                this._currentUser = arg;
            });

            // Commands
            NewCommand = new Command(AddNewFollower);
            CancelCommand = new Command(ShowFollowingList);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ShowFollowingList() {
            this.CleanUp();
            MessagingCenter.Send<LoadFollowersList, Employee>(new LoadFollowersList(), "LoadFollowersList", this._currentUser);
        }

        private async void AddNewFollower() {
            Message = "";
            if (!Utilities.IsValidEmail(this._following.Username)){
                Message = "Email address not valid";
                return;
            };

            if (this._following.Username.ToLower() == this._currentUser.Username.ToLower())
            {
                Message = "Cannot add yourself";
                return;
            };

            if (this._currentUser.Following == null) {
                this._currentUser.Following = new List<Employee>();
            }

            this._currentUser.Following.Add(this._following);
            bool success = await this._employeeDataService.UpdateEmployee(this._currentUser);

            if (success) {
                this.CleanUp();
                MessagingCenter.Send<LoadFollowersList, Employee>(new LoadFollowersList(), "LoadFollowersList", this._currentUser);
            }
        }

        private void CleanUp() {
            this.Message = "";
            this.Following = new Employee();
        }
    }
}
