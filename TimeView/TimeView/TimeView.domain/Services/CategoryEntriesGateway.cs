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


    public class CategoryEntriesGateway
    {
        private static string baseAddress = "http://localhost:51150/";

        public static async System.Threading.Tasks.Task<CategoryEntry[]> getCategoryEntriesForCategory(int categoryId)
        {
            // Get the list via WebAPI
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                string url = "api/CategoryEntries?CategoryId=" + categoryId;
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    CategoryEntry[] CategoryEntrys = await response.Content.ReadAsAsync<CategoryEntry[]>();
                    return CategoryEntrys;
                }
            }

            return null;
        }

    

    }
}
