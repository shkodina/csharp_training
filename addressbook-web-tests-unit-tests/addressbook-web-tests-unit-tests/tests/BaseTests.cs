using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using addressbook_web_tests_unit_tests;

namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class BaseTests
    {
        protected AppManager app;

        [SetUp]
        public void SetupTest()
        {
            app = AppManager.GetInstance();           
        }

        protected string GenNewSuffixByCurTimeStamp()
        {
            return System.DateTime.Now.ToString();
        }

        private static Random rnd = new Random();
        public static string GenRndStr(int max)
        {
           
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder strb = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                strb.Append(
                    Convert.ToChar(32 + // make simbol printable (from 32 to 255)
                        Convert.ToInt32(rnd.NextDouble() * 223) // gen simbol position from 0 to 223 
                    )
                );
            }
            return strb.ToString();
        }

    }
}
