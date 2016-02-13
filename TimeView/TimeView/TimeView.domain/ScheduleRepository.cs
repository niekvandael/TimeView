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
    public class ScheduleRepository : IScheduleRepository
    {

        private string baseAddress = "http://localhost:51150/";

        async void IScheduleRepository.CreateSchedule(Schedule schedule)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                string url = "api/Schedules/post";
                HttpResponseMessage response = await client.PostAsJsonAsync(url, schedule);
            }
        }

        async System.Threading.Tasks.Task<Schedule[]> IScheduleRepository.getScheduleForEmployee(int EmployeeId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "api/Schedules?EmployeeId=" + EmployeeId;

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    Schedule[] schedules = await response.Content.ReadAsAsync<Schedule[]>();
                    return schedules;
                }
            }

            return null;
        }

        async void IScheduleRepository.UpdateSchedule(Schedule schedule)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                string url = "api/Schedules/put";
                HttpResponseMessage response = await client.PutAsJsonAsync(url, schedule);
            }
        }
    }
}
