using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data.Services;
using TimeView.wpf.Dialogs;
using TimeView.wpf.Services;
using TimeView.wpf.ViewModel;

namespace TimeView.wpf
{
    public class ViewModelLocator
    {

        //
        // Repositories
        //
        private static IScheduleDataService scheduleDataService = new ScheduleDataService(new ScheduleRepository());
        private static ICategoryEntryDataService categoryEntryDataService = new CategoryEntryDataService(new CategoryEntryRepository());
        private static IEmployeeDataService employeeDataService = new EmployeeDataRepository(new EmployeeRepository());

        //
        // Dialogs
        //
        private static IViewDialog followingListViewDialog = new FollowingListViewDialog();
        private static IViewDialog loginViewDialog = new LoginViewDialog();
        private static IViewDialog scheduleListViewDialog = new ScheduleListViewDialog();

        //
        // ListViews
        //
        private static ScheduleListViewModel scheduleListViewModel = new ScheduleListViewModel(scheduleDataService, categoryEntryDataService);
        public static ScheduleListViewModel ScheduleListViewModel { get { return scheduleListViewModel; } }

        private static LoginViewModel loginViewModel = new LoginViewModel(employeeDataService, followingListViewDialog, loginViewDialog);
        public static LoginViewModel LoginViewModel { get { return loginViewModel; } }

        private static FollowingListViewModel followingListViewModel = new FollowingListViewModel(employeeDataService, scheduleListViewDialog);
        public static FollowingListViewModel FollowingListViewModel { get { return followingListViewModel; } }

    }
}
