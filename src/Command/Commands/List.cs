using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using IPGeolocation;

namespace demilis.Command.Commands
{
    internal class List : Command
    {
        public List() : base()
        {
            name = "list";
            alias = "ls";
            description = "Displays a list of active sessions";
        }
        public override void Execute(ArrayList args)
        {
            int consoleWidth = Console.WindowWidth; // need 2 account for uneven numbers
            int activeSessions = 0;

            foreach (TcpClient client in Program.dictionary.Values)
            {
                if (client.Connected)
                {
                    activeSessions++;
                }
            }

            // Write Header
            Write.Centered($"Available sessions: {activeSessions}");
            string header = $"Session ";
            while (header.Length < consoleWidth / 3) { header += " "; }
            header += "| IP ";
            while (header.Length < consoleWidth / 1.5) { header += " "; }
            if (Program.useApi) header += "| Location ";
            Console.WriteLine(header);

            int printedLines = 1;
            foreach (int session in Program.dictionary.Keys) // maybe come up with a way to not repeat 2 foreach loops but get same result
            {
                if (Program.dictionary[session].Connected)
                {
                    if (printedLines % 2 == 0) { Console.ForegroundColor = ConsoleColor.DarkGray; }
                    string toWrite = "";
                    if (Program.nicknames.ContainsKey(session)) toWrite = $"Session {session} ({Program.nicknames[session]}) ";
                    else toWrite = $"Session {session} ";
                    while (toWrite.Length < consoleWidth / 3) { toWrite += " "; }
                    if (Program.hideIPs) toWrite += "| " + Regex.Replace(Program.dictionary[session].Client.RemoteEndPoint.ToString() + " ", "[0-9]", "*");
                    else toWrite += "| " + Program.dictionary[session].Client.RemoteEndPoint + " ";
                    while (toWrite.Length < consoleWidth / 1.5) { toWrite += " "; }

                    if (Program.useApi)
                    {
                        try
                        {
                            IPGeolocationAPI api = new IPGeolocationAPI("9397b3fe90f5426a8556253450d51005");

                            GeolocationParams geoParams = new GeolocationParams();
                            geoParams.SetIp(GetIPFromEndPoint(Program.dictionary[session].Client.RemoteEndPoint));
                            geoParams.SetFields("geo,time_zone,currency");

                            Geolocation geolocation = api.GetGeolocation(geoParams);

                            if (geolocation.GetStatus() == 200)
                            {
                                toWrite += "| " + geolocation.GetCountryName() + " ";
                            }
                        }
                        catch
                        {
                            toWrite += "| Unknown ";
                        }
                    }
                    Console.WriteLine(toWrite);
                    Console.ResetColor();
                    printedLines++;
                }
            }
        }
        public string GetIPFromEndPoint(EndPoint IPEndPoint)
        {
            int index = IPEndPoint.ToString().IndexOf(":");

            if (index >= 0)
            {
                return IPEndPoint.ToString().Substring(0, index); // IP without port
            }
            return null;
        }
    }
}
