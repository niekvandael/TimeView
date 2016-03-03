using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IsPresented = false;
        }
    }
}
