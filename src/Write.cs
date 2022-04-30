using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demilis
{
    public class Write
    {
        static int consoleWidth = Console.WindowWidth; // need 2 account for uneven numbers
        // Default terminal length = 80
        internal static void Centered(string text)
        {
            consoleWidth = Console.WindowWidth;
            // need to account for uneven consle width numbers or use 80
            int spacesToAdd = 0;
            if (text.Length % 2 != 0) // simplify if statement
            {
                spacesToAdd = (consoleWidth - 1) - text.Length;
            }
            else
            {
                spacesToAdd = consoleWidth - text.Length;
            }
            string spaces = "";
            for (int i = 0; i < (spacesToAdd / 2); i++)
            {
                spaces += " ";
            }
            Console.WriteLine(spaces + text + spaces);
        }

        internal static void Logo()
        {
            consoleWidth = Console.WindowWidth;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (consoleWidth > @"   ___  ______  _________   ________".Length)
            {
                Write.Centered(@"   ___  ______  _________   ________");
                Write.Centered(@"  / _ \/ __/  |/  /  _/ /  /  _/ __/");
                Write.Centered(@" / // / _// /|_/ _/ // /___/ /_\ \  ");
                Write.Centered(@"/____/___/_/  /_/___/____/___/___/  ");
            }
            else
            {
                Write.Centered("DEMILIS");
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Write.Centered("The Multiple TCP Connection Listener");
            Console.ResetColor();
        }
        internal static void Separator()
        {
            consoleWidth = Console.WindowWidth;
            string separator = "\n";
            for (int i = 0; i < consoleWidth; i++)
            {
                separator += ":";
            }
            separator += "\n";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(separator);
            Console.ResetColor();
        }
        internal static void HelpPage()
        {
            consoleWidth = Console.WindowWidth;
            int printedLines = 1;
            ArgManager argManager = new ArgManager();

            Write.Centered("-VALID ARGUMENTS-\n");

            foreach (Argument arg in argManager.GetArgs())
            {
                string currentLine = arg.required ? "(Required) --" + arg.GetName() + " " : "--" + arg.GetName() + " ";

                if (!arg.GetAlias().Equals("none"))
                {
                    string spaces2Add = "";
                    for (int i = 0; i < (consoleWidth/6)-currentLine.Length; i++)
                    {
                        spaces2Add += " ";
                    }
                    currentLine += $"{spaces2Add}-{arg.GetAlias()}";
                }

                do
                {
                    currentLine += " ";
                }
                while (currentLine.Length < (consoleWidth/2) - 13);

                currentLine += "Descripiton: " + arg.GetDescription();

                if (printedLines % 2 == 0) { Console.ForegroundColor = ConsoleColor.DarkGray; }   
                Console.WriteLine(currentLine);
                Console.ResetColor();
                printedLines++;
            }
        }
        internal static void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static Func<int, string> MoveLeft = count => $"\x1b[{count}D";
        public static Func<int, string> MoveRight = count => $"\x1b[{count}C";
        public static Func<int, string> MoveDown = count => $"\x1b[{count}B";
        public static Func<int, string> MoveUp = count => $"\x1b[{count}A";
    }
}
