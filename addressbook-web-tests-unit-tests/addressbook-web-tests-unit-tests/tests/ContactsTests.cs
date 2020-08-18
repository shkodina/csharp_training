using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
    [TestFixture]
    public class ContactsTests : BaseTestsAuth
    {

        [Test]
        public void ContactCreationTest()
        {
            ContactData cd = new ContactData("Alex", "Piper");
            app.mContactsHelper
                .GoToContacts()
                .InitCreationNewContact()
                .FillNewContactFields(cd)
                .SubmitContactCreation()
                .GoToContacts();

            //Thread.Sleep(2000);
        }

        [Test]
        public void ContactEditTest()
        {
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
            app.mContactsHelper
                .GoToContacts()
                .SelectContact(0)
                .SubmitDeleteContact()
                .GoToContacts();
        }
    }
}
