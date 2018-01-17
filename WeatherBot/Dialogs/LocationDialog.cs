using Microsoft.Bot.Builder.ConnectorEx;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class LocationDialog : QuestionDialog
    {
        public LocationDialog(string messageText) : base(messageText)
        {
        }

        public override async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();

            if (message.ChannelId == "facebook")
            {
                message.ChannelData = new FacebookMessage(_messageText, new List<FacebookQuickReply>{
                        new FacebookQuickReply(FacebookQuickReply.ContentTypes.Location, default(string), default(string))
                    });
            }
            else
            {
                message.Text = _messageText;
            }

            await context.PostAsync(message);
            context.Wait(MessageReceivedAsync);
        }
    }
}