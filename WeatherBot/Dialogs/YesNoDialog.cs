using Microsoft.Bot.Builder.Dialogs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using System;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class YesNoDialog : IDialog<bool?>
    {
        protected string _message;

        public YesNoDialog()
        {
        }

        public YesNoDialog(string message)
        {
            _message = message;
        }

        public virtual async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();
            message.Text = _message;
            message.SuggestedActions = new SuggestedActions
            {
                Actions = new List<CardAction>
                {
                    new CardAction
                    {
                        Title = "Yes",
                        Type = ActionTypes.PostBack,
                        Value = true.ToString()
                    },
                    new CardAction
                    {
                        Title = "No",
                        Type = ActionTypes.PostBack,
                        Value = false.ToString()
                    }
                }
            };

            await context.PostAsync(message);
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;
            if(bool.TryParse(activity.Text ?? String.Empty, out bool answer))
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