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

        public static Message CreateCommand(Command command, Message message, bool fromDatabase = false)
        {

            string[] keywords;
            Message replyMessage = Reply.CreateAttachment(message);
            OutputMenu[] menuItems;
            if (!generateKeywords(command, out keywords))
                return replyAndClearCookie(message);

            //choose data source         
            if (fromDatabase)
            {
                // load from database
                menuItems = loadFromDatabaseUsingCategories(keywords);
            }
            else
            {
                // load from botuserdata
                var x = message.GetBotUserData<OutputMenu[]>("returnedMenuItems");
                if (x != null)
                {
                    var temp = new List<OutputMenu>();
                    foreach (var item in x)
                        foreach (var keyword in keywords)
                            if (item.Name.Contains(keyword))
                                temp.Add(item);
                    if (!temp.Any())
                    {
                        menuItems = loadFromDatabaseUsingCategories(keywords);
                    }
                    else
                    {
                        menuItems = temp.ToArray();
                    }
                }
                else
                {
                    menuItems = loadFromDatabaseUsingCategories(keywords); //fallback if unable to load from botuserdata
                }
            }

            //foreach (var item in menuItems)
            //    replyMessage.Text += item.Name + "\n\n";

            if (menuItems.Length < 1)
            {
                return replyAndClearCookie(message);
            }

            Attachment attachment = new Attachment()
            {
                Text = "Pick one:",
                Actions = new List<Bot.Action>()
            };


            for (var x = 0; x < menuItems.Length; x++)
            {
                attachment.Actions.Add(new Bot.Action() { Title = menuItems[x].Name, Message = $"Add {menuItems[x].Name}" });
            }
            replyMessage.Attachments.Add(attachment);

            //// persist data
            ////replyMessage.SetBotUserData("from", message.GetBotUserData<string>("from"));
            //replyMessage.SetBotUserData("order", message.GetBotUserData<Dictionary<string, int>>("order"));
            replyMessage.SetBotUserData("returnedMenuItems", menuItems);

            return replyMessage;


        }
    }
}