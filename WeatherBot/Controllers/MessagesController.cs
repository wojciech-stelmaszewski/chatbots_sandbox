using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using WeatherBot.Dialogs;

namespace WeatherBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new RootDialog());
            }
            if (activity.Type == ActivityTypes.DeleteUserData)
            {
                var stateClient = activity.GetStateClient();
                stateClient.BotState.DeleteStateForUser(activity.ChannelId, activity.From.Id);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
    }
}