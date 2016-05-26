using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Linq;
using System;

namespace UiFoodRobot
{
    public class MessageParser
    {
        const string NotSupportMessage = "Sorry, I could not understand that.";
        const string cookie = "../../lastUpdate.txt";

        public static Message HandleMessage(Message message)
        {
            Command command = null;
            if (Command.TryParse(message.Text, out command) == false)
            {
                return CreateReply(message, NotSupportMessage);
            }

            switch (command.Action)
            {
                case "add":
                    return HandleAddCommand(command, message);
                case "clear":
                    return HandleClearCommand(command, message);
                case "delete":
                    return HandleDeleteCommand(message);
                case "sudo":
                    YellowConstructor yc = new YellowConstructor();

                    return CreateReply(message, yc.GetMenu());
                default:
                    return CreateReply(message, NotSupportMessage);
            }
        }
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
                return CreateReply(message, "I need a menu item to add to your oder. Try \"Add burger\" or \"Add soup\"");
            }

            var category = command.Parameters.ToLowerInvariant();
            var order = message.GetBotUserData<Dictionary<string, int>>("order");
            var time = message.GetBotUserData<DateTime>("time");

            if (order == null)
            {
                order = new Dictionary<string, int>();
            }


            //FoodObject x = new FoodObject();
            string xjs = "hodor.";

            int tally = 0;
            order.TryGetValue(category, out tally);

            tally += 1;
            order[category] = tally;

            var replyMessage = CreateReply(message, xjs);

            // Set the new tally value to the reply message
            replyMessage.SetBotUserData("order", order);
            replyMessage.SetBotUserData("time", DateTime.Now);
            return replyMessage;
        }

        public static Message HandleClearCommand(Command command, Message message)
        {
            if (string.IsNullOrWhiteSpace(command.Parameters))
            {
                return CreateReply(message, "I category to reset the tally for. Example: clear coffee");
            }

            var category = command.Parameters.ToLowerInvariant();
            var tallies = message.GetBotUserData<Dictionary<string, int>>("order");

            if (tallies == null)
            {
                tallies = new Dictionary<string, int>();
            }

            tallies[category] = 0;

            var replyMessage = CreateReply(message, $"The tally for '{category}' has been reset to zero");
            replyMessage.SetBotUserData("order", tallies);

            return replyMessage;
        }

        public static Message HandleDeleteCommand(Message message)
        {
            var tallies = message.GetBotUserData<Dictionary<string, int>>("order");
            if (tallies != null)
            {
                tallies = null;
            }
            var replyMessage = CreateReply(message, $"Your order has been deleted. Please start from scratch.");
            replyMessage.SetBotUserData("order", tallies);
            return replyMessage;
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