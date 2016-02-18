using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeView.data;
using TimeView.Test.Mocks.DataServices;
using TimeView.Test.Mocks.Dialogs;
using TimeView.wpf.Dialogs;
using TimeView.wpf.Services;
using TimeView.wpf.ViewModel;

namespace TimeView.Test
{
    [TestClass]
    public class FollowingListViewModelTests
    {
        private IEmployeeDataService _employeeDataService;
        private IViewDialog _scheduleListViewDialog;

        private FollowingListViewModel GetViewModel()
        {
            return new FollowingListViewModel(_employeeDataService, _scheduleListViewDialog);
        }

        [TestInitialize]
        public void Init()
        {
            _employeeDataService = new MockEmployeeDataService();
            _scheduleListViewDialog = new MockScheduleListViewDialog();
        }

        [TestMethod]
        public void LoadAllFollowings()
        {
            //Arrange
            ObservableCollection<Employee> following;
            var expectedEmployees = _employeeDataService.GetEmployee(new Employee {Id = 1}).Result.Following;

            //act
            var viewModel = GetViewModel();
            viewModel.CurrentUser = _employeeDataService.GetEmployee(new Employee {Id = 1}).Result;
            viewModel.LoadData();

            following = viewModel.Employees;

            //assert
            CollectionAssert.AreEqual(expectedEmployees, following);
        }
    }
}