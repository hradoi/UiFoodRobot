using Microsoft.Bot.Connector;
using System.Threading.Tasks;
using System.Web.Http;
using UiFoodRobot;

namespace UiFoodRobot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                // return our reply to the user
                return MessageParser.HandleMessage(message);
            }
            else
            {
                return MessageParser.HandleSystemMessage(message);
            }
        }
      
    }
}