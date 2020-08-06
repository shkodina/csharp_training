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
    class CommonActions
    {
        private IWebDriver driver;
        private BaseData basedata;

        public CommonActions(IWebDriver driver)
        {
            this.driver = driver;
            this.basedata = new BaseData();
        }

        public void OpenHomePage()
        {
            driver.Navigate().GoToUrl(basedata.BaaseURL);
        }

        public void Login(LoginData ld)
        {
            driver.FindElement(By.XPath("//input[@name='user']")).Click();
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(ld.Name);
            driver.FindElement(By.Name("pass")).Click();
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(ld.Pass);
            //driver.FindElement(By.Id("LoginForm")).Submit();
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }
        public void Logout()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        public void GotoHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }
    }
}
