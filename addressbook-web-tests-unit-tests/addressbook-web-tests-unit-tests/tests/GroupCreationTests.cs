using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class GroupCreationTests : BaseTests
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
    }
}