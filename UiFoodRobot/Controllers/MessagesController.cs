using Microsoft.Bot.Connector;
using System.Threading.Tasks;
using System.Web.Http;
using UiFoodRobot;

namespace MondayFoodRobot
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
                return HandleMessage(message);
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }
        private Message HandleMessage(Message m)
        {
            string rm = "";
            string text = m.Text;
            if (text == "/h" || text == "/help")
                rm = "Help message";
            else if (text == "/o" || text == "/order")
                rm = "What will it be?";
            else if (text == "/m" || text == "/menu")
            {
                rm = "Here's the menu:\n";
                rm += "1. No menu\n";
                rm += "2. You're fat!\n";
            }
            else rm = "No comprendes, senor!";

            return m.CreateReplyMessage(rm);
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
                Message reply = message.CreateReplyMessage("Hello! Type /h or /help for help, /m for menu, /o to order");
                return reply;
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
                return message.CreateReplyMessage("Type /h or /help for help, /m for menu, /o to order");
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
                return message.CreateReplyMessage("Hasta la vista, baby!");

            }
            else if (message.Type == "EndOfConversation")
            {
                return message.CreateReplyMessage("Buh bye!");
            }

            return null;
        }
    }
}