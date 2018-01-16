using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Call(DialogRootBuilder.BuildRootDialog(), ResumeAfter);
            return Task.CompletedTask;
        }

        private Task ResumeAfter(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(DialogRootBuilder.BuildRootDialog(), ResumeAfter);
            return Task.CompletedTask;
        }
    }
}