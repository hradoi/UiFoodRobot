using Microsoft.Bot.Connector;
using System.Web.Http;
using System.Collections.Generic;

namespace UiFoodRobot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
         public Message Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                return MessageParser.HandleMessage(message);
            }
            else
            {
                return MessageParser.HandleSystemMessage(message);
            }
        }
    }
}