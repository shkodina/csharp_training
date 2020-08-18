using System;
using OpenQA.Selenium;

namespace addressbook_web_tests_unit_tests
{
    public class MContactsHelper : MBaseHelper
    {
        public MContactsHelper(IWebDriver driver, AppManager app) : base(driver, app)
        {
        }

        public MContactsHelper GoToContacts()
        {
            this.AppManager.mNavyHelper.GoToByName("home");
            return this;
        }

        public MContactsHelper FillNewContactFields(ContactData cd)
        {
            FillInputByName("firstname", cd.Name);
            FillInputByName("middlename", cd.Surname);
            return this;
        }

        public MContactsHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            return this;
        }

        public MContactsHelper InitCreationNewContact()
        {
            this.AppManager.mNavyHelper.GoToByName("add new");
            return this;
        }
    }
}
