using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests_unit_tests
{
    public class LoginData
    {
        private string name;
        private string pass;

        public LoginData()
        {
            name = "admin";
            pass = "secret";
        }

        public string Name { get => name; set => name = value; }
        public string Pass { get => pass; set => pass = value; }
    }
}
