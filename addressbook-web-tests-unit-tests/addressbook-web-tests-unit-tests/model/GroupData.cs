using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests_unit_tests
{
    public class GroupData
    {
        private string name = "";
        private string header = "";
        private string footer = "";

        public GroupData(string name)
        {
            this.name = name;
        }

        public string Header { get => header; set => header = value; }
        public string Footer { get => footer; set => footer = value; }
        public string Name { get => name; set => name = value; }
    }
}
