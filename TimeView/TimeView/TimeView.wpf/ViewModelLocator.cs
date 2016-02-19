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
        private static readonly IScheduleDataService ScheduleDataService = new ScheduleDataService(new ScheduleRepository());
        private static readonly ICategoryEntryDataService CategoryEntryDataService = new CategoryEntryDataService(new CategoryEntryRepository());
        private static readonly IEmployeeDataService EmployeeDataService = new EmployeeDataService(new EmployeeRepository());
        private static readonly IOpenDataDataService OpenDataDataService = new OpenDataDataService(new OpenDataRepository());

        //
        // Dialogs
        //
        private static readonly IViewDialog FollowingListViewDialog = new FollowingListViewDialog();
        private static readonly IViewDialog LoginViewDialog = new LoginViewDialog();
        private static readonly IViewDialog ScheduleListViewDialog = new ScheduleListViewDialog();
        private static readonly IViewDialog OpenDataListViewDialog = new OpenDataListViewDialog();

        //
        // ListViews
        //
        public static ScheduleListViewModel ScheduleListViewModel { get; } = new ScheduleListViewModel(ScheduleDataService, CategoryEntryDataService);
        public static LoginViewModel LoginViewModel { get; } = new LoginViewModel(EmployeeDataService, FollowingListViewDialog, LoginViewDialog);
        public static FollowingListViewModel FollowingListViewModel { get; } = new FollowingListViewModel(EmployeeDataService, ScheduleListViewDialog, OpenDataListViewDialog);
        public static OpenDataListViewModel OpenDataListViewModel { get; } = new OpenDataListViewModel(OpenDataDataService);
    }
}