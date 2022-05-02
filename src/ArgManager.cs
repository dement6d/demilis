using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demilis
{
    public class ArgManager
    {
        public ArrayList arguments = new ArrayList();
        public ArgManager()
        {
            arguments.Add(new Argument("help", "Shows this page"));
            arguments.Add(new Argument("host", "Set the local IPv4 address to listen on", "h"));
            arguments.Add(new Argument("port", "Set the PORT to listen on", "p", true));
            arguments.Add(new Argument("verbose", "Specify this argument if you wish for demilis to print more output information", "v"));
            arguments.Add(new Argument("show-location", "Provides client location in the client list", "sl"));
            arguments.Add(new Argument("hide-ips", "Censors IP Addresses when clients connect and in the client list", "hips"));
        }
        public ArrayList GetArgs()
        {
            return arguments;
        }
    }
}
