using System;
using OpenQA.Selenium;

namespace mantis_test
{
    public class RegistrationHelper : MBaseHelper
    {
        public RegistrationHelper(IWebDriver driver) : base(driver)
        {
        }
       public RegistrationHelper(IWebDriver driver, AppManager app) : base(driver, app)
        {
        }

        public void Rigester(AccountData acc)
        {
            OpenHomePage(BaseData.BaseURL);
            OpenRegPage();
            FillRegForm(acc);
            SubmitReg();
        }

        private void OpenRegPage()
        {
            this.driver.FindElement(By.CssSelector("a.back-to-login-link.pull-left")).Click();
        }

        private void SubmitReg()
        {
            throw new NotImplementedException();
        }

        private void FillRegForm(AccountData acc)
        {
            this.driver.FindElement(By.Name("username")).SendKeys(acc.Name);
            this.driver.FindElement(By.Name("email")).SendKeys(acc.Email);
        }
    }
}