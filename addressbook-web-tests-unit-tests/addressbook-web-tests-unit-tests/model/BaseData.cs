using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests_unit_tests
{
    public class BaseData
    {
        
        private string baseURL = "http://localhost/addressbook/";
        private string testDataBaseAddress = @"C:\Users\user\Source\Repos\csharp_training\addressbook-web-tests-unit-tests\addressbook-test-data-generators\bin\Debug\";

        public string BaseURL { get => baseURL; set => baseURL = value; }
        public string TestDataBaseAddress { get => testDataBaseAddress; set => testDataBaseAddress = value; }
    }

}
