using System.Collections.Generic;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.domain;

namespace TimeView.Test.Mocks
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        public Task<Employee> GetEmployee(int employeeId)
        {
            var employee = GetOneEmployee();

            return Task.FromResult(employee);
        }

        public Task<Employee> GetEmployee(string username, string password)
        {
            Employee employee = null;

            if (username == "niek" && password == "niek")
            {
                employee = GetOneEmployee();
            }

            return Task.FromResult(employee);
        }

        private Employee GetOneEmployee()
        {
            var following1 = new Employee
            {
                Id = 2,
                Name = "Chrissy",
                Username = "chrissy",
                CompanyId = 2,
                Password = "chrissy"
            };

            var employee = new Employee
            {
                Id = 1,
                Name = "Niek",
                Username = "niek",
                CompanyId = 1,
                Password = "niek",
                Following = new List<Employee>()
            };
            employee.Following.Add(following1);

            return employee;
        }
    }
}