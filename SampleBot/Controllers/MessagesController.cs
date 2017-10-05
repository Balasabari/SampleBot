using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading;
using System.Linq;

namespace SampleBot
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
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                //Activity reply = activity.CreateReply();
                //reply.Type = ActivityTypes.Typing;
                //reply.Text = null;
                //await connector.Conversations.ReplyToActivityAsync(reply);

                // int milliseconds = 1000;
                // Thread.Sleep(milliseconds);                
                //if (activity.Text.ToLower().Equals("yes", StringComparison.InvariantCultureIgnoreCase))
                //{
                    await Conversation.SendAsync(activity, () => new Dialogs.MathDialog());
                //}
                
               
                //Activity reply = activity.CreateReply($"Welcome");
                // var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                //await connector.Conversations.ReplyToActivityAsync(reply);
                ////await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());

            }
            else
            {
               HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private async Task<Activity> HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                if (message.MembersAdded.Any(m => m.Id == message.Recipient.Id))
                {
                    var connector = new ConnectorClient(new Uri(message.ServiceUrl));

                   var response = message.CreateReply();
                    response.Text = "Hi! My Name is sample bot"+Environment.NewLine+"Do you want run calculation function"+ Environment.NewLine+"Type " +"**Yes**"+"or"+"**No**";                                    

                    await connector.Conversations.ReplyToActivityAsync(response);
               }

               
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {

            }

            return null;
        }

        
    }
}