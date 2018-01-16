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

        private readonly HttpClient httpClient = new HttpClient();

        public void Dispose()
        {
            httpClient.Dispose();
        }

        public async Task<OpenWeatherResponse> GetWeatherAsync(double latitude, double longitude)
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={ApiId}";
            var resonse = await httpClient.GetAsync(url);
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