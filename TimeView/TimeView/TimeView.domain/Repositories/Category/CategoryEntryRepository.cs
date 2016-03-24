using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TimeView.domain;

namespace TimeView.data.Services
{
    public class CategoryEntryRepository : ICategoryEntryRepository
    {
        private readonly string baseAddress = "http://localhost:51150/";

        async Task<CategoryEntry[]> ICategoryEntryRepository.GetCategoryEntriesForCompany(int companyId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "api/CategoryEntries?EmployeeId=" + companyId;

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<CategoryEntry[]>();
                    return result;
                }
            }

            return null;
        }


        public async Task<bool> CreateCategoryEntry(CategoryEntry categoryEntry)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var url = "api/CategoryEntries/post";
                HttpResponseMessage response = null;

                try
                {

                    string json = JsonConvert.SerializeObject(categoryEntry);
 
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


    }
}