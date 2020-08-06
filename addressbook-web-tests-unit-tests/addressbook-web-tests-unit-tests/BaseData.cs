using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests_unit_tests
{
    class BaseData
    {
        private string baaseURL = "http://localhost/addressbook/";

        public string BaaseURL { get => baaseURL; set => baaseURL = value; }
    }

}
