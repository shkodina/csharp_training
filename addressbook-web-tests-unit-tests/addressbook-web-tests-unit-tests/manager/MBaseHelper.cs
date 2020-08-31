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
    public class MBaseHelper
    {
        protected IWebDriver driver;
        private AppManager appManager;
        protected AppManager AppManager { get => appManager; set => appManager = value; }

        public MBaseHelper(IWebDriver driver)
        {
            this.driver = driver;
            OpenHomePage(new BaseData().BaseURL);
        }

        public MBaseHelper(IWebDriver driver, AppManager appManager) : this(driver)
        {
            this.appManager = appManager;
        }

        public MBaseHelper FillInputByName(string name, string value)
        {
            if (value != null)
            {
                driver.FindElement(By.Name(name)).Click();
                driver.FindElement(By.Name(name)).Clear();
                driver.FindElement(By.Name(name)).SendKeys(value);
            }
            return this;
        }

        public MBaseHelper SubmitByClick(string button_name)
        {
            driver.FindElement(By.Name(button_name)).Click();
            return this;
        }

        public MBaseHelper ClickByText(String text)
        {
            driver.FindElement(By.LinkText(text)).Click();
            return this;
        }

        public bool IsLogin()
        {
            return IsElementPresent(By.Name("logout"));
        }

        public bool IsLogin(LoginData correctLogin)
        {
            return (IsLogin())
                    &&

                    GetLoogedUserName() == correctLogin.Name;
        }

        private string GetLoogedUserName()
        {
            return driver.FindElement(By.Name("logout"))
                    .FindElement(By.TagName("b"))
                    .Text
                    .Split('(')[1]
                    .Split(')')[0];
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public MBaseHelper OpenHomePage(string URL)
        {
            driver.Navigate().GoToUrl(URL);
            return this;
        }

        public void GotoHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }

        public string GetNullIfEmpty(string txt)
        {
            if (txt == null)
                return null;
            if (txt.Trim() == "")
                return null;
            return txt;
        }
    }
}

