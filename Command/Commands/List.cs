using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using IpInfo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            int consoleWidth = Console.WindowWidth; // need 2 account for uneven numbers
            int activeSessions = 0;
            foreach (Socket socket in Program.dictionary.Values)
            {
                if (socket.Connected)
                {
                    activeSessions++;
                }
            }
            Write.Centered($"Available sessions: {activeSessions}");
            string header = $"Session ";
            while (header.Length < consoleWidth / 3) { header += " "; }
            header += "| IP ";
            while (header.Length < consoleWidth / 1.5) { header += " "; }
            header += "| Location ";
            Console.WriteLine(header);

            int printedLines = 1;
            foreach (int session in Program.dictionary.Keys) // maybe come up with a way to not repeat 2 foreach loops but get same result
            {
                if (Program.dictionary[session].Connected)
                {
                    if (printedLines % 2 == 0) { Console.ForegroundColor = ConsoleColor.DarkGray; }
                    string toWrite = $"Session {session} ";
                    while (toWrite.Length < consoleWidth / 3) { toWrite += " "; }
                    toWrite += "| " + Program.dictionary[session].RemoteEndPoint + " ";
                    while (toWrite.Length < consoleWidth / 1.5) { toWrite += " "; }

                    string ip = Program.dictionary[session].RemoteEndPoint.ToString();
                    int index = ip.IndexOf(":");

                    if (index >= 0)
                    {
                        ip = ip.Substring(0, index);
                    }

                    toWrite += "| " + GetLocation(ip) + " ";

                    Console.WriteLine(toWrite);
                    Console.ResetColor();
                    printedLines++;
                }
            }
        }
        public string GetLocation(string ip)
        {
            try
            {
                IpInfoApi ipInfoApi = new IpInfoApi(new HttpClient());
                return ipInfoApi.GetLocationByIpAsync(ip).Result;
            }
            catch (Exception e)
            {
                return "Unknown";
            }
        }
    }
}
