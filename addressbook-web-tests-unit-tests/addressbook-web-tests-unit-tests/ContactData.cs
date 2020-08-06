using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests_unit_tests
{
    class ContactData
    {
        private string name = "DefaultName";
        private string surname = "DefaultSurName";
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
