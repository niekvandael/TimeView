using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.DAL.Repositories.Schedule
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly string baseAddress = "http://timeview.azurewebsites.net/";

        async void IScheduleRepository.SaveSchedules(List<data.Schedule> schedules, Func<bool, bool> callback)
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

        async void IScheduleRepository.CreateSchedule(TimeView.data.Schedule schedule)
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

        async Task<TimeView.data.Schedule[]> IScheduleRepository.GetScheduleForEmployee(int employeeId)
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
                    var result = await response.Content.ReadAsAsync<TimeView.data.Schedule[]>();
                    return result;
                }
            }

            return null;
        }

        async void IScheduleRepository.UpdateSchedule(TimeView.data.Schedule schedule)
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