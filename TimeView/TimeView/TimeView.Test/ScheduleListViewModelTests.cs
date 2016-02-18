using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeView.data;
using TimeView.Test.Mocks.DataServices;
using TimeView.wpf.Extensions;
using TimeView.wpf.Services;
using TimeView.wpf.ViewModel;

namespace TimeView.Test
{
    [TestClass]
    public class ScheduleListViewModelTests
    {
        private ICategoryEntryDataService _categoryEntryDataService;
        private IEmployeeDataService _employeeDataService;
        private IScheduleDataService _scheduleDataService;

        private ScheduleListViewModel GetViewModel()
        {
            return new ScheduleListViewModel(_scheduleDataService, _categoryEntryDataService);
        }

        [TestInitialize]
        public void Init()
        {
            _employeeDataService = new MockEmployeeDataService();
            _scheduleDataService = new MockScheduleDataService();
            _categoryEntryDataService = new MockCategoryEntryDataService();
        }

        [TestMethod]
        public void LoadAllSchedules()
        {
            //Arrange
            ObservableCollection<Schedule> schedules;
            var expectedSchedules = _scheduleDataService.GetScheduleForEmployee(new Employee {Id = 1});

            //act
            var viewModel = GetViewModel();
            viewModel.Employee = _employeeDataService.GetEmployee(new Employee {Id = 1}).Result;
            viewModel.LoadScheduleList();
            schedules = viewModel.Schedules;

            //assert
            CollectionAssert.AreEqual(schedules.ToObservableCollection(),
                expectedSchedules.Result.ToObservableCollection());
        }


        [TestMethod]
        public void LoadAllCategoryEntries()
        {
            //Arrange
            ObservableCollection<CategoryEntry> categoryEntries;
            var expectedCategoryEntries = _categoryEntryDataService.GetCategoryEntriesForCompany(1);

            //act
            var viewModel = GetViewModel();
            viewModel.Employee = _employeeDataService.GetEmployee(new Employee {Id = 1}).Result;
            viewModel.LoadCategoryEntries();
            categoryEntries = viewModel.CategoryEntries;

            //assert
            CollectionAssert.AreEqual(categoryEntries.ToObservableCollection(),
                expectedCategoryEntries.Result.ToObservableCollection());
        }

        [TestMethod]
        public void GetNextDayWithSchedule()
        {
            //Arrange
            var expectedDate = DateTime.Now.AddDays(1);

            //act
            var viewModel = GetViewModel();
            viewModel.Employee = _employeeDataService.GetEmployee(new Employee {Id = 1}).Result;
            viewModel.LoadScheduleList();

            //assert
            Assert.AreEqual(expectedDate.Date, DateTime.Now.AddDays(1).Date);
        }

        [TestMethod]
        public void GetNextDayWithoutSchedule()
        {
            //Arrange
            var expectedDate = DateTime.Now.AddDays(1);

            //act
            var viewModel = GetViewModel();
            viewModel.Employee = _employeeDataService.GetEmployee(new Employee {Id = 1}).Result;

            //assert
            Assert.AreEqual(expectedDate.Date, DateTime.Now.AddDays(1).Date);
        }
    }
}