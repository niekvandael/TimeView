using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
    }
}