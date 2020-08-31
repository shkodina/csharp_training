﻿using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class ContactsTests : BaseTestsAuth
    {

        [Test]
        public void ContactCreationTest()
        {
            List<ContactData> oldList = app.mContactsHelper.GoToContacts().GetContactsList();

            ContactData cd = new ContactData("Alex", "SuperPiper");
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
                ContactCreationTest();

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
                ContactCreationTest();

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
