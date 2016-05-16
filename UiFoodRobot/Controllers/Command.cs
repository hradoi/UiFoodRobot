using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UiFoodRobot.Controllers
{
    public class Command
    {
        public string Action { get; set; }

        public string Parameters { get; set; }

        public static bool TryParse(string text, out Command command)
        {
            command = null;

            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            text = text.Trim();
            command = new Command();

            // Look for the first white space
            var whiteSpaceIndex = text.IndexOf(' ');

            if (whiteSpaceIndex == -1)
            {
                // No white space, treat the message as a command without parameter
                command.Action = text.ToLowerInvariant();
                return true;
            }

            // Extract the action & parameters
            command.Action = text.Substring(0, whiteSpaceIndex).ToLowerInvariant();
            command.Parameters = text.Substring(whiteSpaceIndex + 1);

            return true;
        }
    }
}