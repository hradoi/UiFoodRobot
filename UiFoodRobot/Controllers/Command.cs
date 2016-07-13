using System.Collections.Generic;
using System.Linq;

namespace UiFoodRobot
{
    public class Command
    {
        public string Action { get; set; }
        public string[] Parameters { get; set; }
        
        public static bool TryParse(string text, out Command command)
        {
            command = null;
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            text = text.Trim().ToLowerInvariant();
            command = new Command();

            // Look for the first whitespace and split there
            var whiteSpaceIndex = text.IndexOf(' ');

            if (whiteSpaceIndex == -1)
            {
                // No white space, treat the message as a command without parameter
                command.Action = text;
                return true;
            }

            // Extract the action & parameters
            command.Action = text.Substring(0, whiteSpaceIndex).ToLowerInvariant();
            command.Parameters = text.Substring(whiteSpaceIndex + 1).Split(); //procesare de scos "de", "si", "cu" etc.
            return true;
        }

        internal static bool generateKeywords(Command command, out string[] keywords)
        {
            List<string> ignoredKeywords = new List<string> { "de", "si", "cu", "and", "la", "sau" };
            if (command.Parameters != null)
            {
                keywords = command.Parameters.Where(p => !ignoredKeywords.Contains(p)).ToArray();
                return (keywords.Count() != 0) ? true : false;
            }

            keywords = null;
            return false;
        }
    }
}
