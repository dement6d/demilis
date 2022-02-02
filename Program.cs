using demilis;
using System.Collections;
using System.Net;
using System.Net.Sockets;

bool verbose = false;
string ipInput = "127.0.0.1";
IPAddress ip;
ushort port = 80;

if (Environment.GetCommandLineArgs().Contains("--help") || Environment.GetCommandLineArgs().Contains("help") && Environment.GetCommandLineArgs().Length <= 2)
{
    Write.Centered("Help for using demilis");
}
Write.Logo();
Write.Separator();
Console.ResetColor();
// HELP PAGE
if (Environment.GetCommandLineArgs().Contains("--help") || Environment.GetCommandLineArgs().Contains("help") && Environment.GetCommandLineArgs().Length <= 2)
{
    Write.HelpPage();
    return;
}

// SET VERBOSE FROM ARGUMENT
if (Environment.GetCommandLineArgs().Contains("--verbose") || Environment.GetCommandLineArgs().Contains("-v"))
{
    verbose = true;
}
// GET IP FROM ARGUMENT
if (Environment.GetCommandLineArgs().Contains("--host") || Environment.GetCommandLineArgs().Contains("-h"))
{
    short indexOfHostArg = 0;
    if (Environment.GetCommandLineArgs().Length >= 3)
    {
        if (Environment.GetCommandLineArgs().Contains("-h"))
        {
            indexOfHostArg = (short)Array.FindIndex(Environment.GetCommandLineArgs(), row => row.Contains("-h"));
        }
        else
        {
            indexOfHostArg = (short)Array.FindIndex(Environment.GetCommandLineArgs(), row => row.Contains("--host"));
        }
        try
        {
            new TcpListener(IPAddress.Parse(Environment.GetCommandLineArgs().GetValue(indexOfHostArg + 1).ToString()), port);
            ipInput = Environment.GetCommandLineArgs().GetValue(indexOfHostArg + 1).ToString();
        }
        catch
        {
            Write.Error($"{Environment.GetCommandLineArgs().GetValue(indexOfHostArg + 1)} is not a valid IP.");
            return;
        }
    }
    else
    {
        Write.Error("Pleace specify a valid IP after the host argument");
        return;
    }
}
// GET PORT FROM ARGUMENT
if (Environment.GetCommandLineArgs().Contains("--port") || Environment.GetCommandLineArgs().Contains("-p"))
{
    short indexOfPortArg = 0;
    if (Environment.GetCommandLineArgs().Length >= 3)
    {
        if (Environment.GetCommandLineArgs().Contains("-h"))
        {
            indexOfPortArg = (short)Array.FindIndex(Environment.GetCommandLineArgs(), row => row.Contains("-p"));
        }
        else
        {
            indexOfPortArg = (short)Array.FindIndex(Environment.GetCommandLineArgs(), row => row.Contains("--port"));
        }
        int result;
        if (Int32.TryParse(Environment.GetCommandLineArgs().GetValue(indexOfPortArg+1).ToString(), out result))
        {
            if (result < 1)
            {
                Write.Error($"The selected port ({Environment.GetCommandLineArgs().GetValue(indexOfPortArg+1)}) is invalid. The port must be greater than 0.");
                return;
            }
            else if (result > ushort.MaxValue)
            {
                Write.Error($"The selected port ({Environment.GetCommandLineArgs().GetValue(indexOfPortArg + 1)}) is invalid. The port must be less or equal to {ushort.MaxValue}.");
                return;
            }
            port = (ushort)result;
        }
        else
        {
            Write.Error("Pleace specify a valid number after the port argument");
            return;
        }
    }
    else
    {
        Write.Error("Pleace specify a valid number after the port argument");
        return;
    }
}

TcpListener listener = new TcpListener(System.Net.IPAddress.Parse(ipInput), port);
try
{
    listener.Start();
}
catch (Exception e)
{
    string toWrite = verbose ? e.ToString() : e.Message;
    Write.Error($"Result of listening on {System.Net.IPAddress.Parse(ipInput)}:{port}: {toWrite}");
    return;
}
Console.WriteLine($"Listening for incoming TCP connections on {System.Net.IPAddress.Parse(ipInput)}:{port}");

ArrayList sockets = new ArrayList();
while (true)
{
    try
    {
        Socket socket = listener.AcceptSocket();
        sockets.Add(socket);
        Console.WriteLine($"{socket.RemoteEndPoint} connected");
    }
    catch (Exception e)
    {
        string toWrite = verbose ? e.ToString() : e.Message;
        Write.Error(toWrite);
    }
}