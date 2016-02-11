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


    public class ScheduleGateway
    {
        private static string baseAddress = "http://localhost:51150/";

        public static async System.Threading.Tasks.Task<Schedule[]> getScheduleForEmployee(int EmployeeId)
        {
            // Get the list via WebAPI
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

        public static async System.Threading.Tasks.Task<HttpResponseMessage> putSchedule(Schedule schedule)
        {
            // Get the list via WebAPI
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                string url = "api/Schedules/put";
                HttpResponseMessage response = await client.PutAsJsonAsync(url, schedule);

                return response;
            }
        }

        public static async System.Threading.Tasks.Task<HttpResponseMessage> postSchedule(Schedule schedule)
        {
            // Get the list via WebAPI
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                string url = "api/Schedules/post";
                HttpResponseMessage response = await client.PostAsJsonAsync(url, schedule);

                return response;
            }
        }

        public static async void addOrUpdate(Schedule[] Schedules) {
            foreach (Schedule schedule in Schedules)
            {
                if (schedule.Id == -1)
                {
                    await postSchedule(schedule);
                }
                else {
                    await putSchedule(schedule);
                }
            }
        }
    }
}
