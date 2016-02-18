using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TimeView.domain;

namespace TimeView.data.Services
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly string baseAddress = "http://localhost:51150/";

        async void IScheduleRepository.SaveSchedules(List<Schedule> schedules, Func<bool, bool> callback)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var url = "api/Schedules/post";
                var response = await client.PostAsJsonAsync(url, schedules);

                callback(response.IsSuccessStatusCode);
            }
        }

        async void IScheduleRepository.CreateSchedule(Schedule schedule)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var url = "api/Schedules/post";
                await client.PostAsJsonAsync(url, schedule);
            }
        }

        async Task<Schedule[]> IScheduleRepository.GetScheduleForEmployee(int employeeId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "api/Schedules?EmployeeId=" + employeeId;

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Schedule[]>();
                    return result;
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


                var url = "api/Schedules/put";
                await client.PutAsJsonAsync(url, schedule);
            }
        }
    }
}