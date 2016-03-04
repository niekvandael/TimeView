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
        public FollowingListView master;
        public ScheduleListView scheduleList;
        private ScheduleDetailView scheduleDetail;

        public MainPage()
        {

            // Set the master and detail
            master = new FollowingListView();
            scheduleList = new ScheduleListView();
            scheduleDetail = new ScheduleDetailView();

            this.Master = master;
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

            masterPage.ListView.ItemSelected += OnItemSelected;

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
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NavigateToScheduleList();
            masterPage.ListView.SelectedItem = null;
            IsPresented = false;
        }

        void NavigateToScheduleDetails() {
            this.Detail = scheduleDetail;
        }

        void NavigateToScheduleList()
        {
            this.Detail = scheduleList;
        }

    }
}
