using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using TimeView.domain;
using System.Threading.Tasks;

namespace TimeView.data.Services
{
    public class CategoryEntryRepository : ICategoryEntryRepository
    {

        private string baseAddress = "http://localhost:51150/";

        async System.Threading.Tasks.Task<CategoryEntry[]> ICategoryEntryRepository.getCategoryEntriesForCompany(int CompanyId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "api/CategoryEntries?EmployeeId=" + CompanyId;

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    CategoryEntry[] result = await response.Content.ReadAsAsync<CategoryEntry[]>();
                    return result;
                }
            }

            return null;
        }
    }
}