using System;
using System.Globalization;
using System.Web;

namespace WeatherBot.Geo
{
    public class GeocodingRequest
    {
        public GeoLocation GeoLocation { get; set; }
        public string Address { get; set; }
        public string Language { get; set; }
        public string ApiKey { get; set; }

        public Uri ToUri()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            if (GeoLocation != null)
            {
                queryString["latlng"] = $"{GeoLocation.Latitude.ToString(CultureInfo.InvariantCulture)},{GeoLocation.Longitude.ToString(CultureInfo.InvariantCulture)}";

            }

            if (!String.IsNullOrWhiteSpace(Address))
            {
                queryString["address"] = Address;
            }

            if (!String.IsNullOrWhiteSpace(Language))
            {
                queryString["language"] = Language;
            }

            if (!String.IsNullOrWhiteSpace(ApiKey))
            {
                queryString["key"] = ApiKey;
            }

            var url = "json?" + queryString.ToString();

            return new Uri(url, UriKind.Relative);
        }
    }
}
