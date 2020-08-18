using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace addressbook_web_tests_unit_tests
{
    public class MNavyHelper : MBaseHelper
    {
        public MNavyHelper(IWebDriver driver, AppManager appManager) : base(driver, appManager)
        {
        }

        public MNavyHelper GoToByName(string name)
        {
            driver.FindElement(By.LinkText(name)).Click();
            return this;
        }
    }
}
