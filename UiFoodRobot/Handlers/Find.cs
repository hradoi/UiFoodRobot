using CrawlerLibrary;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrawlerLibrary.Model;

using Bot = Microsoft.Bot.Connector;

namespace UiFoodRobot
{
    public static class Find
    {
        public static Message CreateCommand(Command command, Message message)
        {
            List<string> ignoredKeywords = new List<string> { "de", "si", "cu", "and", "la", "sau" };
            if (command.Parameters == null)
                return Reply.Create(message, "Please try again, using more relevant parameters.");

            var categories = command.Parameters.Where(p => !ignoredKeywords.Contains(p)).ToList();

            if (categories.Count() == 0)
                return Reply.Create(message, "Please try again, using more relevant parameters.");

            YellowFoodConstructor yellowFoodRepository = new YellowFoodConstructor();
            List<OutputMenu> menuItems = yellowFoodRepository.SearchTodaysMenu(categories.ToArray());

            if (!menuItems.Any())
            {
                return Reply.Create(message, "I couldn't find anything, please try again!");
            }

            Message replyMessage = Reply.CreateAttachment(message);

            Attachment attachment = new Attachment()
            {
                Text = "Pick one:",
                Actions = new List<Bot.Action>()
            };

            foreach (var x in menuItems)
            {
                attachment.Actions.Add(new Bot.Action() { Title = x.Name, Message = $"Add {x.Name}", Image = x.Img });
            }
            replyMessage.Attachments.Add(attachment);

            // persist data
            replyMessage.SetBotUserData("flag", "OK");
            replyMessage.SetBotUserData("order", message.GetBotUserData<Dictionary<string, int>>("order"));
            replyMessage.SetBotUserData("items", menuItems.ToArray());

            return replyMessage;
        }
    }
}