using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UiFoodRobot
{
    public class Delete
    {
        public static Message CreateCommand(Message message)
        {
            //var tallies = message.GetBotUserData<Dictionary<string, int>>("order");
            //if (tallies != null)
            //{
            //    tallies = null;
            //}
            var replyMessage = Reply.Create(message, $"Your order has been deleted. Please start from scratch.");
            replyMessage.SetBotUserData("order", null);
            replyMessage.SetBotUserData("items", null);
            return replyMessage;
        }
    }
}