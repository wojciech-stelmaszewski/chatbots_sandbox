using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class OptionsDialog : IDialog<string>
    {
        private readonly string _messageText;
        private readonly IEnumerable<string> _options;

        public OptionsDialog(string messageText, IEnumerable<string> options)
        {
            _messageText = messageText;
            _options = options;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();
            message.Text = _messageText;
            message.SuggestedActions = new SuggestedActions
            {
                Actions = _options.Select(option => new CardAction
                {
                    Title = option,
                    Type = ActionTypes.PostBack,
                    Value = option
                }).ToList()
            };

            await context.PostAsync(message);
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;
            var matchedOption = _options.FirstOrDefault(o => o.Equals(activity.Text, StringComparison.InvariantCultureIgnoreCase));
            if (matchedOption != null)
            {
                context.Done<string>(matchedOption);
            }
            else
            {
                context.Done<string>(null);
            }
        }
    }
}