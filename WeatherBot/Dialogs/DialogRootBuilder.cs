using System;
using System.Linq;
using Microsoft.Bot.Builder.Dialogs;

namespace WeatherBot.Dialogs
{
    [Serializable]
    public static class DialogRootBuilder
    {
        public static IDialog<object> BuildRootDialog()
        {
            return
                (from boolean in new YesNoDialog() select boolean).Switch(
                        new Case<bool?, IDialog<object>>(x => x.HasValue && x.Value, (_, __) =>
                            from questionToUser in new DisplayStringDialog("What do you want to know?")
                            from intent in new WitIntentValueDialog("E6DFDKGS4STETYSJ7DVS6YASQFZNKTPX")
                            from answer in new MsBotFaqAnswerDialog(intent)
                            from result in new DisplayStringDialog(answer)
                            select result),
                        new Case<bool?, IDialog<object>>(x => x.HasValue && !x.Value, (_, __) => 
                            from result in new DisplayStringDialog("I'm sorry to hear that.")
                            select result),
                        new DefaultCase<bool?, IDialog<object>>((_, __) => 
                            from result in new DisplayStringDialog("What was that!?")
                            select result)).Unwrap();
;
        }
    }
}