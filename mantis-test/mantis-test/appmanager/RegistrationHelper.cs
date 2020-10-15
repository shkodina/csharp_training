using System;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

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
            String url = GetConfirmURL(acc);
            FillPwdForm(url, acc);
            SubmitFillPwdForm(url);
            System.Threading.Thread.Sleep(10 * 1000);
        }

        private void FillPwdForm(string url, AccountData acc)
        {
            driver.Url = url;
            this.driver.FindElement(By.CssSelector("input#realname.form-control")).SendKeys(acc.Name);
            this.driver.FindElement(By.CssSelector("input#password.form-control")).SendKeys(acc.Password);
            this.driver.FindElement(By.CssSelector("input#password-confirm.form-control")).SendKeys(acc.Password);
        }

        private void SubmitFillPwdForm(string url)
        {
            this.driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        private string GetConfirmURL(AccountData acc)
        {
            String m = this.AppManager.MHelper.GetLastMail(acc);
            return Regex.Match(m, @"http://\S*").Value;
        }

        private void OpenRegPage()
        {
            this.driver.FindElement(By.CssSelector("a.back-to-login-link.pull-left")).Click();
        }

        private void SubmitReg()
        {
            this.driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        private void FillRegForm(AccountData acc)
        {
            this.driver.FindElement(By.Name("username")).SendKeys(acc.Name);
            this.driver.FindElement(By.Name("email")).SendKeys(acc.Email);
        }
    }
}