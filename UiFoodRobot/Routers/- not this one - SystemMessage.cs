using Microsoft.Bot.Connector;

namespace UiFoodRobot.Routers
{
    public class SystemMessageRouter
    {
        public static Message HandleMessage(Message message)
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
                Message reply = message.CreateReplyMessage("Hello!");
                return reply;
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
                return message.CreateReplyMessage("'Ello there!");
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