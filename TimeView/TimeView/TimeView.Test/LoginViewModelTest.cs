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
    public class LoginViewModelTest
    {
        private IEmployeeDataService _employeeDataService;
        private IViewDialog _loginViewDialog;
        private IViewDialog _scheduleListViewDialog;

        private LoginViewModel GetViewModel()
        {
            return new LoginViewModel(_employeeDataService, _scheduleListViewDialog, _loginViewDialog);
        }

        [TestInitialize]
        public void Init()
        {
            _employeeDataService = new MockEmployeeDataService();
            _scheduleListViewDialog = new MockScheduleListViewDialog();
            _loginViewDialog = new MockLoginViewDialog();
        }

        [TestMethod]
        public void LoginSuccess()
        {
            //Arrange
            var expectedEmployee = _employeeDataService.GetEmployee(new Employee {Id = 1}).Result;

            //act
            var viewModel = GetViewModel();
            viewModel.DoLogin("niek", "niek");

            //assert
            Assert.AreEqual(viewModel.Employee, expectedEmployee);
        }

        [TestMethod]
        public void LoginFail()
        {
            //Arrange
            var expectedEmployee = new Employee();

            //act
            var viewModel = GetViewModel();
            viewModel.DoLogin("niek", "fout");

            //assert
            Assert.AreEqual(viewModel.Employee, expectedEmployee);
        }
    }
}