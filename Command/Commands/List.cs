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
                    toWrite += "| " + GetLocation(Program.dictionary[session].RemoteEndPoint.ToString()) + " ";

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
                IPLocation IPLocation;
                string retJson = DownloadDataNoAuth(string.Format("https://www.freegeoip.net/json/{0}", ip));
                var JO = JObject.Parse(retJson);
                return (string)JO["country_name"];
            }
            catch
            {
                return "Unknown";
            }
        }
        public string DownloadDataNoAuth(string hostURI)
        {
            string retXml = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(hostURI);
                request.Method = "GET";
                String responseLine = String.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(dataStream);
                        retXml = sr.ReadToEnd();
                        sr.Close();
                        dataStream.Close();
                    }
                }
            }
            catch (Exception e)
            {
                retXml = null;
            }
            return retXml;
        }
    }
    public class IPLocation
    {
        public string IPAddress { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string Region { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string ISP { get; set; }
        public string Organization { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ZipCode { get; set; }
        public string TimeZone { get; set; }
    }
}
