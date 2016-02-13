using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.domain
{
    public interface IEmployeeRepository
    {
        System.Threading.Tasks.Task<Employee> getEmployee(string username, string password);
    }
}
