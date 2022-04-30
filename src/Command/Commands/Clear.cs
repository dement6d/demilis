using System.Collections;

namespace demilis.Command.Commands
{
    internal class Clear : Command
    {
        public Clear() : base()
        {
            base.name = "clear";
            base.description = "Clears the command line interface";
        }
        public override void Execute(ArrayList args)
        {
            Console.Clear();
            Write.Logo();
        }
    }
}
