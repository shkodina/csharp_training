using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class GroupsTests : BaseTests
    {
        [SetUp]
        public void SetupTest()
        {
            app.mLoginHelper.Login(new LoginData());

        }

        [Test]
        public void GroupCreationTest()
        {

            GroupData group = new GroupData("group1");
            group.Footer = "gr1 footer";
            group.Header = "gr1 fheader";

            app.mGroupsHelper
                .GoToGroups()
                .InitCreationNewGroup()
                .FillNewGroupFields(group)
                .SubmitGroupCreation()
                .GoToGroups();
 
            //Thread.Sleep(2000);
        }

        [Test]
        public void GroupRemovalTest()
        {
            app.mGroupsHelper
                .GoToGroups()
                .SelectGroup(1)
                .SubmitDeleteGroup()
                .GoToGroups();
        }

        [Test]
        public void GroupEditTest()
        {

            GroupData group = new GroupData("group1edited");
            group.Footer = "gr1 edited footer";
            group.Header = "gr1 edited fheader";

            app.mGroupsHelper
                .GoToGroups()
                .SelectGroup(1)
                .SubmitEditGroup()
                .FillNewGroupFields(group)
                .SubmitUpdateGroup()
                .GoToGroups();
        }
    }
}