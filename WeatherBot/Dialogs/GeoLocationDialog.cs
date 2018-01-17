using Microsoft.Bot.Builder.ConnectorEx;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherBot.Facebook;
using WeatherBot.Geo;
using WeatherBot.Weather;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class GeoLocationDialog : IDialog<GeoLocation>
    {
        private const string FacebookChannelId = "facebook";
        
        private readonly string _apiKey;
        private readonly string _language;
        private string _messageText;

        public GeoLocationDialog(string messageText, string apiKey, string language)
        {
            _messageText = messageText;
            _apiKey = apiKey;
            _language = language;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();

            if (message.ChannelId == FacebookChannelId)
            {
                message.ChannelData = new FacebookMessage(_messageText, new List<FacebookQuickReply>{
                        new FacebookQuickReply(FacebookQuickReply.ContentTypes.Location, default(string), default(string))});
            }
            else
            {
                message.Text = _messageText;
            }

            await context.PostAsync(message);
            context.Wait<IMessageActivity>(MessageReceivedAsync);
        }


        protected async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            GeoLocation geoLocation = null;
            var activity = await result;

            if (string.IsNullOrEmpty(activity.Text) && activity.ChannelId == FacebookChannelId)
            {
                var facebookLocation = (FacebookLocation)activity.ChannelData.ToObject<FacebookLocation>();
                double? latitude = facebookLocation?.message?.attachments?[0]?.payload?.coordinates?.lat;
                double? longitude = facebookLocation?.message?.attachments?[0]?.payload?.coordinates?.@long;

                if (latitude.HasValue && longitude.HasValue)
                {
                    geoLocation = new GeoLocation()
                    {
                        Latitude = latitude.Value,
                        Longitude = longitude.Value
                    };
                }
            }
            else
            {
                var googleLocationService = new GoogleLocationService();
                var geoCodingRequest = new GeocodingRequest
                {
                    Address = activity.Text ?? string.Empty,
                    ApiKey = _apiKey,
                    Language = _language
                };
                geoLocation = await googleLocationService.GetLocationAsync(geoCodingRequest);
            }

            if (geoLocation != null)
            {
                var weatherLocation = context.UserData.GetValueOrDefault<WeatherLocation>(WeatherBot.Storage.Location.UserDataLocationKey) ?? new WeatherLocation();
                weatherLocation.GeoLocation = geoLocation;
                context.UserData.SetValue(WeatherBot.Storage.Location.UserDataLocationKey, weatherLocation);
            }

            context.Done<GeoLocation>(geoLocation);
        }
    }
}