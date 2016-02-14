using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.domain;

namespace TimeView.wpf.Services
{
    class EmployeeDataRepository : IEmployeeDataService
    {
        IEmployeeRepository repository;

        public EmployeeDataRepository(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Employee> GetEmployee(string username, string password)
        {
            return await repository.getEmployee(username, password);
        }

        public async Task<Employee> GetEmployee(Employee employee)
        {
            return await repository.getEmployee(employee.Id);
        }
    }
}
