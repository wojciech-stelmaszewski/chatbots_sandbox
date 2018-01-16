using System;
using Newtonsoft.Json;

namespace WeatherBot.Geo
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class GeocodeResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }
}