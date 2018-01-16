using Newtonsoft.Json;

namespace WeatherBot.Weather
{
    public class OpenWeatherResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("weather")]
        public OpenWeatherWeatherResponse[] Weather { get; set; }

        [JsonProperty("main")]
        public OpenWeatherMainResponse Main { get; set; }

        [JsonProperty("cod")]
        public long Code { get; set; }
    }

    public class OpenWeatherWeatherResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class OpenWeatherMainResponse
    {
        [JsonProperty("Temp")]
        public double Temp { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("temp_min")]
        public string TempMin { get; set; }

        [JsonProperty("temp_max")]
        public string TempMax { get; set; }
    }
}