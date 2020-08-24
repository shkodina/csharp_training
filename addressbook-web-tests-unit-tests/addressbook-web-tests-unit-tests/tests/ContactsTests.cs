using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class ContactsTests : BaseTestsAuth
    {

        [Test]
        public void ContactCreationTest()
        {
            List<ContactData> oldList = app.mContactsHelper.GetContactsList();

            ContactData cd = new ContactData("Alex", "SuperPiper");
            app.mContactsHelper
                .GoToContacts()
                .InitCreationNewContact()
                .FillNewContactFields(cd)
                .SubmitContactCreation()
                .GoToContacts();

            oldList.Add(cd);

            List<ContactData> newList = app.mContactsHelper.GetContactsList();

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

            List<ContactData> oldList = app.mContactsHelper.GetContactsList();

            int index_for_remove = 0;

            app.mContactsHelper
                .GoToContacts()
                .SelectContact(index_for_remove)
                .SubmitDeleteContact()
                .GoToContacts();

            oldList.RemoveAt(index_for_remove);

            List<ContactData> newList = app.mContactsHelper.GetContactsList();

            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
