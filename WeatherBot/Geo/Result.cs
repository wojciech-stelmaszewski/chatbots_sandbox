using System;
using Newtonsoft.Json;

namespace WeatherBot.Geo
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class Result
    {
        [JsonProperty("types")]
        public string[] Types { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("address_components")]
        public AddressComponent[] AddressComponents { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("partial_match")]
        public bool PartialMatch { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
    }
}