using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using TimeView.domain;

namespace TimeView.data.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private string baseAddress = "http://localhost:51150/";


        async System.Threading.Tasks.Task<Employee> IEmployeeRepository.getEmployee(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "api/Employees?username=" + username + "&password=" + password;

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    Employee result = await response.Content.ReadAsAsync<Employee>();
                    return result;
                }
            }

            return null;
        }

        async System.Threading.Tasks.Task<Employee> IEmployeeRepository.getEmployee(int employeeId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "api/Employees/" + employeeId;

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    Employee result = await response.Content.ReadAsAsync<Employee>();
                    return result;
                }
            }

            return null;
        }
    }
}
