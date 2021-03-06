﻿using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using addressbook_web_tests_unit_tests.model;

namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class GroupsTests : GroupBaseTests
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
            //return RandomGroupProvider(5);
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
            

            //List<GroupData> oldGroups = app.mGroupsHelper.GetGroupsList();
            List<GroupData> oldGroups = AddressBookDBHelper.GetAllGroups();

            app.mGroupsHelper
                .GoToGroups()
                .InitCreationNewGroup()
                .FillNewGroupFields(group)
                .SubmitGroupCreation()
                .GoToGroups();

            oldGroups.Add(group);

            Assert.AreEqual(oldGroups.Count, app.mGroupsHelper.GetGroupsCount());

            //List<GroupData> newGroups = app.mGroupsHelper.GetGroupsList();
            List<GroupData> newGroups = AddressBookDBHelper.GetAllGroups();

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

            //List<GroupData> oldGroups = app.mGroupsHelper.GetGroupsList();
            List<GroupData> oldGroups = AddressBookDBHelper.GetAllGroups();

            GroupData removedGroup = oldGroups[index_of_changed_group];

            oldGroups.RemoveAt(index_of_changed_group);

            app.mGroupsHelper
                .GoToGroups()
                .SelectGroup(removedGroup.Id)
                .SubmitDeleteGroup()
                .GoToGroups();

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

            //List<GroupData> oldGroups = app.mGroupsHelper.GetGroupsList();
            List<GroupData> oldGroups = AddressBookDBHelper.GetAllGroups();
            GroupData editedGroup = oldGroups[index_of_changed_group];

            GroupData newGroup = new GroupData("group1 edited " + GenNewSuffixByCurTimeStamp());
            newGroup.Footer = "gr1 edited footer " + GenNewSuffixByCurTimeStamp();
            newGroup.Header = "gr1 edited fheader " + GenNewSuffixByCurTimeStamp();

            app.mGroupsHelper
                .GoToGroups()
                .SelectGroup(editedGroup.Id)
                .SubmitEditGroup()
                .FillNewGroupFields(newGroup)
                .SubmitUpdateGroup()
                .GoToGroups();

            oldGroups[index_of_changed_group] = newGroup;

            Assert.AreEqual(oldGroups.Count, app.mGroupsHelper.GetGroupsCount());

            //List<GroupData> newGroups = app.mGroupsHelper.GetGroupsList();
            List<GroupData> newGroups = AddressBookDBHelper.GetAllGroups();

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

        [Test]
        public void TestAddingContactToGroup()
        {
            List<GroupData> grList = AddressBookDBHelper.GetAllGroups();
            List<ContactData> cdList = AddressBookDBHelper.GetAllContacts();

            if(cdList.Count == 0)
            {
                new ContactsTests().ContactCreationTest(ContactsTests.ContactProvider().GetEnumerator().Current);
                cdList = AddressBookDBHelper.GetAllContacts();
            }

            if (grList.Count == 0)
            {
                GroupCreationTest(GroupsCreator().ElementAt(0));
                grList = AddressBookDBHelper.GetAllGroups();
            }

            // найдем контакт, который НЕ во всех группах
            foreach(ContactData cd in cdList)
            {
                List<GroupData> cdGroups = AddressBookDBHelper.GetGroupsByContact(cd);

                if (cdGroups.Count != grList.Count) // где то есть группа БЕЗ этого контакта
                {
                    GroupData gr = null;
                    if (cdGroups.Count == 0) // контакта нет ни в одной группе
                    {
                        gr = grList[0];                     
                    }
                    else
                    {
                        gr = grList.Except(cdGroups).First();
                    }

                    TestAddingContactToGroupActionPart(gr, cd);
                    return;

                }

                /*
                foreach (GroupData gr in cdGroups)
                {
                    System.Console.Out.WriteLine(string.Format("For contact : {0} -> group: {1}", cd.Name, gr.Name));
                }
                */
            }

            // если мы тут, значит все контакты во всех группах
            // можно создать контакт и впихнуть в любую группу, или создать новую группу и взять любой контакт
            // ну а раз тест в классе групп, то создадим группу и впихнем в нее первый контакт

             
            GroupCreationTest(GroupsCreator().ElementAt(0));
            TestAddingContactToGroupActionPart(
                AddressBookDBHelper.GetAllGroups().Except(AddressBookDBHelper.GetGroupsByContact(cdList[0])).First(), 
                cdList[0]);

        }

        private void TestAddingContactToGroupActionPart(GroupData gr, ContactData cd)
        {
            List<ContactData> oldList = AddressBookDBHelper.GetContactsInGroup(gr);

            app.mContactsHelper.GoToContacts()
                        .ClearGroupFilter()
                        .SelectContact(cd.Id)
                        .SelectGroupToAdd(gr.Name)
                        .CommitAddingContactToGroup();

            List<ContactData> newList = AddressBookDBHelper.GetContactsInGroup(gr);

            oldList.Add(cd);

            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void TestRemovalContactFromGroup()
        {
            // получить список групп (из БД)
            // найти группу где есть хоть один контакт (из БД)
            // открыть контакты и выбрать эту группу в фильтре
            // выбрать контакт в списке и удалить его
            // получить новый список контактов в выбранно группе
            // удалить удаленный контакт из первого списка и сравнить

            List<GroupData> grList = AddressBookDBHelper.GetAllGroups();
            List<ContactData> cdList = AddressBookDBHelper.GetAllContacts();

            if (cdList.Count == 0)
            {
                new ContactsTests().ContactCreationTest(ContactsTests.ContactProvider().GetEnumerator().Current);
                cdList = AddressBookDBHelper.GetAllContacts();
            }

            if (grList.Count == 0)
            {
                GroupCreationTest(GroupsCreator().ElementAt(0));
                grList = AddressBookDBHelper.GetAllGroups();
            }

            ContactData victumContact = null;
            GroupData victumGroup = null;

            List<ContactData> oldList = null;

            foreach (GroupData gr in AddressBookDBHelper.GetAllGroups())
            {
                oldList = AddressBookDBHelper.GetContactsInGroup(gr);
                if (oldList.Count > 0)
                {
                    victumGroup = gr;
                    break;
                }
                    
            }
            
            if (victumGroup == null)
            {
                // All groups are empty
                Assert.Warn("All groups are empty");
                victumGroup = AddressBookDBHelper.GetAllGroups().First();
                victumContact = AddressBookDBHelper.GetAllContacts().First();
                TestAddingContactToGroupActionPart(victumGroup,  victumContact);

            }
            else
            {
                victumContact = oldList.First();
            }
            
           

            // а теперь сами проверки по удалению группы...

            app.mContactsHelper.GoToContacts()
                                .SelectGroupFilter(victumGroup.Name)
                                .SelectContact(victumContact.Id)
                                .CommitRemoveContactFromGroup();


            List<ContactData> newList = AddressBookDBHelper.GetContactsInGroup(victumGroup);

            oldList.Remove(victumContact);

            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);
        }


        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUI = app.mGroupsHelper.GetGroupsList();
            DateTime end = DateTime.Now;
            
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupData> fromDB = AddressBookDBHelper.GetAllGroups();
            end = DateTime.Now;

            System.Console.Out.WriteLine(end.Subtract(start));





            System.Console.Out.WriteLine("Groups count = " + fromUI.Count);

            foreach (GroupData gr in fromDB)
            {
                System.Console.Out.WriteLine("For Group: " + gr.Name);
                foreach ( ContactData cd in AddressBookDBHelper.GetContactsInGroup(gr))
                {
                    System.Console.Out.WriteLine("Contact:: " + cd.Name);
                }
            }
        }
    }
}