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
            command.Parameters = text.Substring(whiteSpaceIndex + 1).Split();
            return true;
        }
    }
}
