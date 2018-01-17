using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WeatherBot.Weather;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class UseExistingLocationDialog : YesNoDialog
    {
        public UseExistingLocationDialog() : base()
        {
        }

        public override async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();

            WeatherLocation existingLocation = null;
            try
            {
                existingLocation = context.UserData.GetValueOrDefault<WeatherLocation>(WeatherBot.Storage.Location.UserDataLocationKey);
            }
            catch (Exception ex)
            {
                // Do nothing
            }
            
            if (existingLocation != null)
            {
                _message = $"Your current location is {existingLocation.Name}. Would you like to use this location?";
                await base.StartAsync(context);
            }
            else
            {
                context.Done<bool?>(false);
            }
        }
    }
}