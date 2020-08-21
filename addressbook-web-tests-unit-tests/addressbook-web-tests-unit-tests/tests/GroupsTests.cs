﻿using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class GroupsTests : BaseTestsAuth
    {
        [Test]
        public void GroupCreationTest()
        {
            List<GroupData> oldGroups = app.mGroupsHelper.GetGroupsList();

            GroupData group = new GroupData("group1");
            group.Footer = "gr1 footer";
            group.Header = "gr1 fheader";

            app.mGroupsHelper
                .GoToGroups()
                .InitCreationNewGroup()
                .FillNewGroupFields(group)
                .SubmitGroupCreation()
                .GoToGroups();

            oldGroups.Add(group);

            List<GroupData> newGroups = app.mGroupsHelper.GetGroupsList();

            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void GroupRemovalTest()
        {
            if (!app.mGroupsHelper.IsGroupExists())
                GroupCreationTest();

            app.mGroupsHelper
                .GoToGroups()
                .SelectGroup(0)
                .SubmitDeleteGroup()
                .GoToGroups();
        }

        [Test]
        public void GroupEditTest()
        {
            if (!app.mGroupsHelper.IsGroupExists())
                GroupCreationTest();

            List<GroupData> oldGroups = app.mGroupsHelper.GetGroupsList();

            int index_of_changed_group = 0;

            GroupData group = new GroupData("group1 edited " + GenNewSuffixByCurTimeStamp());
            group.Footer = "gr1 edited footer " + GenNewSuffixByCurTimeStamp();
            group.Header = "gr1 edited fheader " + GenNewSuffixByCurTimeStamp();

            app.mGroupsHelper
                .GoToGroups()
                .SelectGroup(index_of_changed_group)
                .SubmitEditGroup()
                .FillNewGroupFields(group)
                .SubmitUpdateGroup()
                .GoToGroups();

            oldGroups[index_of_changed_group] = group;

            List<GroupData> newGroups = app.mGroupsHelper.GetGroupsList();

            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}