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
        private bool required = false;
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
        public Argument(string name, string description, bool required)
        {
            this.name = name;
            this.description = description;
            this.required = required;
        }
        public Argument(string name, string description, string alias, bool required)
        {
            this.name = name;
            this.description = description;
            this.alias = alias;
            this.required = required;
        }
        public string GetName() { return name; }
        public string GetDescription() { return description; }
        public string GetAlias() { return alias; }
        public bool IsRequired() { return required; }
    }
}
