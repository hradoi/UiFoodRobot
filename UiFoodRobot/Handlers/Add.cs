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
    public static class Add
    {
        public static Message StartCommand(Command command, Message message)
        {
            message.SetBotUserData("from","add");
            Message replyMessage = Find.CreateCommand(command, message);
            if (replyMessage.GetBotUserData<string>("from") == "find")
                return replyMessage;
            OutputMenu[] returnedMenuItems = message.GetBotUserData<OutputMenu[]>("returnedMenuItems");
            replyMessage.Text = returnedMenuItems[0].Name;
            
            replyMessage.Attachments = new Attachment[0];
            return replyMessage;
        }

        internal static Message FinishCommand(Command command, Message message)
        {
            throw new NotImplementedException();
        }
    }
}