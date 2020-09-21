using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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
            FillInputByName("middlename", cd.MiddleName);
            FillInputByName("lastname", cd.Surname);

            FillInputByName("home", cd.HomePhone);
            FillInputByName("work", cd.WorkPhone);
            FillInputByName("mobile", cd.MobiPhone);

            FillInputByName("address", cd.Address);

            FillInputByName("email", cd.EMail); 
            FillInputByName("email2", cd.EMail2); 
            FillInputByName("email3", cd.EMail3); 

            return this;
        }

        public MContactsHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            contactsListHash = null;
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
            contactsListHash = null;
            return this;
        }

        public MContactsHelper InitEditContact(int v)
        {
            v = v + 2;
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + v + "]//img[@alt='Edit']")).Click();
            return this;
        }

        public MContactsHelper IniOpenDetailContact(int v)
        {
            v = v + 2;
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + v + "]//img[@alt='Details']")).Click();
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

            contactsListHash = null;
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

        public MContactsHelper SelectContact(string id)
        {
            driver.FindElement(By.Id(id)).Click();
            return this;
        }

        private List<ContactData> contactsListHash = null;

 
 
        public List<ContactData> GetContactsList()
        {
            if (contactsListHash == null)
            {
                contactsListHash = new List<ContactData>();
                foreach (IWebElement el in driver.FindElements(By.XPath("//table[@id='maintable']/tbody/tr[@name='entry']")))
                {
                    IReadOnlyList<IWebElement> tags = el.FindElements(By.TagName("td"));
                    contactsListHash.Add(new ContactData(
                        tags[2].Text,
                        tags[1].Text
                    )
                    {
                        Id = el.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(contactsListHash);
        }

        public string GetContactDetailsFromForm()
        {
            IWebElement content = driver.FindElement(By.XPath("//div[@id='content']"));
            return content.Text;

        }

        public ContactData GetContactInfoFromForm()
        {

            ContactData cd = new ContactData(
                GetNullIfEmpty(this.driver.FindElement(By.Name("firstname")).GetAttribute("Value")),
                GetNullIfEmpty(this.driver.FindElement(By.Name("lastname")).GetAttribute("Value"))
                    );
            cd.MiddleName = GetNullIfEmpty(this.driver.FindElement(By.Name("middlename")).GetAttribute("Value"));

            cd.Address = GetNullIfEmpty(this.driver.FindElement(By.Name("address")).Text);
            cd.MobiPhone = GetNullIfEmpty(this.driver.FindElement(By.Name("mobile")).GetAttribute("Value"));
            cd.HomePhone = GetNullIfEmpty(this.driver.FindElement(By.Name("home")).GetAttribute("Value"));
            cd.WorkPhone = GetNullIfEmpty(this.driver.FindElement(By.Name("work")).GetAttribute("Value"));
            cd.Fax = GetNullIfEmpty(this.driver.FindElement(By.Name("fax")).GetAttribute("Value"));
            cd.EMail = GetNullIfEmpty(this.driver.FindElement(By.Name("email")).GetAttribute("Value"));
            cd.EMail2 = GetNullIfEmpty(this.driver.FindElement(By.Name("email2")).GetAttribute("Value"));
            cd.EMail3 = GetNullIfEmpty(this.driver.FindElement(By.Name("email3")).GetAttribute("Value"));
            return cd;
        }


        public ContactData GetContactInfoFormTable(int v)
        {
            IList<IWebElement> lines = 
                driver.FindElements(
                          By.XPath("//table[@id='maintable']/tbody/tr[@name='entry']")
                       );

            IReadOnlyList<IWebElement> cells = lines[v].FindElements(By.TagName("td"));

            ContactData cd = new ContactData(
                cells[2].Text,
                cells[1].Text
                );
            cd.Address = cells[3].Text;
            cd.AllEMails = cells[4].Text;
            cd.AllPhones = cells[5].Text;
            return cd;
        }

        public int GetNumberOfSearchResults()
        {
            /*
            string s = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(s);
            return Int32.Parse(m.Value);
            */
            return Int32.Parse(driver.FindElement(By.TagName("label")).FindElement(By.TagName("span")).Text);
        }

        public MContactsHelper ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
            return this;
        }

        public MContactsHelper SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
            return this;
        }

        public MContactsHelper CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(drv => drv.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }
    }
}
