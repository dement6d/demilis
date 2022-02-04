using System.Net;
using System.Net.Sockets;

namespace demilis {
    internal class Program {

        static bool verbose = false;
        static string ipInput = "0.0.0.0";
        public static IPAddress ip;
        public static ushort port = 80;

        public static Dictionary<int, Socket> dictionary = new Dictionary<int, Socket>();
        static int socketNumber = 0;

        static void Main(string[] args) {

             if (HandleArguments() == -1) return;

            Write.Logo();
            Write.Separator();
            Console.ResetColor();

            TcpListener listener = new TcpListener(IPAddress.Parse(ipInput), port);
            try
            {
                ip = IPAddress.Parse(ipInput);
                listener.Start();
            }
            catch (Exception e)
            {
                string toWrite = verbose ? e.ToString() : e.Message;
                Write.Error($"Result of listening on {IPAddress.Parse(ipInput)}:{port}: {toWrite}");
                return;
            }
            Console.WriteLine($"Listening for incoming TCP connections on {System.Net.IPAddress.Parse(ipInput)}:{port}");

            AcceptConnections(listener);
            CommandManager commandManager = new CommandManager();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("demilis> ");
                Console.ResetColor();
                string input = Console.ReadLine();
                try
                {
                    if (!String.IsNullOrEmpty(input.Trim())) commandManager.GetCommand(input).Execute();
                }
                catch
                {
                    Write.Error($"Invalid command \"{input}\", type \"help\" for a list of available commands");
                }
            }
        }
        protected static async Task AcceptConnections(TcpListener listener)
        {
            while (true)
            {
                try
                {
                    Socket socket = await listener.AcceptSocketAsync();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{socket.RemoteEndPoint} connected");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("demilis> ");
                    Console.ResetColor();

                    dictionary.Add(socketNumber, socket);
                    socketNumber++;
                }
                catch (Exception e)
                {
                    string toWrite = verbose ? e.ToString() : e.Message;
                    Write.Error(toWrite);
                }
            }
        }
        protected static int HandleArguments()
        {
            // HELP PAGE
            if (Environment.GetCommandLineArgs().Contains("--help", StringComparer.OrdinalIgnoreCase) || Environment.GetCommandLineArgs().Length <= 2)
            {
                Write.Centered("Help for using demilis");
                Write.Logo();
                Write.Separator();
                Write.HelpPage();
                return -1;
            }
            // SET VERBOSE FROM ARGUMENT
            if (Environment.GetCommandLineArgs().Contains("--verbose", StringComparer.OrdinalIgnoreCase) || Environment.GetCommandLineArgs().Contains("-v", StringComparer.OrdinalIgnoreCase))
            {
                verbose = true;
            }
            // GET IP FROM ARGUMENT
            if (Environment.GetCommandLineArgs().Contains("--host", StringComparer.OrdinalIgnoreCase) || Environment.GetCommandLineArgs().Contains("-h", StringComparer.OrdinalIgnoreCase))
            {
                short indexOfHostArg = 0;
                if (Environment.GetCommandLineArgs().Length >= 3)
                {
                    if (Environment.GetCommandLineArgs().Contains("-h", StringComparer.OrdinalIgnoreCase))
                    {
                        indexOfHostArg = (short)Array.FindIndex(Environment.GetCommandLineArgs(), row => row.Contains("-h", StringComparison.OrdinalIgnoreCase));
                    }
                    else
                    {
                        indexOfHostArg = (short)Array.FindIndex(Environment.GetCommandLineArgs(), row => row.Contains("--host", StringComparison.OrdinalIgnoreCase));
                    }
                    try
                    {
                        new TcpListener(IPAddress.Parse(Environment.GetCommandLineArgs().GetValue(indexOfHostArg + 1).ToString()), port);
                        ipInput = Environment.GetCommandLineArgs().GetValue(indexOfHostArg + 1).ToString();
                    }
                    catch
                    {
                        Write.Error($"{Environment.GetCommandLineArgs().GetValue(indexOfHostArg + 1)} is not a valid IPv4 address");
                        return -1;
                    }
                }
                else
                {
                    Write.Error("Pleace specify a valid IPv4 address after the host argument");
                    return -1;
                }
            }
            // GET PORT FROM ARGUMENT
            if (Environment.GetCommandLineArgs().Contains("--port", StringComparer.OrdinalIgnoreCase) || Environment.GetCommandLineArgs().Contains("-p", StringComparer.OrdinalIgnoreCase))
            {
                short indexOfPortArg = 0;
                if (Environment.GetCommandLineArgs().Length >= 3)
                {
                    if (Environment.GetCommandLineArgs().Contains("-p", StringComparer.OrdinalIgnoreCase))
                    {
                        indexOfPortArg = (short)Array.FindIndex(Environment.GetCommandLineArgs(), row => row.Contains("-p", StringComparison.OrdinalIgnoreCase));
                    }
                    else
                    {
                        indexOfPortArg = (short)Array.FindIndex(Environment.GetCommandLineArgs(), row => row.Contains("--port", StringComparison.OrdinalIgnoreCase));
                    }
                    int result;
                    if (Int32.TryParse(Environment.GetCommandLineArgs().GetValue(indexOfPortArg + 1).ToString(), out result))
                    {
                        if (result < 1)
                        {
                            Write.Error($"The selected port ({Environment.GetCommandLineArgs().GetValue(indexOfPortArg + 1)}) is invalid. The port must be greater than 0");
                            return -1;
                        }
                        else if (result > ushort.MaxValue)
                        {
                            Write.Error($"The selected port ({Environment.GetCommandLineArgs().GetValue(indexOfPortArg + 1)}) is invalid. The port must be less or equal to {ushort.MaxValue}");
                            return -1;
                        }
                        port = (ushort)result;
                    }
                    else
                    {
                        Write.Error($"Pleace specify a valid number after the port argument. {Environment.GetCommandLineArgs().GetValue(indexOfPortArg + 1)} is not a valid number");
                        return -1;
                    }
                }
                else
                {
                    Write.Error("Pleace specify a valid number after the port argument");
                    return -1;
                }
            }
            return 0;
        }
    }
}