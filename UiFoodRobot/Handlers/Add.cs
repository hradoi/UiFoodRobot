using CrawlerLibrary;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrawlerLibrary.Model;

using Bot = Microsoft.Bot.Connector;
using LinqKit;

namespace UiFoodRobot
{
    public static class Add
    {
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

            OutputMenu[] returnedMenuItems = message.GetBotUserData<OutputMenu[]>("returnedMenuItems");
            if (returnedMenuItems == null)
                returnedMenuItems = x.GetTodaysMenu().ToArray();

            string[] keywords;
            Message replyMessage = Reply.CreateAttachment(message);
            if (!Command.generateKeywords(command, out keywords))
                return replyAndClearCookie(message);

            var menuItems = x.SearchFromSource(keywords, returnedMenuItems.ToList(), or:false);

            if (!menuItems.Any())
                menuItems = x.SearchFromSource(keywords, returnedMenuItems.ToList()); // test. If shitty, please replace with from Database

            if (!menuItems.Any())
                menuItems = x.SearchTodaysMenu(keywords, or: false); // get from DB, still strict

            if (!menuItems.Any())
                return replyAndClearCookie(message);

            if (menuItems.Length == 1)
            {
                OutputMenu[] savedOrder = replyMessage.GetBotUserData<OutputMenu[]>("Order");

                if (savedOrder == null)
                {
                    //return Reply.Create(message, "null brah!");
                    savedOrder = new OutputMenu[0];
                }
                List<OutputMenu> order = savedOrder.ToList();
                order.Add(menuItems[0]);

                replyMessage.SetBotUserData("Order", order.ToArray());
                replyMessage.SetBotUserData("returnedMenuItems", null);

                replyMessage.Text = "Here's what's on your order so far: \n\n";
                savedOrder = replyMessage.GetBotUserData<OutputMenu[]>("Order");
                foreach (var j in savedOrder)
                    replyMessage.Text += j.Name + " \n\n";
                return replyMessage;
            }

                Attachment attachment = new Attachment()
            {
                Text = "Pick one:",
                Actions = new List<Bot.Action>()
            };

            for (var i = 0; i < menuItems.Length; i++)
            {
                attachment.Actions.Add(new Bot.Action() { Title = menuItems[i].Name, Message = $"Add {menuItems[i].Name}" });
            }

            replyMessage.Attachments.Add(attachment);
            replyMessage.SetBotUserData("returnedMenuItems", menuItems);
            return replyMessage;
        }

        internal static Message Clear(Message message)
        {
            message.SetBotUserData("Order", null);
            return Reply.Create(message, "Cleared the order for you boss!");
        }
    }
}