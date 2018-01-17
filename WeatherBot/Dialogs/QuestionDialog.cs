using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class QuestionDialog : IDialog<string>
    {
        protected readonly string _messageText;

        public QuestionDialog(string messageText)
        {
            _messageText = messageText;
        }

        public virtual async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();
            message.Text = _messageText;

            await context.PostAsync(message);
            context.Wait(MessageReceivedAsync);
        }

        protected async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;
            context.Done<string>(activity.Text ?? string.Empty);
        }
    }
}