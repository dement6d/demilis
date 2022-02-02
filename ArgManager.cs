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
            arguments.Add(new Argument("host", "Set the local IP to listen on", "h"));
            arguments.Add(new Argument("port", "Set the PORT to listen on", "p"));
            arguments.Add(new Argument("verbose", "Specify this argument if you wish to view all additional connection information while using demilis", "v"));
        }
        public ArrayList GetArgs()
        {
            return arguments;
        }
    }
}
