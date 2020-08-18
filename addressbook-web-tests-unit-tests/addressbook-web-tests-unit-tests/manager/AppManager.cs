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
    public class AppManager
    {
        private IWebDriver driver;
        public MLoginHelper mLoginHelper;
        public MGroupsHelper mGroupsHelper;
        public MContactsHelper mContactsHelper;
        public MNavyHelper mNavyHelper;
        private static AppManager instance;

        private AppManager()
        {
            driver = new ChromeDriver();

            mLoginHelper = new MLoginHelper(driver, this);
            mGroupsHelper = new MGroupsHelper(driver, this);
            mContactsHelper = new MContactsHelper(driver, this);
            mNavyHelper = new MNavyHelper(driver, this);
        }

        public static AppManager GetInstance()
        {
            if (instance == null)
            {
                instance = new AppManager();
            }
            
            return instance;
        }

        ~AppManager()
        {
            driver.Quit();
        }
    }
}
