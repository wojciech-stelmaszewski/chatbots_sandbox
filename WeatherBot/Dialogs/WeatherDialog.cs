using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WeatherBot.Geo;
using WeatherBot.Weather;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class WeatherDialog : IDialog<string>
    {
        private readonly double _latitude;
        private readonly double _longitude;

        public WeatherDialog(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var weatherClient = new OpenWeatherClient("metric");
            var openWeatherResponse = await weatherClient.GetWeatherAsync(_latitude, _longitude);
            var weatherString = $"The temperature in {openWeatherResponse.Name} is {openWeatherResponse.Main.Temp}°C with lows of {openWeatherResponse.Main.TempMin}°C & highs of {openWeatherResponse.Main.TempMax}°C";
            context.Done(weatherString);
        }
    }
}