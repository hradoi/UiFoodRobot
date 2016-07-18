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
        private static OutputMenu[] loadFromDatabaseUsingCategories(string[] categories)
        {
            YellowFoodConstructor yellowFoodRepository = new YellowFoodConstructor();
            return yellowFoodRepository.SearchTodaysMenu(categories);
        }

        private static bool generateKeywords(Command command, out string[] keywords)
        {
            List<string> ignoredKeywords = new List<string> { "de", "si", "cu", "and", "la", "sau" };
            if (command.Parameters != null)
            {
                keywords = command.Parameters.Where(p => !ignoredKeywords.Contains(p)).ToArray();
                return (keywords.Count() != 0) ? true : false;
            }

            keywords = null;
            return false;
        }

        private static Message replyAndClearCookie(Message message)
        {
            Message replyMessage = Reply.Create(message, "Please try again using more relevant terms!");
            replyMessage.SetBotUserData("returnedMenuItems", null);
            return replyMessage;
        }

        public static Message Create(Command command, Message message)
        {
            YellowFoodConstructor x = new YellowFoodConstructor();
            //x.UpdateMenu();

            string[] keywords;
            Message replyMessage = Reply.CreateAttachment(message);
            if (!Command.generateKeywords(command, out keywords))
                return replyAndClearCookie(message);
            var menuItems = x.SearchTodaysMenu(keywords);
            Attachment attachment = new Attachment()
            {
                Text = "Pick one:",
                Actions = new List<Bot.Action>()
            };

            for (var i = 0; i < menuItems.Length; i++)
            {
                attachment.Actions.Add(new Bot.Action() { Title = menuItems[i].Name, Message = $"Add {menuItems[i].Name}", Image = menuItems[i].Img  });
            }
            replyMessage.Attachments.Add(attachment);
            replyMessage.SetBotUserData("returnedMenuItems", menuItems);
            return replyMessage;
        }
    }
}