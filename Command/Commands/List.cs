using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using IPGeolocation;

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
            if (Program.useapi) header += "| Location ";
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

                    if (Program.useapi)
                    {
                        try
                        {
                            IPGeolocationAPI api = new IPGeolocationAPI("9397b3fe90f5426a8556253450d51005");

                            GeolocationParams geoParams = new GeolocationParams();
                            geoParams.SetIp(GetIPFromEndPoint(Program.dictionary[session].RemoteEndPoint.ToString()));
                            geoParams.SetFields("geo,time_zone,currency");

                            Geolocation geolocation = api.GetGeolocation(geoParams);

                            if (geolocation.GetStatus() == 200)
                            {
                                toWrite += "| " + geolocation.GetCountryName() + " ";
                            }

                        }
                        catch (Exception e)
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
        public string GetIPFromEndPoint(string IPEndPoint)
        {
            int index = IPEndPoint.IndexOf(":");

            if (index >= 0)
            {
                return IPEndPoint.Substring(0, index); // IP without port
            }
            return null;
        }
    }
}
