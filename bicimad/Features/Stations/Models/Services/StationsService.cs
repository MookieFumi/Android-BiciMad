using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace bicimad.Models.Services
{
    public class StationsService
    {
        private const string BaseUrl = "https://rbdata.emtmadrid.es:8443/BiciMad/";

        public async Task<List<Station>> GetStationsAsync()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();

            var response = await httpClient.GetAsync("get_stations/WEB.SERV.mookiefumi@outlook.com/AAF16DF9-549F-4EF5-A52A-DCC999DEC42F")
                                      .ConfigureAwait(false);

            var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var stations = JsonConvert.DeserializeObject<BiciMadResult>(SanitizeJson(responseText)).Data.Stations;

            return stations.OrderBy(p => p.Address).ToList();
        }

        private string SanitizeJson(string responseText)
        {
            //TODO: ¿Por qué tengo que hacer esta cerdada? No lo entiendo.
            return responseText.Replace("\\", "").Replace("\"{", "{").Replace("}\"", "}");
        }
    }
}
