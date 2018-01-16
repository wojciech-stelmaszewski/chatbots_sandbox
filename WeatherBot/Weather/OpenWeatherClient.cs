using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherBot.Weather
{
    public class OpenWeatherClient : IDisposable
    {
        private const string ApiId = "98bd0e8c0180c992c4ccfb089c59af64";

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _units;

        public OpenWeatherClient()
        {
        }

        public OpenWeatherClient(string units)
        {
            _units = units;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public async Task<OpenWeatherResponse> GetWeatherAsync(double latitude, double longitude)
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={ApiId}";
            if (!string.IsNullOrEmpty(_units))
            {
                url = string.Concat(url, $"&units={_units}");
            }
            var resonse = await _httpClient.GetAsync(url);
            try
            {
                if (resonse.StatusCode == HttpStatusCode.OK)
                {
                    var openWeatherResponseString = await resonse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<OpenWeatherResponse>(openWeatherResponseString);
                }
            }
            catch
            {
            }

            return null;
        }
    }
}