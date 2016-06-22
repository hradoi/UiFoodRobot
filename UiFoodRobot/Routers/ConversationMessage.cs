using Microsoft.Bot.Connector;

namespace UiFoodRobot
{
    public static class MessageRouter
    {
        public static Message HandleMessage(Message message)
        {
            Command command = null;
            if (Command.TryParse(message.Text, out command) == false)
                return Reply.ErrorMessage(message);
            //string flagFollowUp = message.GetBotUserData<string>("flag");
            switch (command.Action.ToLower())
            {
                //case "hi":
                //case "hello":
                //    return Reply.Create(message,"Hello!");
                //case "ping":
                //    return Ping.CreateCommand(command, message);
                case "add":
                    return Add.StartCommand(command, message);
                //case "finish":
                //    return Add.FinishCommand(command, message);
                //case "clear":
                //    return Clear.CreateCommand(command, message);
                //case "delete":
                //    return Delete.CreateCommand(message);
                case "find":
                    return Find.CreateCommand(command, message);
                case "show":
                    return Find.CreateCommand(new Command { Parameters = new[] { "" } }, message, true);
                case "sudo":
                    return Sudo.CreateCommand(message);
                default:
                    //return Reply.ErrorMessage(message);
                    return Find.TestCreateCommand(command, message);
            }

        }
    }
}