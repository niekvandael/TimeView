using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.DAL.Repositories.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string baseAddress = "http://timeview.azurewebsites.net/";


        async Task<TimeView.data.Employee> IEmployeeRepository.GetEmployee(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "api/Employees?username=" + username + "&password=" + password;

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<TimeView.data.Employee>();
                    return result;
                }
            }

            return null;
        }

        async Task<TimeView.data.Employee> IEmployeeRepository.GetEmployee(int employeeId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "api/Employees/" + employeeId;

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<TimeView.data.Employee>();
                    return result;
                }
            }

            return null;
        }

        async Task<data.Employee> IEmployeeRepository.CreateEmployee(data.Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var url = "api/Employees/post";
                HttpResponseMessage response = null;

                try
                {
                    employee.Id = 0;
                    employee.CompanyId = 1;

                    string json = JsonConvert.SerializeObject(employee);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(url, content);
                }
                catch (Exception e)
                {
                    return null;
                }
                return null; ;
            }
        }
    }
}