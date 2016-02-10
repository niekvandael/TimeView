using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace TimeView.data.Services
{


    public class EmployeesGateway
    {
        private static string baseAddress = "http://localhost:51150/";

        public static async System.Threading.Tasks.Task<bool> getEmployee(int id)
        {
            // Get the list via WebAPI
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                string url = "api/Employees/" + id;
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    bool success = await response.Content.ReadAsAsync<bool>();
                    return success;
                }
            }

            return false;
        }

        public static async System.Threading.Tasks.Task<Employee[]> getEmployees()
        {
            // Get the list via WebAPI
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                string url = "api/Employees/";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Employee[] employees = await response.Content.ReadAsAsync<Employee[]>();
                    return employees;
                }
            }

            return null;
        }

        public static async System.Threading.Tasks.Task<Employee> login(Employee employee)
        {
            // Get the list via WebAPI
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                string url = "api/Employees?username=" + employee.Username + "&password=" + employee.Password;
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    Employee _empl = await response.Content.ReadAsAsync<Employee>();
                    return _empl;
                }
            }

            return null;
        }

    }
}
