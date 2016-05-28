using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Linq;
using System;
using CrawlerLibrary.Model;
using CrawlerLibrary.Context;

namespace UiFoodRobot
{
    public class MessageParser
    {
        const string NotSupportMessage = "Sorry, I could not understand that.";

        public static Message HandleMessage(Message message)
        {
            Command command = null;
            if (Command.TryParse(message.Text, out command) == false)
            {
                return MessageHandlers.CreateReply(message, NotSupportMessage);
            }
            // TODO: add persistence
            switch (command.Action)
            {
                case "add":
                    return MessageHandlers.HandleAddCommand(command, message);
                case "clear":
                    return MessageHandlers.HandleClearCommand(command, message);
                case "delete":
                    return MessageHandlers.HandleDeleteCommand(message);
                case "find":
                    return MessageHandlers.HandleFindCommand(command, message);
                case "sudo":
                    return MessageHandlers.HandleSudo(message);
                default:
                    return MessageHandlers.CreateReply(message, NotSupportMessage);
            }

        }

        public static Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
                Message reply = message.CreateReplyMessage("Hello!");
                return reply;
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
                return message.CreateReplyMessage("'Ello there!");
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
                return message.CreateReplyMessage("Hasta la vista, baby!");

            }
            else if (message.Type == "EndOfConversation")
            {
                return message.CreateReplyMessage("Buh bye!");
            }

            return null;
        }
    }
}