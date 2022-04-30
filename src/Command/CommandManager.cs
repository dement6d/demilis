using demilis.Command.Commands;
using System.Collections;

namespace demilis
{
    internal class CommandManager
    {
        private static ArrayList commands = new ArrayList();
        internal CommandManager()
        {
            commands.Add(new Help());
            commands.Add(new Status());
            commands.Add(new List());
            commands.Add(new Exit());
            commands.Add(new Session());
            commands.Add(new Nick());
            commands.Add(new Clear());
        }

        internal Command.Command GetCommand(string input)
        {
            int index = input.Length;
            if (input.Contains(" ")) index = input.IndexOf(" ");
            if (!String.IsNullOrEmpty(input))
            {
                foreach (Command.Command command in commands)
                {
                    if (command.name.ToLower() == input.ToLower().Trim().Substring(0, index))
                        return command;
                    if (!string.IsNullOrEmpty(command.alias) && command.alias.ToLower() == input.ToLower().Trim().Substring(0, index))
                        return command;
                }
            }
            return null;
        }
        internal static ArrayList GetCommands()
        {
            return commands;
        }
    }
}
