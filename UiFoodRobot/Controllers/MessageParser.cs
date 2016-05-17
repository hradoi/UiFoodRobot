using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Linq;

namespace UiFoodRobot
{
    internal class MessageParser
    {
        const string ErrMessage = "Sorry, I could not understand that. I need a command. Please try the keywords \"add\",\"remove\",\"display\",\"quit\".";
        static readonly List<string> commands = new List<string>() { "add","remove","show","quit"};
        public static Message HandleMessage(Message m)
        {
            
            string text = m.Text.Trim().ToLowerInvariant();
            string rm = "";

            if (string.IsNullOrWhiteSpace(text))
            {
                rm = ErrMessage;
                return m.CreateReplyMessage(rm);
            }

            // Look for the first white space
            var whiteSpaceIndex = text.IndexOf(' ');

            if (whiteSpaceIndex == -1)
            {
                rm = (commands.IndexOf(text) >= 0) ? ShowHelp(text) : ErrMessage;
                //rm += "Whitesapce";
                return m.CreateReplyMessage(rm);
            }

            // Extract the action & parameters
            string action = text.Substring(0, whiteSpaceIndex);
            string parameters = text.Substring(whiteSpaceIndex + 1);//.split();

            rm = (commands.IndexOf(action) >= 0) ? HandleCommand(action, parameters, m) : ErrMessage;
            //rm += "comand";
            return m.CreateReplyMessage(rm);
        }

        private static string ShowHelp(string x)
        {
            switch(x)
            {
                case ("add"):
                    return "Please type 'add', followed by one or more words describing your command.";
                case ("remove"):
                    return "Please type 'remove', followed by the item you want removed.";
                case ("show"):
                    return "Typing 'show menu' will show you what you've ordered.";
                case ("quit"):
                    return "This is a tricky one. Quit should work alone, and push all changes to the Db.";
                default:
                    return "should not reach here";
            }
        }

        private static string HandleCommand(string action, string parameters, Message m)
        {
            switch (action)
            {
                case ("add"):
                    return action + " [ " + parameters + " ]";
                case ("remove"):
                    return action + " [ " + parameters + " ]";
                case ("show"):
                    return action + " [ " + parameters + " ]";
                case ("quit"):
                    return action + " [ " + parameters + " ]";
                default:
                    return "should not reach here";
            }
        }
    public static Message HandleSystemMessage(Message message)
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