using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.DAL.Repositories.Category
{
    public class CategoryEntryRepository : ICategoryEntryRepository
    {
        private readonly string baseAddress = "https://timeview.azurewebsites.net/";

        public Task<CategoryEntry> CreateCategoryEntry(CategoryEntry categoryEntry)
        {
            throw new NotImplementedException();
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