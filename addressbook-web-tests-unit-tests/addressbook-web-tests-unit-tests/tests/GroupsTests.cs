using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;


namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class GroupsTests : BaseTestsAuth
    {
       public static IEnumerable<GroupData> GroupDataFromCSVFile()
        {
            List<GroupData> groups = new List<GroupData>();
            foreach (string l in File.ReadAllLines(new BaseData().TestDataBaseAddress + "groups.csv"))
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }

            return groups;
        }
       public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();

            Excel.Application app = new Excel.Application();
            app.Visible = true;

            Excel.Workbook wb = app.Workbooks.Open(new BaseData().TestDataBaseAddress + "groups.xlsx");
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range = sheet.UsedRange;

            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }

            wb.Close();
            app.Visible = false;
            app.Quit();

            return groups;
        }
       
        /*
       public static IEnumerable<GroupData> GroupDataFromXMLFile()
        {
            return (List<GroupData>)
                new XmlSerializer(typeof(List<GroupData>)).
                    Deserialize(new StreamReader(new BaseData().TestDataBaseAddress + "groups.xml"));
        }
       public static IEnumerable<GroupData> GroupDataFromJSONFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>
                (
                    File.ReadAllText(new BaseData().TestDataBaseAddress + "groups.json")
                );
        }
       */
        public static IEnumerable<GroupData> RandomGroupProvider(int count = 5)
        {
            List<GroupData> groups = new List<GroupData>();

            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(GenRndStr(30))
                {
                    Header = GenRndStr(100),
                    Footer = GenRndStr(100)
                });
            }

            return groups;
        }

        public static IEnumerable<GroupData> GroupsCreator()
        {
            //return RandomGroupProvider();
            //return GroupDataFromCSVFile();
            //return GroupDataFromExcelFile();

            //return GroupDataFromXMLFile();
            //return ReadDataFromXMLFile<GroupData>("groups.xml");

            //return GroupDataFromJSONFile();
            return ReadDataFromJSONFile<GroupData>("groups.json");
        }

        [Test, TestCaseSource("GroupsCreator")]
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
                GroupCreationTest(GroupsCreator().ElementAt(0));

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
                GroupCreationTest(GroupsCreator().ElementAt(0));

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