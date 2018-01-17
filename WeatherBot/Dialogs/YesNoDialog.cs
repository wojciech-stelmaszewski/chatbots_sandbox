using Microsoft.Bot.Builder.Dialogs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using System;
using WeatherBot.Facebook;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class YesNoDialog : IDialog<bool?>
    {
        private const string FacebookChannelId = "facebook";
        protected string _message;

        private const string YesTitle = "Yes";
        private const string YesValue = "true";

        private const string NoTitle = "No";
        private const string NoValue = "false";

        public YesNoDialog()
        {
        }

        public YesNoDialog(string message)
        {
            _message = message;
        }

        public virtual async Task StartAsync(IDialogContext context)
        {
            var yesAction = new CardAction
            {
                Title = YesTitle,
                Type = ActionTypes.PostBack,
                Value = YesValue
            };
            var noAction = new CardAction
            {
                Title = NoTitle,
                Type = ActionTypes.PostBack,
                Value = NoValue
            };

            var message = context.MakeMessage();
            message.Text = _message;
            message.SuggestedActions = new SuggestedActions
            {
                Actions = new List<CardAction> { yesAction, noAction }
            };

            await context.PostAsync(message);
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;
            bool? answer = false;
            if (activity.Text.Equals(YesTitle, StringComparison.InvariantCultureIgnoreCase) ||
                activity.Text.Equals(YesValue, StringComparison.InvariantCultureIgnoreCase))
            {
                answer = true;
            }
            if (activity.Text.Equals(NoTitle, StringComparison.InvariantCultureIgnoreCase) ||
                activity.Text.Equals(NoValue, StringComparison.InvariantCultureIgnoreCase))
            {
                answer = false;
            }

            context.Done<bool?>(answer);
        }
    }
}