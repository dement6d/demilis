using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demilis.Command.Commands
{
    internal class Status : Command
    {
        public Status() : base()
        {
            name = "status";
            description = "Displays the current IP and PORT";
        }
        public override void Execute()
        {
            Console.WriteLine($"Listening for incoming TCP connections on {Program.ip}:{Program.port}");
        }
    }
}
