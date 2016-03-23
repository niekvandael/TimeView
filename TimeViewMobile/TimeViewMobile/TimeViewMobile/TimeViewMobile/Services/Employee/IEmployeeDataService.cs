using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.wpf.Services
{
    public interface IEmployeeDataService
    {
        Task<Employee> GetEmployee(string username, string password);
        Task<Employee> GetEmployee(Employee employee);
        Task<bool> CreateEmployee(Employee employee);
        Task<bool> UpdateEmployee(Employee employee);
       
    }
}