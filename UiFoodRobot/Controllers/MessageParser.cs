using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Linq;

namespace UiFoodRobot
{
    internal class MessageParser
    {
        public static Message HandleMessage(Message message)
        {
            const string NotSupportMessage = "Sorry, I could not understand that.";

            Command command = null;
            if (Command.TryParse(message.Text, out command) == false)
            {
                return MessageParser.CreateReply(message, NotSupportMessage);
            }

            switch (command.Action)
            {
                case "add":
                    return MessageParser.HandleAddCommand(command, message);
                case "clear":
                    return MessageParser.HandleClearCommand(command, message);
                default:
                    return MessageParser.CreateReply(message, NotSupportMessage);
            }

            return message;
        }

        const string ErrMessage = "Sorry, I could not understand that. I need a command. Please try the keywords \"add\",\"remove\",\"show\",\"quit\".";
        static readonly List<string> commands = new List<string>() { "add", "remove", "show", "quit" };
        public static Message CreateReply(Message message, string text)
        {
            var reply = message.CreateReplyMessage(text);
            reply.BotUserData = message.BotUserData;

            return reply;
        }

        public static Message HandleAddCommand(Command command, Message message)
        {
            if (string.IsNullOrWhiteSpace(command.Parameters))
            {
                return CreateReply(message, "I need a category to keep tally for. Example: add exercise");
            }

            var category = command.Parameters.ToLowerInvariant();
            var tallies = message.GetBotUserData<Dictionary<string, int>>("tallies");

            if (tallies == null)
            {
                tallies = new Dictionary<string, int>();
            }

            int tally = 0;
            tallies.TryGetValue(category, out tally);

            tally += 1;
            tallies[category] = tally;

            var replyMessage = CreateReply(message, $"The tally for '{category}' has been updated to {tally}");

            // Set the new tally value to the reply message
            replyMessage.SetBotUserData("tallies", tallies);

            return replyMessage;
        }

        public static Message HandleClearCommand(Command command, Message message)
        {
            if (string.IsNullOrWhiteSpace(command.Parameters))
            {
                return CreateReply(message, "I category to reset the tally for. Example: clear coffee");
            }

            var category = command.Parameters.ToLowerInvariant();
            var tallies = message.GetBotUserData<Dictionary<string, int>>("tallies");

            if (tallies == null)
            {
                tallies = new Dictionary<string, int>();
            }

            tallies[category] = 0;

            var replyMessage = CreateReply(message, $"The tally for '{category}' has been reset to zero");
            replyMessage.SetBotUserData("tallies", tallies);

            return replyMessage;
        }



        private static string ShowHelp(string x)
        {
            switch (x)
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



////deprecated
//private static Message HandleCommand(string action, string parameters, Message m)
//{
//    string rt = "";
//    var order = m.GetBotUserData<Dictionary<string, int>>("order");

//    if (order == null)
//    {
//        order = new Dictionary<string, int>();
//    }
//    int x = 0;
//    switch (action)
//    {
//        case ("add"):

//            order.TryGetValue(parameters, out x);
//            x += 1;
//            order[parameters] = x;
//            rt = "Added 1 to " + parameters;
//            break;
//        case ("remove"):
//            order.TryGetValue(parameters, out x);
//            x -= 1;
//            order[parameters] = x;
//            rt = "Removed 1 from " + parameters;
//            break;
//        //case ("show"):
//        //    return action + " [ " + parameters + " ]";
//        //case ("quit"):
//        //    return action + " [ " + parameters + " ]";
//        default:
//            return m;// "should not reach here";
//    }
//    var rm = m.CreateReplyMessage(rt);
//    rm.SetBotUserData("order", order);
//    return rm;
//}
