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

        async Task<CategoryEntry[]> ICategoryEntryRepository.GetCategoryEntriesForCompany(int companyId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "api/CategoryEntries?CompanyId=" + companyId;

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