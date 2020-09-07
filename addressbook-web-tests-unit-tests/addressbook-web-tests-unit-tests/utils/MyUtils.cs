using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests_unit_tests.utils
{
    class MyUtils
    {
        public static string ValOrStub(string val)
        {
            return val == null ? "" : val;
        }

    }
}
