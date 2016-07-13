using Microsoft.Bot.Connector;

namespace UiFoodRobot
{
    public static class MessageRouter
    {
        public static Message HandleMessage(Message message)
        {
            Command command = null;
            if (!Command.TryParse(message.Text, out command))
                return Reply.ErrorMessage(message);
            //string flagFollowUp = message.GetBotUserData<string>("flag");

            string help = 
                " Welcome to the UiFoodRobot! \n\n" +
                "To find an item, please use the keyword 'find'. \n\n " +
                "To add an item to the menu, either click on a find result or type 'add ' + keywords. \n\n " +
                "To clear your order, use the 'clear' keyword. \n\n " +
                "To see the menu, hit 'show'. \n\n " +
                "To forcefully update the menu, enter 'sudo'. \n\n " +
                "Have an amazing day! \n\n " +
                "";

            switch (command.Action.ToLower())
            {
                case "hi":
                case "hello":
                    return Reply.Create(message, "Hello!");
                case "ping":
                    return Ping.CreateCommand(command, message);
                case "find":
                    return Find.Create(command, message);
                case "add":
                    return Add.Create(command, message);
                case "clear":
                    return Add.Clear(message);
                //case "delete":
                //    return Delete.CreateCommand(message);
                case "show":
                    return Find.Create(new Command { Parameters = new[] { "" } }, message);
                case "sudo":
                    return Sudo.start(command, message);
                case "help":
                    return Reply.Create(message, help);
                default:
                    return Reply.ErrorMessage(message);
            }

        }
    }
}