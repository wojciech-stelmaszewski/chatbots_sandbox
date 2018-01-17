using System;
using System.Linq;
using Microsoft.Bot.Builder.Dialogs;
using WeatherBot.Geo;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public static class DialogRootBuilder
    {
        private const string GoogleLocationServiceApiKey = "AIzaSyDHU9LNBAOCMw3k0jLuArFMw4YoPh7d-z0";
        private const string Language = "en-GB";

        private const string Weather = "Weather";
        private const string Bots = "Bots";
        private const string SomethingElse = "Something else";

        public static IDialog<object> BuildRootDialog()
        {
            var options = new string[] { Weather, Bots, SomethingElse };

            return
                (from option in new OptionsDialog("What are you interested in?", options) select option).Switch(
                    // If the user chose Bots
                    new Case<string, IDialog<object>>(x => Bots.Equals(x), (_, __) =>
                        from questionToUser in new DisplayStringDialog("What do you want to know about Bots?")
                        from intent in new WitIntentValueDialog("E6DFDKGS4STETYSJ7DVS6YASQFZNKTPX")
                        from answer in new MsBotFaqAnswerDialog(intent)
                        from result in new DisplayStringDialog(answer)
                        select result),

                     // If the user chose Weather
                     new Case<string, IDialog<object>>(x => Weather.Equals(x), (_, __) =>
                        (from geoLocation in new GeoLocationDialog("What is your location?", GoogleLocationServiceApiKey, Language) select geoLocation).Switch(
                             new Case<GeoLocation, IDialog<object>>(g => g != null, (x, geo) =>
                             from weatherString in new WeatherDialog(geo.Latitude, geo.Longitude)
                             from result in new DisplayStringDialog(weatherString)
                             select result),
                            new Case<GeoLocation, IDialog<object>>(g => g == null, (x, xx) =>
                             from result in new DisplayStringDialog("Sorry - I don't know where that is!")
                             select result)
                            ).Unwrap()),

                     // If the user chose something else
                     new Case<string, IDialog<object>>(x => SomethingElse.Equals(x), (_, __) =>
                        from result in new DisplayStringDialog("I'm sorry to hear that.....What's wrong with Weather or Bots!")
                        select result),

                    // Invalid response
                    new DefaultCase<string, IDialog<object>>((_, __) =>
                        from result in new DisplayStringDialog("What was that!?")
                        select result)).Unwrap();
        }
    }
}