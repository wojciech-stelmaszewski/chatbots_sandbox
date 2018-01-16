using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using WeatherBot.Services.Wit;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class WitIntentValueDialog : IDialog<string>
    {
        private const decimal ConfidenceBenchmark = 0.9M;

        public readonly string _witAccessToken;

        public WitIntentValueDialog(string witAccessToken)
        {
            _witAccessToken = witAccessToken;
        }

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as IMessageActivity;

            var witService = new WitService(_witAccessToken);
            var witResponse = witService.GetResponse(activity.Text ?? String.Empty);
            WitIntent intent = witResponse.entities?.intent?.OrderByDescending(x => x.confidence)?.FirstOrDefault(x => x.confidence >= ConfidenceBenchmark);

            context.Done(intent?.value);
        }
    }
}