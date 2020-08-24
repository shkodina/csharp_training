using System;
using System.Collections.Generic;
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
            FillInputByName("lastname", cd.Surname);
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

        public MContactsHelper SubmitContactUpdate()
        {
            this.SubmitByClick("update");
            return this;
        }

        public MContactsHelper InitEditContact(int v)
        {
            v = v + 2;
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + v + "]//img[@alt='Edit']")).Click();
            return this;
        }

        public MContactsHelper SubmitDeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            //driver.SwitchTo().Alert().Accept();

            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            alert.Accept();
            //alert.Dismiss();

            return this;
        }

        public bool IsContactExist()
        {
            GoToContacts();
            return IsElementPresent(By.XPath("//table[@id='maintable']//img[@alt='Edit']"));
        }

        public MContactsHelper SelectContact(int v)
        {
            v = v + 2;
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + v + "]/td/input")).Click();
            return this;
        }

        public List<ContactData> GetContactsList()
        {
            List<ContactData> cd = new List<ContactData>();
            this.GoToContacts();
            foreach (IWebElement el in driver.FindElements(By.XPath("//table[@id='maintable']/tbody/tr[@name='entry']")))
            {
                IReadOnlyList <IWebElement> tags = el.FindElements(By.TagName("td"));
                cd.Add(new ContactData(
                    tags[2].Text,
                    tags[1].Text
                ));
            }
            return cd;
        }
    }
}
