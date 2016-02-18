using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    public class LoginViewModelTest
    {
        private IEmployeeDataService employeeDataService;
        private IViewDialog scheduleListViewDialog;
        private IViewDialog loginViewDialog;

        private LoginViewModel GetViewModel() {
            return new LoginViewModel(this.employeeDataService, this.scheduleListViewDialog, this.loginViewDialog);
        }

        [TestInitialize]
        public void Init() {
            employeeDataService = new MockEmployeeDataService();
            scheduleListViewDialog = new MockScheduleListViewDialog();
            loginViewDialog = new MockLoginViewDialog();
        }

        [TestMethod]
        public void LoginSuccess()
        {
            //Arrange
            Employee expectedEmployee = employeeDataService.GetEmployee(new Employee { Id = 1 }).Result;

            //act
            var viewModel = GetViewModel();
            viewModel.doLogin("niek", "niek");

            //assert
            Assert.AreEqual(viewModel.Employee, expectedEmployee);
        }

        [TestMethod]
        public void LoginFail()
        {
            //Arrange
            Employee expectedEmployee = new Employee { };

            //act
            var viewModel = GetViewModel();
            viewModel.doLogin("niek", "fout");

            //assert
            Assert.AreEqual(viewModel.Employee, expectedEmployee);
        }
    }
}
