using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests_unit_tests
{
    public class MLoginHelper : MBaseHelper
    {
        public MLoginHelper(IWebDriver driver, AppManager app) : base(driver, app)
        {
        }

        public MLoginHelper Login(LoginData ld)
        {
            if (IsLogin()) 
            {
                return this;
            }

            FillInputByName("user", ld.Name);
            FillInputByName("pass", ld.Pass);
            driver.FindElement(By.XPath("(//input[@value='Login'])")).Click();
            //SubmitByClick("Login");
            return this;
        }

 
        public MLoginHelper Logout()
        {
            if (IsLogin())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
            return this;
        }

    }
}
