using System;
using System.Collections.Generic;
using System.Linq;
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
        public override void Execute()
        {
            
        }
    }
}
