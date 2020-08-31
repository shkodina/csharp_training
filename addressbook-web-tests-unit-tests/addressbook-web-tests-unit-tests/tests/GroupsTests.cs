using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class GroupsTests : BaseTestsAuth
    {

        public static IEnumerable<GroupData> RandomGroupProvider()
        {
            List<GroupData> groups = new List<GroupData>();

            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenRndStr(30))
                {
                    Header = GenRndStr(100),
                    Footer = GenRndStr(100)
                });
            }

            return groups;
        }

        [Test, TestCaseSource("RandomGroupProvider")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroups = app.mGroupsHelper.GetGroupsList();

            app.mGroupsHelper
                .GoToGroups()
                .InitCreationNewGroup()
                .FillNewGroupFields(group)
                .SubmitGroupCreation()
                .GoToGroups();

            oldGroups.Add(group);

            Assert.AreEqual(oldGroups.Count, app.mGroupsHelper.GetGroupsCount());

            List<GroupData> newGroups = app.mGroupsHelper.GetGroupsList();

            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void GroupRemovalTest()
        {
            int index_of_changed_group = 0;

            if (!app.mGroupsHelper.IsGroupExists())
                GroupCreationTest(RandomGroupProvider().GetEnumerator().Current);

            List<GroupData> oldGroups = app.mGroupsHelper.GetGroupsList();

            app.mGroupsHelper
                .GoToGroups()
                .SelectGroup(index_of_changed_group)
                .SubmitDeleteGroup()
                .GoToGroups();

            GroupData removedGroup = oldGroups[index_of_changed_group];
            oldGroups.RemoveAt(index_of_changed_group);

            Assert.AreEqual(oldGroups.Count, app.mGroupsHelper.GetGroupsCount());

            List<GroupData> newGroups = app.mGroupsHelper.GetGroupsList();

            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData gr in newGroups)
            {
                Assert.AreNotEqual(removedGroup.Id, gr.Id);
            }
        }

        [Test]
        public void GroupEditTest()
        {
            int index_of_changed_group = 0;

            if (!app.mGroupsHelper.IsGroupExists())
                GroupCreationTest(RandomGroupProvider().GetEnumerator().Current);

            List<GroupData> oldGroups = app.mGroupsHelper.GetGroupsList();
            GroupData editedGroup = oldGroups[index_of_changed_group];

            GroupData newGroup = new GroupData("group1 edited " + GenNewSuffixByCurTimeStamp());
            newGroup.Footer = "gr1 edited footer " + GenNewSuffixByCurTimeStamp();
            newGroup.Header = "gr1 edited fheader " + GenNewSuffixByCurTimeStamp();

            app.mGroupsHelper
                .GoToGroups()
                .SelectGroup(index_of_changed_group)
                .SubmitEditGroup()
                .FillNewGroupFields(newGroup)
                .SubmitUpdateGroup()
                .GoToGroups();

            oldGroups[index_of_changed_group] = newGroup;

            Assert.AreEqual(oldGroups.Count, app.mGroupsHelper.GetGroupsCount());

            List<GroupData> newGroups = app.mGroupsHelper.GetGroupsList();

            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData gr in newGroups)
            {
                if (gr.Id == editedGroup.Id)
                {
                    Assert.AreEqual(gr.Name, newGroup.Name);
                }
            }
        }
    }
}