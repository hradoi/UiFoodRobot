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
            Message replyMessage = Find.CreateCommand(command, message);

            var x =  replyMessage.GetBotUserData<string>("flag");
            if (x != null)
                replyMessage.Text = "Yolo";
            return replyMessage;
            throw new NotImplementedException();
        }

        internal static Message FinishCommand(Command command, Message message)
        {
            throw new NotImplementedException();
        }
    }
}