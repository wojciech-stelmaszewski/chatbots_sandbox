using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;
using WeatherBot.Faq;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public class MsBotFaqAnswerDialog : IDialog<string>
    {
        public readonly string _intent;

        public MsBotFaqAnswerDialog(string intent)
        {
            _intent = intent;
        }

        public Task StartAsync(IDialogContext context)
        {
            var answer = !String.IsNullOrEmpty(_intent) ? MicrosoftBotFrameworkFaq.FindAnAnswer(_intent) : "Sorry, I didn't understand the question.";
            context.Done(answer);
            return Task.CompletedTask;
        }
    }
}