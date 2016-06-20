using Microsoft.Bot.Connector;
using UiFoodRobot.Routers;

namespace UiFoodRobot
{
    public static class Ping
    {
        public static Message CreateCommand(Command command, Message message)
        {
            return Reply.Create(message, "pong");
        }
    }
}