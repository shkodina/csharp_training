using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;

namespace mantis_test
{
    public class AdminHelper : MBaseHelper
    {
        public AdminHelper(IWebDriver driver) : base(driver)
        {
        }

        public AdminHelper(IWebDriver driver, AppManager appManager) : base(driver, appManager)
        {
        }

        public List<AccountData> GetAllAccounts()
        {
            return null;
        }

        public void DeleteAccount(AccountData acc)
        {
            IWebDriver drv = OpenAppAndLogin();
            drv.Url = BaseData.BaseURL + "/manage_user_page.php";

            ReadOnlyCollection<IWebElement> s =
                drv.FindElements(By.XPath("//body//table/tbody//a"));

            String u = null;
            foreach(IWebElement el in s)
            {
                if (el.Text == acc.Name)
                {
                    u = el.GetAttribute("href");
                    break;
                }
            }

            drv.Url = BaseData.BaseURL + "/" + u;
            System.Threading.Thread.Sleep(500);
            drv.FindElement(By.XPath("//input[@value='Delete User']")).Click();
            drv.FindElement(By.XPath("//input[@value='Delete Account']")).Click();
            System.Threading.Thread.Sleep(1000);
        }

        public IWebDriver OpenAppAndLogin()
        {
            IWebDriver drv = new SimpleBrowserDriver();
            drv.Url = BaseData.BaseURL + BaseData.LoginPage;
            drv.FindElement(By.XPath("//input[@name='username']")).SendKeys("administrator");
            drv.FindElement(By.XPath("//input[@type='submit']")).Click();
            System.Threading.Thread.Sleep(5000);
            drv.FindElement(By.XPath("//input[@name='password']")).SendKeys("root");
            drv.FindElement(By.XPath("//input[@type='submit']")).Click();
            return drv;
        }
    }
}
