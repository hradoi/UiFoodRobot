//using Microsoft.Bot.Connector;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace UiFoodRobot
//{
//    public class Clear
//    {
//        public static Message CreateCommand(Command command, Message message)
//        {
//            if (string.IsNullOrWhiteSpace(command.Parameters))
//            {
//                return Reply.Create(message, "I category to reset the tally for. Example: clear coffee");
//            }

//            var category = command.Parameters.ToLowerInvariant();
//            var tallies = message.GetBotUserData<Dictionary<string, int>>("order");

//            if (tallies == null)
//            {
//                tallies = new Dictionary<string, int>();
//            }

//            tallies[category] = 0;

//            var replyMessage = Reply.Create(message, $"The tally for '{category}' has been reset to zero");
//            replyMessage.SetBotUserData("order", tallies);

//            return replyMessage;
//        }
//    }
//}