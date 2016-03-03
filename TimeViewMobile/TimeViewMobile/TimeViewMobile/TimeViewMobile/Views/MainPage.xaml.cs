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
        public ScheduleListView detail;

        public MainPage()
        {

            // Set the master and detail
            master = new FollowingListView();
            detail = new ScheduleListView();

            this.Master = master;
            this.Detail = detail;

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
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            masterPage.ListView.SelectedItem = null;
            IsPresented = false;
        }
    }
}
