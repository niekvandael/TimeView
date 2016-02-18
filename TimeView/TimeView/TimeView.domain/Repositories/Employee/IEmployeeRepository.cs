using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.domain
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployee(string username, string password);
        Task<Employee> GetEmployee(int employeeId);
    }
}