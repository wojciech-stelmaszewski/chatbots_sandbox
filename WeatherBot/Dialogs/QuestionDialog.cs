using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class QuestionDialog : IDialog<string>
    {
        private readonly string _messageText;

        public QuestionDialog(string messageText)
        {
            _messageText = messageText;
        }

        public async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();
            message.Text = _messageText;

            await context.PostAsync(message);
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;
            context.Done<string>(activity.Text ?? string.Empty);
        }
    }
}