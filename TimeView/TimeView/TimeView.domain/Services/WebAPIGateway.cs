using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace TimeView.data.Services
{


    public class WebAPIGateway
    {
        private static string baseAddress = "http://localhost:51150/";

        public static async System.Threading.Tasks.Task<Employee> getProduct(int? id)
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
                    Employee employee = await response.Content.ReadAsAsync<Employee>();
                    return employee;
                }
            }

            return null;
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



    }

}
