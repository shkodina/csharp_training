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
    }
}
