using System;
using OpenQA.Selenium;

namespace addressbook_web_tests_unit_tests
{
    public class MGroupsHelper : MBaseHelper
    {
        public MGroupsHelper(IWebDriver driver, AppManager app) : base(driver, app)
        {
        }

        public MGroupsHelper GoToGroups()
        {

            this.AppManager.mNavyHelper.GoToByName("groups");
            return this;
        }

        public MGroupsHelper InitCreationNewGroup()
        {
            this.SubmitByClick("new");
            return this;
        }

        public MGroupsHelper FillNewGroupFields(GroupData group)
        {
            this.FillInputByName("group_name", group.Name);
            this.FillInputByName("group_header", group.Header);
            this.FillInputByName("group_footer", group.Header);
            return this;
        }

        public MGroupsHelper SubmitGroupCreation()
        {
            this.SubmitByClick("submit");
            return this;
        }

        public bool IsGroupExists()
        {
            GoToGroups();
            //return IsElementPresent(By.Name("selected[]"));
            return IsElementPresent(By.XPath("//span[@class='group']"));
        }

        public MGroupsHelper SubmitDeleteGroup()
        {
            driver.FindElement(By.XPath("(//input[@name='delete'])[2]")).Click();
            return this;
        }

         public MGroupsHelper SelectGroup(int v)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + v + "]")).Click();
            return this;
        }

        public MGroupsHelper SubmitEditGroup()
        {
            this.SubmitByClick("edit");
            return this;
        }

        public MGroupsHelper SubmitUpdateGroup()
        {
            this.SubmitByClick("update");
            return this;
        }
    }
}
