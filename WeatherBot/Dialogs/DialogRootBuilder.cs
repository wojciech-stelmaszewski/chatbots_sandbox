using System;
using System.Linq;
using Microsoft.Bot.Builder.Dialogs;
using WeatherBot.Dialogs;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public static class DialogRootBuilder
    {
        public static IDialog<object> BuildRootDialog()
        {
            return from intent in new WitIntentValueDialog("E6DFDKGS4STETYSJ7DVS6YASQFZNKTPX")
                   from answer in new MsBotFaqAnswerDialog(intent)
                   from result in new DisplayStringDialog(answer)
                   select result;
        }
    }
}