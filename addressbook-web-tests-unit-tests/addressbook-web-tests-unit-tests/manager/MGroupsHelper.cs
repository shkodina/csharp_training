using System;
using System.Collections.Generic;
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

        private List<GroupData> groupsHash = null;

        public List<GroupData> GetGroupsList()
        {
            if (groupsHash == null)
            {
                groupsHash = new List<GroupData>();
                GoToGroups();
                foreach (IWebElement el in driver.FindElements(By.CssSelector("span.group")))
                {
                    groupsHash.Add(new GroupData(el.Text)
                        {
                            Id = el.FindElement(By.TagName("input")).GetAttribute("value")
                        }
                    );
                }
            }
            return new List<GroupData>(groupsHash);
        }

        public int GetGroupsCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
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
            groupsHash = null;
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
            groupsHash = null;
            return this;
        }

         public MGroupsHelper SelectGroup(int v)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (v + 1) + "]")).Click();
            return this;
        }
        public MGroupsHelper SelectGroup(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])")).Click();
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
            groupsHash = null;
            return this;
        }
    }
}
