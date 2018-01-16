using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hello!");
            context.Wait(MessageReceivedAsync);
        }
        
        private Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(DialogRootBuilder.BuildRootDialog(), MessageReceivedAsync);
            return Task.CompletedTask;
        }
    }
}