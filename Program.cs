using demilis;

Console.WriteLine($"Argument count: {Environment.GetCommandLineArgs().Length}");
if (Environment.GetCommandLineArgs().Contains("--help") || Environment.GetCommandLineArgs().Contains("-h"))
{
    //
    Write.Centered("Help for running demilis");
    Write.Logo();

    Console.ForegroundColor = ConsoleColor.DarkRed;
    Write.Separator();
    Console.ResetColor();
    return;
}
Console.ReadLine();
