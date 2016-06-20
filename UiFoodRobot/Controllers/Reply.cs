using System;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace UiFoodRobot
{
    public static class Reply
    {
        internal static Message Create(Message message, string text)
        {
            var reply = message.CreateReplyMessage(text);
            reply.BotUserData = message.BotUserData;
            return reply;
        }

        internal static Message CreateAttachment(Message message)
        {
            var reply = message.CreateReplyMessage();
            reply.BotUserData = message.BotUserData;
            reply.Attachments = new List<Attachment>();
            return reply;
        }


        internal static Message ErrorMessage(Message message)
        {
            var reply = message.CreateReplyMessage("Sorry, I could not understand that.");
            reply.BotUserData = message.BotUserData;
            return reply;
        }
    }
}