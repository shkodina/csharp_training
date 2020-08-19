using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class GroupsTests : BaseTestsAuth
    {
   
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
        public void SafetyGroupRemovalTest()
        {
            if (!app.mGroupsHelper.IsGroupExists())
                GroupCreationTest();

            GroupRemovalTest();
        }

        [Test]
        public void GroupEditTest()
        {

            GroupData group = new GroupData("group1 edited " + GenNewSuffixByCurTimeStamp());
            group.Footer = "gr1 edited footer " + GenNewSuffixByCurTimeStamp();
            group.Header = "gr1 edited fheader " + GenNewSuffixByCurTimeStamp();

            app.mGroupsHelper
                .GoToGroups()
                .SelectGroup(1)
                .SubmitEditGroup()
                .FillNewGroupFields(group)
                .SubmitUpdateGroup()
                .GoToGroups();
        }

        [Test]
        public void SafetyGroupEditTest()
        {
            if (!app.mGroupsHelper.IsGroupExists())
                GroupCreationTest();

            GroupEditTest();
        }
    }
}