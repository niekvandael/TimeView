using System.Threading.Tasks;
using TimeView.DAL.Repositories.Employee;
using TimeView.data;

namespace TimeView.wpf.Services
{
    internal class EmployeeDataService : IEmployeeDataService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeDataService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Employee> GetEmployee(string username, string password)
        {
            return await _repository.GetEmployee(username, password);
        }

        public async Task<Employee> GetEmployee(Employee employee)
        {
            return await _repository.GetEmployee(employee.Id);
        }
    }
}