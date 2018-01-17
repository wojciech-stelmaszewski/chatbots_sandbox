using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WeatherBot.Geo;
using WeatherBot.Storage;
using WeatherBot.Weather;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class WeatherDialog : IDialog<string>
    {
        private double _latitude;
        private double _longitude;

        public WeatherDialog()
        {
        }

        public WeatherDialog(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var weatherLocation = context.UserData.GetValueOrDefault<WeatherLocation>(WeatherBot.Storage.Location.UserDataLocationKey);
            if (weatherLocation?.GeoLocation != null)
            {
                _latitude = weatherLocation.GeoLocation.Latitude;
                _longitude = weatherLocation.GeoLocation.Longitude;
            }

            var weatherClient = new OpenWeatherClient("metric");
            var openWeatherResponse = await weatherClient.GetWeatherAsync(_latitude, _longitude);

            if (weatherLocation != null)
            {
                weatherLocation.Name = openWeatherResponse.Name;
                context.UserData.SetValue(WeatherBot.Storage.Location.UserDataLocationKey, weatherLocation);
            }

            var weatherString = $"The temperature in {openWeatherResponse.Name} is {openWeatherResponse.Main.Temp}°C with lows of {openWeatherResponse.Main.TempMin}°C & highs of {openWeatherResponse.Main.TempMax}°C";
            context.Done(weatherString);
        }
    }
}