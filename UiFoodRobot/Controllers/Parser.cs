using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;

namespace UiFoodRobot
{
    public class Parser
    {
        String from;
        String text;
        public Parser(Message m)
        {
            from = m.From.Name;
            text = m.Text;
        }
        public string action()
        {
            return from + " said: " + text;
        }

    }
}