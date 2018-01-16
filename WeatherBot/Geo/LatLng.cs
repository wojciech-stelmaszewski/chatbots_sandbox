using System;
using Newtonsoft.Json;

namespace WeatherBot.Geo
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class LatLng
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }
}