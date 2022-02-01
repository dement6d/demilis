using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demilis
{
    public class Write
    {
        static int consoleWidth = Console.WindowWidth;
        // Default terminal length = 80
        internal static void Centered(string text)
        {
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
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Write.Centered(":::::::-.  .,::::::  .        :   ::: :::     ::: .::::::. ");
            Write.Centered(" ;;,   `';,;;;;''''  ;;,.    ;;;  ;;; ;;;     ;;;;;;`    ` ");
            Write.Centered(" `[[     [[ [[cccc   [[[[, ,[[[[, [[[ [[[     [[['[==/[[[[,");
            Write.Centered("  $$,    $$ $$\"\"\"\"   $$$$$$$$\"$$$ $$$ $$'     $$$  '''    $");
            Write.Centered("  888_,o8P' 888oo,__ 888 Y88\" 888o888o88oo,.__888 88b    dP");
            Write.Centered("  MMMMP\"`   \"\"\"\"YUMMMMMM  M'  \"MMMMMM\"\"\"\"YUMMMMMM  \"YMmMY\" ");
            Console.ForegroundColor = ConsoleColor.Red;
            Write.Centered("The Multiple Connection Listener");
            Console.ResetColor();
        }

        internal static void Separator()
        {
            string separator = "\n";
            for (int i = 0; i < consoleWidth; i++)
            {
                separator += ":";
            }
            separator += "\n";
            Console.WriteLine(separator);
        }
    }
}
