using CrawlerLibrary;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using Bot = Microsoft.Bot.Connector;
namespace UiFoodRobot
{
    public static class Sudo
    {

        public static Message start(Command command, Message message)
        {
            string rm;
            if (command.Parameters[0] == "password")
            {
                YellowFoodConstructor YFC = new YellowFoodConstructor();
                YFC.UpdateMenu(true);
                var today = YFC.GetTodaysMenu();
                rm = "Forcefully updated the menu for you!\n\n Here's what's good:\n\n";
                //foreach (var x in today)
                //    rm += $"- {x.Name} \n\n";

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
            }
            else
                rm = "Not today, bub!";
            var replyMessage = Reply.Create(message, rm);
            return replyMessage;
        }
    }
}