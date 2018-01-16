using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using WeatherBot.Faq;
using WeatherBot.Services.Wit;

namespace WeatherBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private const decimal ConfidenceBenchmark = 0.9M;
        private readonly WitService _witService = new WitService();

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                var connectorClient = new ConnectorClient(new Uri(activity.ServiceUrl));

                var faqResponse = GetFaqResponse(activity.Text);
                var reply = activity.CreateReply(faqResponse);
                await connectorClient.Conversations.ReplyToActivityAsync(reply);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private string GetFaqResponse(string message)
        {
            var witResponse = _witService.GetResponse(message);
            WitIntent intent = witResponse.entities?.intent?.FirstOrDefault(x => x.confidence >= ConfidenceBenchmark);
            if (intent != null)
            {
                return MicrosoftBotFrameworkFaq.FindAnAnswer(intent.value);
            }
            return "Sorry, I didn't understand the question.";
        }
    }
}