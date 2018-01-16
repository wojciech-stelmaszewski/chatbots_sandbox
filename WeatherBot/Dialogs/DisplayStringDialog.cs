using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class DisplayStringDialog : IDialog<string>
    {
        private readonly string _string;

        public DisplayStringDialog(string @string)
        {
            _string = @string;
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync(_string);
            context.Done<string>(null);
        }
    }
}