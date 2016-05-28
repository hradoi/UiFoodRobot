using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Connector;
using CrawlerLibrary;

namespace UiFoodRobot
{
    public class MessageHandlers
    {
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

            int tally = 0;
            order.TryGetValue(category, out tally);

            tally += 1;
            order[category] = tally;
            YellowFoodConstructor x = new YellowFoodConstructor();
            string rm = "";
            foreach (var q in x.Query(category))
                rm += $"{q.Name} has been added to your order\n\n";

            var replyMessage = CreateReply(message, rm);

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

        public static Message HandleFindCommand(Command command, Message message)
        {
            if (string.IsNullOrWhiteSpace(command.Parameters))
            {
                return CreateReply(message, "Here I will return the whole menu. It's gonna be the best menu there was. The graetest function. Nobody builds menu-building functions better than me. I will build the whole menu. And make Mexico pay for that menu. Mark my words. ");
            }

            YellowFoodConstructor x = new YellowFoodConstructor();
             
            string rm = "I found: \n\n ";
            List<CrawlerLibrary.Model.OutputMenu> queryResult = x.Query(command.Parameters);

            if (!queryResult.Any())
            {
                rm += "nothing :(";
                return CreateReply(message, rm);
            }

            foreach (var q in queryResult)
                rm += $"{q.Name}\n\n";

            var replyMessage = CreateReply(message, rm);
            //replyMessage.SetBotUserData("order", tallies);
            return replyMessage;
        }

        public static Message HandleSudo(Message message)
        {
            YellowFoodConstructor x = new YellowFoodConstructor();
            x.UpdateMenu(true);
            var replyMessage = CreateReply(message, "Forcefully updated the menu for you!");
            return replyMessage;
        }
    }
}