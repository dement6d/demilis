using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demilis.Command.Commands
{
    internal class Nick : Command
    {
        public Nick() : base()
        {
            name = "nick";
            description = "Set a nickname for a session. Example: 'nick 1 Client1'";
        }

        public override void Execute(ArrayList args)
        {
            if (args.Count >= 2)
            {
                if (int.TryParse(args[0].ToString(), out int result))
                {
                    if (Session.SessionExists(result))
                    {
                        string input = "no";
                        if (Program.nicknames.ContainsValue(args[1].ToString()))
                        {
                            Write.Error($"Session {Program.nicknames.FirstOrDefault(x => x.Value == args[1]).Key} already has this nickname.\nContinue anyways? yes/No");
                            input = Console.ReadLine().Trim().ToLower();
                        }
                        else
                        {
                            if (Program.nicknames.ContainsKey(result)) Program.nicknames.Remove(result);
                            Program.nicknames.Add(result, args[1].ToString());
                        }
                        if (input == "yes" || input == "ye" || input == "y" || input == "absolutely" || input == "ofc")
                        {
                            if (Program.nicknames.ContainsKey(result)) Program.nicknames.Remove(result);
                            Program.nicknames.Add(result, args[1].ToString());
                        }
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
                Write.Error("Please specify a session and a nickname (without spaces) for it. Example: 'nick 1 Client1'");
            }
        }
    }
}
