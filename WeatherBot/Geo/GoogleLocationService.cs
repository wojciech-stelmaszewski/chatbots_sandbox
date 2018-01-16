using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherBot.Geo
{
    public class GoogleLocationService
    {
        public async Task<GeoLocation> GetLocationAsync(GeocodingRequest request)
        {
            using (var client = new HttpClient())
            {
                var url = "https://maps.google.com/maps/api/geocode/" + request.ToUri();
                var resonse = await client.GetAsync(url);

                if (resonse.StatusCode == HttpStatusCode.OK)
                {
                    var str = await resonse.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<GeocodeResponse>(str);

                    var result = response.Results.FirstOrDefault();

                    if (result != null)
                        return new GeoLocation
                        {
                            Latitude = result.Geometry.Location.Lat,
                            Longitude = result.Geometry.Location.Lng
                        };
                }

                return null;
            }
        }
    }
}
