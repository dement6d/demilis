using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demilis.Command
{
    internal class Command
    {
        public string name;
        public string? alias;
        public string description;
        public Command() { }
        public Command(string name, string description, string? alias=null)
        {
            this.name = name;
            this.alias = alias;
            this.description = description;
        }
        public virtual void Execute(ArrayList args) { }
    }
}
