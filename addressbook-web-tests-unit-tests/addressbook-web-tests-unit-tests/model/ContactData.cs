using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests_unit_tests
{
    public class ContactData
    {
        private string name = null;
        private string surname = null;
        public ContactData() { }

        public ContactData(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
    }
}
