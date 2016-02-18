using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.Test.Mocks;
using TimeView.Test.Mocks.Dialogs;
using TimeView.wpf.Dialogs;
using TimeView.wpf.Services;
using TimeView.wpf.ViewModel;

namespace TimeView.Test
{
    [TestClass]
    public class FollowingListViewModelTests
    {
        private IEmployeeDataService employeeDataService;
        private IViewDialog scheduleListViewDialog;

        private FollowingListViewModel GetViewModel()
        {
            return new FollowingListViewModel(this.employeeDataService, this.scheduleListViewDialog);
        }

        [TestInitialize]
        public void Init()
        {
            employeeDataService = new MockEmployeeDataService();
            scheduleListViewDialog = new MockScheduleListViewDialog();
        }

        [TestMethod]
        public void LoadAllFollowings()
        {
            //Arrange
            ObservableCollection<Employee> following = null;
            var expectedEmployees = employeeDataService.GetEmployee(new Employee { Id = 1 }).Result.Following;

            //act
            var viewModel = GetViewModel();
            viewModel.CurrentUser = employeeDataService.GetEmployee(new Employee { Id = 1 }).Result;
            viewModel.LoadData();

            following = viewModel.Employees;

            //assert
            CollectionAssert.AreEqual(expectedEmployees, following);
        }
    }
}
