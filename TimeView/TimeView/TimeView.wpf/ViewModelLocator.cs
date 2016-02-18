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
        private static readonly IScheduleDataService scheduleDataService =
            new ScheduleDataService(new ScheduleRepository());

        private static readonly ICategoryEntryDataService categoryEntryDataService =
            new CategoryEntryDataService(new CategoryEntryRepository());

        private static readonly IEmployeeDataService employeeDataService =
            new EmployeeDataRepository(new EmployeeRepository());

        //
        // Dialogs
        //
        private static readonly IViewDialog followingListViewDialog = new FollowingListViewDialog();
        private static readonly IViewDialog loginViewDialog = new LoginViewDialog();
        private static readonly IViewDialog scheduleListViewDialog = new ScheduleListViewDialog();

        //
        // ListViews
        //

        public static ScheduleListViewModel ScheduleListViewModel { get; } =
            new ScheduleListViewModel(scheduleDataService, categoryEntryDataService);

        public static LoginViewModel LoginViewModel { get; } = new LoginViewModel(employeeDataService,
            followingListViewDialog, loginViewDialog);

        public static FollowingListViewModel FollowingListViewModel { get; } =
            new FollowingListViewModel(employeeDataService, scheduleListViewDialog);
    }
}