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
            FillRegForm(acc);
            SubmitReg();
        }

        private void SubmitReg()
        {
            throw new NotImplementedException();
        }

        private void FillRegForm(AccountData acc)
        {
            throw new NotImplementedException();
        }
    }
}