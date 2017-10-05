using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Net.Http;

namespace SampleBot.Dialogs
{
    [Serializable]
    public class MathDialog : IDialog<object>
    {
        public int number1 { get; set; }
        public string finalmessage;
        public  Task StartAsync(IDialogContext context)
        {
            
            context.Wait(Home);
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
        private async Task Home(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var cmts = await argument;
            if (cmts.Text.ToLower().Equals("yes", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync($"Do You Want perform Add or Minus Calculation Function?");
                context.Wait(GetFisrNum);
            }
            else if (cmts.Text.ToLower().Equals("no", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync("Ok Bye");
            }
            else {
                await context.PostAsync("I can't understand!!");
            }
        }
        private async Task GetFisrNum(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {                                
                var message = await argument;
                finalmessage = message.Text;
                await context.PostAsync($"Enter first Numbers");
                context.Wait(GetSecdNum);
            
        }
        private async Task GetSecdNum(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var number = await argument;
            this.number1 =int.Parse(number.Text);
            await context.PostAsync($"Enter Secd Numbers");
            context.Wait(FinalResult);
        }
        private async Task FinalResult(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {            
            var number = await argument;
            var number2 = int.Parse(number.Text);
            if (finalmessage.ToLower().Equals("add", StringComparison.InvariantCultureIgnoreCase))
            {
                await context.PostAsync($"Your Final Result is:{this.number1}+{number2}={this.number1 + number2}");
                context.Wait(Welcomemessage);
            } else if (finalmessage.ToLower().Equals("minus",StringComparison.InvariantCultureIgnoreCase))

            {
                await context.PostAsync($"Your Final Result is:{this.number1}-{number2}={this.number1 - number2}");
                context.Wait(Welcomemessage);

            }
            
           
        }
        private async Task Welcomemessage(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
                var cmts = await argument;
            
                await context.PostAsync($"Do you want again run calculation function" + Environment.NewLine + "Type " + "**Yes**" + "or" + "**No**");
                context.Wait(Home);
           
        }
        public async Task botisalive(IDialogContext context)
        {
            IMessageActivity message = Activity.CreateMessageActivity();
            if (message.Type == ActivityTypes.Ping)
            {

                await context.PostAsync($"Bot is Alive");
            }
            else
            {
                await context.PostAsync($" Bot is not alive");
            }



        }

    }
}