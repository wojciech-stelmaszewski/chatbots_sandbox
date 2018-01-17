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
    public class ReuseLocationDialog : IDialog<bool>
    {
        public ReuseLocationDialog() { }

        public async Task StartAsync(IDialogContext context)
        {
            var location = context.UserData.GetValueOrDefault<string>(WeatherBot.Storage.Location.UserDataKey);
            if (string.IsNullOrWhiteSpace(location))
            {
                context.Done<bool>(false);
            }
            else
            {
                var message = context.MakeMessage();
                message.Text = $"Last time you asked for weather in the {location}, do you want to use this location again?";
                message.SuggestedActions = new SuggestedActions
                {
                    Actions = new List<CardAction>
                {
                    new CardAction
                    {
                        Title = $"Yes use {location}",
                        Type = ActionTypes.PostBack,
                        Value = true.ToString()
                    },
                    new CardAction
                    {
                        Title = "No I want to enter a new location",
                        Type = ActionTypes.PostBack,
                        Value = false.ToString()
                    }
                }
                };

                await context.PostAsync(message);
                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;
            if (bool.TryParse(activity.Text ?? String.Empty, out bool answer))
            {
                context.Done<bool?>(answer);
            }
            else
            {
                context.Done<bool?>(null);
            }
        }
    }
}