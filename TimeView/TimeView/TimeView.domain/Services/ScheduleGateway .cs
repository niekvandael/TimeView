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

        public static async System.Threading.Tasks.Task<Schedule[]> getScheduleForEmployee(int employeeId)
        {
            // Get the list via WebAPI
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "api/Schedules?employeeId=" + employeeId;

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    Schedule[] schedules = await response.Content.ReadAsAsync<Schedule[]>();
                    return schedules;
                }
            }

            return null;
        }


    }
}
