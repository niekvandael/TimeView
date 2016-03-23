using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;
using TimeViewMobile.Messages;
using Xamarin.Forms;

namespace TimeViewMobile.Views
{
    public partial class MainPage : MasterDetailPage
    {
        public FollowingListView followingListView;
        public ScheduleListView scheduleList;
        private ScheduleDetailView scheduleDetail;
        private Login login;
        private Register register;
        private AddFollower addFollower;

        public MainPage()
        {

            // Set the master and detail
            followingListView = new FollowingListView();
            scheduleList = new ScheduleListView();
            scheduleDetail = new ScheduleDetailView();
            login = new Login();
            register = new Register();
            addFollower = new AddFollower();

            this.Master = login;
            this.Detail = scheduleList;

            this.Master.Title = "Master";
            this.Detail.Title = "Detail";

            // Initialize
            InitializeComponent();


            // Code the display the List first
            this.Appearing += (sender, e) =>
            {
                this.IsPresented = true;
            };

            followingListView.ListView.ItemSelected += OnItemSelected;

            MessagingCenter.Subscribe<LoadDetailMessage>(this, "LoadScheduleDetailView", (sender) => {

                if(sender.IsScheduleList){
                    NavigateToScheduleList();
                    return;
                }

                if(sender.IsScheduleDetail){
                    NavigateToScheduleDetails();
                    return;
                }
            });

            MessagingCenter.Subscribe<LoadFollowersList, Employee>(this, "LoadFollowersList", (sender, arg) => {
                NavigateToFollowingList();
            });

            MessagingCenter.Subscribe<LoadRegisterView, Employee>(this, "LoadRegisterView", (sender, arg) => {
                NavigateToRegisterView();
            });

            MessagingCenter.Subscribe<ShowAddFollower, Employee>(this, "ShowAddFollower", (sender, arg) => {
                NavigateToAddFollowerView();
            });


        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NavigateToScheduleList();
            followingListView.ListView.SelectedItem = null;
            IsPresented = false;
        }

        void NavigateToScheduleDetails() {
            this.Detail = scheduleDetail;
        }

        void NavigateToScheduleList()
        {
            this.Detail = scheduleList;
        }

        void NavigateToFollowingList()
        {
            this.Master = followingListView;
            this.IsPresented = true;
        }

        void NavigateToRegisterView() {
            this.Master = register;
            this.IsPresented = true;
        }

        void NavigateToAddFollowerView() {
            this.Master = addFollower;
            this.IsPresented = true;
        }
    }
}
