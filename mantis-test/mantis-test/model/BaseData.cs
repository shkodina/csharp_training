﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_test
{
    public class BaseData
    {
        public static string BaseURL = @"http://localhost/mantisbt-2.24.2";
        public static string TestDataBaseAddress = @"C:\Users\user\Source\Repos\csharp_training\addressbook-web-tests-unit-tests\addressbook-test-data-generators\bin\Debug\";
        public static string RegPage = @"/signup_page.php";
        public static string LoginPage = @"/login_page.php";
        public static string MantisConfigFileByFTP = @"/config_defaults_inc.php";
        public static string MantisConfigFileLocal = "config_defaults_inc.php";
        public static string TelnetHost = "localhost";
        public static int TelnetPort = 4555;

    }

}
