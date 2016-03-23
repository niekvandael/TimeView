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

        private Employee _following;
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
            MessagingCenter.Send<LoadFollowersList, Employee>(new LoadFollowersList(), "LoadFollowersList", this._currentUser);
        }

        private void AddNewFollower() {
            this.CurrentUser.Following.Add(this._following);
            this._employeeDataService.UpdateEmployee(this._currentUser);
        }
    }
}
