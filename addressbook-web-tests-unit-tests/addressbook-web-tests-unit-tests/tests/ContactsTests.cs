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
    public class ContactsTests : BaseTestsAuth
    {
        public static IEnumerable<ContactData> ContactDataFromXMLFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>)).
                    Deserialize(new StreamReader(new BaseData().TestDataBaseAddress + "contacts.xml"));
        }
        public static IEnumerable<ContactData> ContactDataFromJSONFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>
                (
                    File.ReadAllText(new BaseData().TestDataBaseAddress + "contacts.json")
                );
        }

        public static IEnumerable<ContactData> RandomContactProvider(int count = 5)
        {
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(GenRndStr(30, true), GenRndStr(30, true))
                {
                    MiddleName = GenRndStr(30, true),
                    Address = GenRndStr(100),
                    EMail = GenRndStr(10, true) + "@" + GenRndStr(3, true) + "." + GenRndStr(2, true),
                    EMail2 = GenRndStr(10, true) + "@" + GenRndStr(3, true) + "." + GenRndStr(2, true),
                    EMail3 = GenRndStr(10, true) + "@" + GenRndStr(3, true) + "." + GenRndStr(2, true),
                    MobiPhone = GenRndPhone(8, 20),
                    HomePhone = GenRndPhone(8, 20),
                    WorkPhone = GenRndPhone(8, 20),
                });
            }

            return contacts;
        }

        public static IEnumerable<ContactData> ContactProvider()
        {
            return ContactDataFromXMLFile();
        }

        [Test,TestCaseSource("ContactProvider")]
        public void ContactCreationTest(ContactData cd)
        {
            List<ContactData> oldList = app.mContactsHelper.GoToContacts().GetContactsList();

            app.mContactsHelper
                .GoToContacts()
                .InitCreationNewContact()
                .FillNewContactFields(cd)
                .SubmitContactCreation()
                .GoToContacts();

            oldList.Add(cd);

            List<ContactData> newList = app.mContactsHelper.GoToContacts().GetContactsList();

            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);

        }

        [Test]
        public void ContactEditTest()
        {
            if (!app.mContactsHelper.IsContactExist())
                ContactCreationTest(ContactProvider().GetEnumerator().Current);

            ContactData cd = 
                new ContactData("AlexEdit " + GenNewSuffixByCurTimeStamp()
                                , "PiperEdit" + GenNewSuffixByCurTimeStamp());

            app.mContactsHelper
                .GoToContacts()
                .InitEditContact(0)
                .FillNewContactFields(cd)
                .SubmitContactUpdate()
                .GoToContacts();
        }

        [Test]
        public void ContactRemovalTest()
        {
            
            if (!app.mContactsHelper.IsContactExist())
                ContactCreationTest(ContactProvider().GetEnumerator().Current);

            List<ContactData> oldList = app.mContactsHelper.GoToContacts().GetContactsList();

            int index_for_remove = 0;

            app.mContactsHelper
                .GoToContacts()
                .SelectContact(index_for_remove)
                .SubmitDeleteContact()
                .GoToContacts();

            oldList.RemoveAt(index_for_remove);

            List<ContactData> newList = app.mContactsHelper.GoToContacts().GetContactsList();

            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void CheckContactInformationTest()
        {
            int index = 0;
            ContactData fromTable = app.mContactsHelper
                                        .GoToContacts()
                                        .GetContactInfoFormTable(index);

            ContactData fromForm = app.mContactsHelper
                                        .GoToContacts()
                                        .InitEditContact(index)
                                        .GetContactInfoFromForm();



            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }
        [Test]

        public void CheckContactDetailsTest()
        {
            int cnt = app.mContactsHelper.GoToContacts().GetNumberOfSearchResults();
            for (int index = 0; index < cnt; index++)
            {
                CheckContactDetails(index);
            }

        }

        public void CheckContactDetails(int index)
        {
            ContactData fromForm = app.mContactsHelper
                                        .GoToContacts()
                                        .InitEditContact(index)
                                        .GetContactInfoFromForm();

            string detailsInfoTxt = app.mContactsHelper
                                        .GoToContacts()
                                        .IniOpenDetailContact(index)
                                        .GetContactDetailsFromForm();



            Assert.AreEqual(fromForm.ToString(), detailsInfoTxt);
        }
    }
}
