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

        public void OpenHomePage(string URL)
        {
            driver.Navigate().GoToUrl(URL);
        }

        public void GotoHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }

    }
}

