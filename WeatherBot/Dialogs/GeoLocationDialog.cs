using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WeatherBot.Geo;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class GeoLocationDialog : IDialog<GeoLocation>
    {
        private readonly string _location;
        private readonly string _apiKey;
        private readonly string _language;
        
        public GeoLocationDialog(string location, string apiKey, string language)
        {
            _location = location;
            _apiKey = apiKey;
            _language = language;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var googleLocationService = new GoogleLocationService();
            var geoRequest = new GeocodingRequest()
            {
                Address = _location,
                ApiKey = _apiKey,
                Language = _language
            };
            var geoLocation = await googleLocationService.GetLocationAsync(geoRequest);
            context.Done(geoLocation);
        }
    }
}