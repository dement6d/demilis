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
                    toWrite += "| " + GetLocation(Program.dictionary[session].RemoteEndPoint.ToString()) + " ";

                    Console.WriteLine(toWrite);
                    Console.ResetColor();
                    printedLines++;
                }
            }
        }
        private string GetLocation(string ip)
        {
            using var client = new HttpClient();
            IpInfoApi ipInfo = new IpInfoApi(client);
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfoApi>(info);
                return ipInfo.GetCountryByIpAsync(ip).Result;
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }
    }
}
