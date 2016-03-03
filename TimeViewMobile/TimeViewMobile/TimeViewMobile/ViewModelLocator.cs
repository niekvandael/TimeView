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

namespace TimeViewMobile
{
    public static class ViewModelLocator
    {
        //
        // Repositories
        //
        private static readonly IScheduleDataService ScheduleDataService = new ScheduleDataService(new ScheduleRepository());
        private static readonly ICategoryEntryDataService CategoryEntryDataService = new CategoryEntryDataService(new CategoryEntryRepository());
        private static readonly IEmployeeDataService EmployeeDataService = new EmployeeDataService(new EmployeeRepository());


        //
        // ListViews
        //
        public static FollowingListViewModel FollowingListViewModel { get; } = new FollowingListViewModel(EmployeeDataService);
    }
}
