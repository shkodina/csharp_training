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
    }
}
