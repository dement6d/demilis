using demilis.Command.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        internal Command.Command GetCommand(string input)
        {
            int index = input.Length;
            if (input.Contains(" ")) index = input.IndexOf(" ");
            if (!String.IsNullOrEmpty(input))
            {
                foreach (Command.Command command in commands)
                {
                    if (command.name.ToLower() == input.ToLower().Trim() || command.name.ToLower() == input.ToLower().Substring(0, index).ToString())
                    {
                        return command;
                    }
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
