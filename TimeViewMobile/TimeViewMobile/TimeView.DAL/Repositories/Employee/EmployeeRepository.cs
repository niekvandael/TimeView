﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.DAL.Repositories.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string baseAddress = "https://timeview.azurewebsites.net/";


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

        async Task<bool> IEmployeeRepository.CreateEmployee(TimeView.data.Employee employee)
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
                    employee.Follower = new List<TimeView.data.Employee>();
                    employee.Following = new List<TimeView.data.Employee>();

                    string json = JsonConvert.SerializeObject(employee);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(url, content);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateEmployee(data.Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var url = "api/Employees/put";
                HttpResponseMessage response = null;

                try
                {
                    string json = JsonConvert.SerializeObject(employee);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PutAsync(url, content);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}