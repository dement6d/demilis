using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace demilis.Command.Commands
{
    internal class List : Command
    {
        public List() : base()
        {
            name = "list";
            description = "Displays a list of active sessions";
        }
        public override void Execute()
        {
            int activeSessions = 0;
            foreach (Socket socket in Program.dictionary.Values)
            {
                if (socket.Connected)
                {
                    activeSessions++;
                }
            }
            Write.Centered($"Available sessions: {activeSessions}");
            int printedLines = 1;
            foreach (int session in Program.dictionary.Keys) // maybe come up with a way to not repeat 2 foreach loops but get same result
            {
                if (Program.dictionary[session].Connected)
                {
                    if (printedLines % 2 == 0) { Console.ForegroundColor = ConsoleColor.DarkGray; }
                    Console.WriteLine($"Session {session}: {Program.dictionary[session].RemoteEndPoint}");
                    Console.ResetColor();
                    printedLines++;
                }
            }
        }
    }
}
