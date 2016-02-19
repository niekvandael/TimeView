using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TimeView.domain;

namespace TimeView.data.Services
{
    public class OpenDataRepository : IOpenDataRepository
    {
        private readonly string baseAddress = "http://timeview.azurewebsites.net/";

        public async Task<Company[]> UpdateOpenData()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "api/OpenData/";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Company[]>();
                    return result;
                }
            }

            return null;
        }
    }
}