using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests_unit_tests
{
    class LoginData
    {
        private string name = "admin";
        private string pass = "secret";

        public LoginData()
        {

        }

        public string Name { get => name; set => name = value; }
        public string Pass { get => pass; set => pass = value; }
    }
}
