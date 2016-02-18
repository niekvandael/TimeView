using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.Test.Mocks;
using TimeView.Test.Mocks.DataServices;
using TimeView.Test.Mocks.Dialogs;
using TimeView.wpf.Dialogs;
using TimeView.wpf.Extensions;
using TimeView.wpf.Services;
using TimeView.wpf.ViewModel;

namespace TimeView.Test
{
    [TestClass]
    public class ScheduleListViewModelTests
    {
        private IEmployeeDataService employeeDataService;
        private IScheduleDataService scheduleDataService;
        private ICategoryEntryDataService categoryEntryDataService;

        private ScheduleListViewModel GetViewModel()
        {
            return new ScheduleListViewModel(this.scheduleDataService, this.categoryEntryDataService);
        }

        [TestInitialize]
        public void Init()
        {
            employeeDataService = new MockEmployeeDataService();
            scheduleDataService = new MockScheduleDataService();
            categoryEntryDataService = new MockCategoryEntryDataService();
        }

        [TestMethod]
        public void LoadAllSchedules()
        {
            //Arrange
            ObservableCollection<Schedule> schedules = null;
            var expectedSchedules = scheduleDataService.GetScheduleForEmployee(new Employee { Id = 1 });

            //act
            var viewModel = GetViewModel();
            viewModel.Employee = employeeDataService.GetEmployee(new Employee { Id = 1 }).Result;
            viewModel.LoadScheduleList();
            schedules = viewModel.Schedules;

            //assert
            CollectionAssert.AreEqual(schedules.ToObservableCollection(), expectedSchedules.Result.ToObservableCollection());
        }


        [TestMethod]
        public void LoadAllCategoryEntries()
        {
            //Arrange
            ObservableCollection<CategoryEntry> categoryEntries = null;
            var expectedCategoryEntries = categoryEntryDataService.GetCategoryEntriesForCompany(1);

            //act
            var viewModel = GetViewModel();
            viewModel.Employee = employeeDataService.GetEmployee(new Employee { Id = 1 }).Result;
            viewModel.LoadCategoryEntries();
            categoryEntries = viewModel.CategoryEntries;

            //assert
            CollectionAssert.AreEqual(categoryEntries.ToObservableCollection(), expectedCategoryEntries.Result.ToObservableCollection());
        }

        [TestMethod]
        public void getNextDayWithSchedule()
        {
            //Arrange
            DateTime expectedDate = DateTime.Now.AddDays(1);

            //act
            var viewModel = GetViewModel();
            viewModel.Employee = employeeDataService.GetEmployee(new Employee { Id = 1 }).Result;
            viewModel.LoadScheduleList();
            DateTime nextDay = viewModel.getNextDay();

            //assert
            Assert.AreEqual(expectedDate.Date, DateTime.Now.AddDays(1).Date);
        }

        [TestMethod]
        public void getNextDayWithoutSchedule()
        {
            //Arrange
            DateTime expectedDate = DateTime.Now.AddDays(1);

            //act
            var viewModel = GetViewModel();
            viewModel.Employee = employeeDataService.GetEmployee(new Employee { Id = 1 }).Result;
            DateTime nextDay = viewModel.getNextDay();

            //assert
            Assert.AreEqual(expectedDate.Date, DateTime.Now.AddDays(1).Date);
        }
    }
}
