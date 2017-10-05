using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Net.Http;

namespace SampleBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            //IMessageActivity message = Activity.CreateMessageActivity();
            //await context.PostAsync("You want to");
            //context.Wait(MessageReceivedAsync);
            context.Wait(samplemethod);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;                      
            int length = (activity.Text ?? string.Empty).Length;
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");
            context.Wait(MessageReceivedAsync);
        }
        private async Task samplemethod(IDialogContext context, IAwaitable<object> result)
        {
            var client = new HttpClient() { BaseAddress = new Uri("https://swc.cdn.skype.com/sdk/v1/sdk.min.js") };
            var activity = await result as Activity;
            int length = (activity.Text ?? string.Empty).Length;
            await context.PostAsync($"You Message {activity.Text} which was {length} characters");
            context.Wait(samplemethod);
        }
    }
}