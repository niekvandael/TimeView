using System.Threading.Tasks;
using TimeView.data;
using TimeView.wpf.Services;

namespace TimeView.Test.Mocks.DataServices
{
    internal class MockEmployeeDataService : IEmployeeDataService
    {
        private readonly MockEmployeeRepository _repository = new MockEmployeeRepository();

        public Task<Employee> GetEmployee(Employee employee)
        {
            return _repository.GetEmployee(employee.Id);
        }

        public Task<Employee> GetEmployee(string username, string password)
        {
            return _repository.GetEmployee(username, password);
        }
    }
}