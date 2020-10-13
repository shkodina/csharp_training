using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace mantis_test
{
    public class AppManager
    {
        private IWebDriver driver;
        /*
        public MLoginHelper mLoginHelper;
        public MGroupsHelper mGroupsHelper;
        public MContactsHelper mContactsHelper;
        public MNavyHelper mNavyHelper;
        */

        private static ThreadLocal<AppManager> instance = new ThreadLocal<AppManager>();

        public RegistrationHelper RegHelper { get;  set; }
        public FTPHelper FTPHelper { get;  set; }
        public JamesHelper JHelper { get; set; }

        private AppManager()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            driver = new ChromeDriver(chromeOptions);

            /*
            mLoginHelper = new MLoginHelper(driver, this);
            mGroupsHelper = new MGroupsHelper(driver, this);
            mContactsHelper = new MContactsHelper(driver, this);
            mNavyHelper = new MNavyHelper(driver, this);
            */

            RegHelper = new RegistrationHelper(driver, this);
            FTPHelper = new FTPHelper(driver, this);
            JHelper = new JamesHelper(driver, this);
        }

        public static AppManager GetInstance()
        {
            if (!instance.IsValueCreated)
            {
                instance.Value = new AppManager();
            }
            
            return instance.Value;
        }

        ~AppManager()
        {
            driver.Quit();
        }
    }
}
