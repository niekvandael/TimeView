using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.DAL.Repositories.Category;
using TimeView.DAL.Repositories.Employee;
using TimeView.DAL.Repositories.Schedule;
using TimeView.wpf.Services;
using TimeViewMobile.ViewModels;
using Xamarin.Forms;

namespace TimeViewMobile.Views
{
    public partial class FollowingListView : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public FollowingListView()
        {
            InitializeComponent();

            //
            // Repositories
            //
            IScheduleDataService ScheduleDataService = new ScheduleDataService(new ScheduleRepository());
            ICategoryEntryDataService CategoryEntryDataService = new CategoryEntryDataService(new CategoryEntryRepository());
            IEmployeeDataService EmployeeDataService = new EmployeeDataService(new EmployeeRepository());


            //
            // ListViews
            //
            FollowingListViewModel FollowingListViewModel = new FollowingListViewModel(EmployeeDataService);

            this.BindingContext = FollowingListViewModel;
        }
    }
}
