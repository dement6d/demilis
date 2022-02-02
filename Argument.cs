using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demilis
{
    internal class Argument
    {
        private string name;
        private string description;
        private string alias = "none";
        public Argument(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
        public Argument(string name, string description, string alias)
        {
            this.name = name;
            this.description = description;
            this.alias = alias;
        }
        public string GetName() { return name; }
        public string GetDescription() { return description; }
        public string GetAlias() { return alias; }
    }
}
