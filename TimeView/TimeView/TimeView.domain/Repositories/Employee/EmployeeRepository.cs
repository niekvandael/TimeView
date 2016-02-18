using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TimeView.domain;

namespace TimeView.data.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string baseAddress = "http://localhost:51150/";


        async Task<Employee> IEmployeeRepository.GetEmployee(string username, string password)
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
                    var result = await response.Content.ReadAsAsync<Employee>();
                    return result;
                }
            }

            return null;
        }

        async Task<Employee> IEmployeeRepository.GetEmployee(int employeeId)
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
                    var result = await response.Content.ReadAsAsync<Employee>();
                    return result;
                }
            }

            return null;
        }
    }
}