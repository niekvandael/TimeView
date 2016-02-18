using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.wpf.Services;

namespace TimeView.Test.Mocks
{
    class MockEmployeeDataService : IEmployeeDataService
    {
        private MockEmployeeRepository repository = new MockEmployeeRepository();

        public Task<Employee> GetEmployee(Employee employee)
        {
            return repository.getEmployee(employee.Id);
        }

        public Task<Employee> GetEmployee(string username, string password)
        {
            return repository.getEmployee(username, password);
        }
    }
}
