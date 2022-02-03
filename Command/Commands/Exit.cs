using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demilis.Command.Commands
{
    internal class Exit : Command
    {
        public Exit() : base()
        {
            name = "exit";
            description = "Exits demilis";
        }

        public override void Execute()
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
