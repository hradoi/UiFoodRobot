using CrawlerLibrary;
using Microsoft.Bot.Connector;

namespace UiFoodRobot
{
    public static class Sudo
    {

        public static Message CreateCommand(Message message)
        {
            YellowFoodConstructor YFC = new YellowFoodConstructor();
            YFC.UpdateMenu(true);
            var today = YFC.GetTodaysMenu();
            string rm = "Forcefully updated the menu for you!\n\n Here's what's good:\n\n";
            foreach (var x in today)
                rm += $"- {x.Name} \n\n";
            
            var replyMessage = Reply.Create(message, rm);
            return replyMessage;
        }
    }
}