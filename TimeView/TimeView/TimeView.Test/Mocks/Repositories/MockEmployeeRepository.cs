using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.domain;

namespace TimeView.Test.Mocks
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        public Task<Employee> getEmployee(int employeeId)
        {
            Employee employee = this.GetOneEmployee();

            return Task.FromResult(employee);
        }

        public Task<Employee> getEmployee(string username, string password)
        {
            Employee employee = null;

            if (username == "niek" && password == "niek")
            {
                employee = this.GetOneEmployee();
            }

            return Task.FromResult(employee);
        }

        private Employee GetOneEmployee() {
            Employee following1 = new Employee
            {
                Id = 2,
                Name = "Chrissy",
                Username = "chrissy",
                CompanyId = 2,
                Password = "chrissy",
            };

            Employee employee = new Employee
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
