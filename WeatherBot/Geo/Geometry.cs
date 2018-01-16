using System;
using Newtonsoft.Json;

namespace WeatherBot.Geo
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class Geometry
    {
        [JsonProperty("location")]
        public LatLng Location { get; set; }

        [JsonProperty("location_type")]
        public string LocationType { get; set; }
    }
}