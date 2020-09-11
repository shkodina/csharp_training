using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using addressbook_web_tests_unit_tests.model;
using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
    public class GroupBaseTests : BaseTestsAuth
    {
        [TearDown]
        public void CompareGroups_UI_vs_DB()
        {
            if (!PERFORM_LONG_UI_CHECKS) return;

            List<GroupData> fromUI = app.mGroupsHelper.GoToGroups().GetGroupsList();
            List<GroupData> fromDB = AddressBookDBHelper.GetAllGroups();
            fromUI.Sort();
            fromDB.Sort();
            Assert.AreEqual(fromUI, fromDB);

        }
    }
}
