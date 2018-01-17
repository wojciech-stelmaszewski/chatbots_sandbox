using Microsoft.Bot.Builder.ConnectorEx;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherBot.Facebook;
using WeatherBot.Geo;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class GeoLocationDialog : IDialog<GeoLocation>
    {
        private const string FacebookChannelId = "facebook";

        private readonly string _apiKey;
        private readonly string _language;
        private string _messageText;
        private bool _reuseSaved;

        public GeoLocationDialog(string messageText, string apiKey, string language, bool reuseSaved)
        {
            _messageText = messageText;
            _apiKey = apiKey;
            _language = language;
            _reuseSaved = reuseSaved;
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (_reuseSaved)
            {
                var location = context.UserData.GetValueOrDefault<string>(WeatherBot.Storage.Location.UserDataKey);
                var geoLocation = await ReuseAsync(location);
                context.Done<GeoLocation>(geoLocation);
            }
            else
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

            context.Done<GeoLocation>(geoLocation);
        }

        protected async Task<GeoLocation> ReuseAsync(string location)
        {
            var googleLocationService = new GoogleLocationService();
            var geoCodingRequest = new GeocodingRequest
            {
                Address = location ?? string.Empty,
                ApiKey = _apiKey,
                Language = _language
            };
            return await googleLocationService.GetLocationAsync(geoCodingRequest);
        }
    }
}