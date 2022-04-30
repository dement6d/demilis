using System.Collections;
using System.Diagnostics;

namespace demilis.Command.Commands
{
    internal class Exit : Command
    {
        public Exit() : base()
        {
            name = "exit";
            alias = "quit";
            description = "Exits demilis";
        }

        public override void Execute(ArrayList args)
        {
            Environment.Exit(0);
        }
    }
}
