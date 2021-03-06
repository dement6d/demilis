using System.Collections;
using System.Net.Sockets;
using System.Text;

namespace demilis.Command.Commands
{
    internal class Session : Command
    {
        public Session() : base()
        {
            name = "session";
            alias = "s";
            description = "Interacts with the specified session. Example: 'session 1'";
        }
        public override void Execute(ArrayList args)
        {
            if (args.Count > 0)
            {
                if (int.TryParse(args[0].ToString(), out int result) || Program.nicknames.ContainsValue(args[0].ToString()))
                {
                    if (Program.nicknames.ContainsValue(args[0].ToString())) result = Program.nicknames.FirstOrDefault(x => x.Value == args[0].ToString()).Key;
                    if (SessionExists(result))
                    {
                        ReadLine.ClearHistory();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Write.Centered($"Interacting with session {result}");
                        Console.ResetColor();
                        Console.WriteLine();
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
                Read(session);
                NetworkStream stream = Program.dictionary[session].GetStream();
                while (true)
                {
                    string input = ReadLine.Read().Trim();
                    if (!string.IsNullOrEmpty(input)) ReadLine.AddHistory(input);

                    if (input.Trim() == "exit")
                    {
                        ReadLine.ClearHistory();
                        Program.interacting = false;
                        break;
                    }

                    byte[] buffMessage = Encoding.ASCII.GetBytes(input+"\r\n");

                    stream.Write(buffMessage, 0, buffMessage.Length);
                    stream.Flush();
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
            TcpClient client = Program.dictionary[session];
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);

            while (Program.interacting)
            {
                try
                {
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
        public static bool SessionExists(int number)
        {
            foreach (int session in Program.dictionary.Keys)
            {
                if (Program.dictionary[session].Connected && session == number) return true;
            }
            return false;
        }
    }
}
