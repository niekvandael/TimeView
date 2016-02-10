using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using TimeView.data;

namespace TimeView.data.Services
{


    public class CompaniesGateway
    {
        private static string baseAddress = "http://localhost:51150/";

        public static async System.Threading.Tasks.Task<Company[]> getCompanies()
        {
            // Get the list via WebAPI
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                string url = "api/Companies/";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Company[] Companys = await response.Content.ReadAsAsync<Company[]>();
                    return Companys;
                }
            }

            return null;
        }

    

    }
}
