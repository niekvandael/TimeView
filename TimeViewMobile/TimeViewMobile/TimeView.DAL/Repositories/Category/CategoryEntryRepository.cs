using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.DAL.Repositories.Category
{
    public class CategoryEntryRepository : ICategoryEntryRepository
    {
        private readonly string baseAddress = "http://timeview.azurewebsites.net/";

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

                    if (response.IsSuccessStatusCode) {
                        return true;
                    }
                    return false;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public bool DeleteCategoryEntry(CategoryEntry categoryEntry)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryEntry> UpdateCategoryEntry(CategoryEntry categoryEntry)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICategoryEntryRepository.DeleteCategoryEntry(CategoryEntry categoryEntry)
        {
            throw new NotImplementedException();
        }

        async Task<CategoryEntry[]> ICategoryEntryRepository.GetCategoryEntries(int categoryId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "api/CategoryEntries?CategoryId=" + categoryId;

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<CategoryEntry[]>();
                    return result;
                }
            }

            return null;
        }
    }
}