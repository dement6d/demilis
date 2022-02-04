using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demilis.Command.Commands
{
    internal class Help : Command
    {
        public Help() : base()
        {
            base.name = "help";
            base.description = "Displays list of commands";
        }
        public override void Execute()
        {
            int consoleWidth = Console.WindowWidth; // need 2 account for uneven numbers
            int printedLines = 1;

            Write.Centered("-AVAILABLE COMMANDS-\n");

            foreach (Command command in CommandManager.GetCommands())
            {
                string currentLine = command.name + " ";

                do
                {
                    currentLine += " ";
                }
                while (currentLine.Length < (consoleWidth / 2) - 13);

                currentLine += "Descripiton: " + command.description;

                if (printedLines % 2 == 0) { Console.ForegroundColor = ConsoleColor.DarkGray; }
                Console.WriteLine(currentLine);
                Console.ResetColor();
                printedLines++;
            }
        }
    }
}
