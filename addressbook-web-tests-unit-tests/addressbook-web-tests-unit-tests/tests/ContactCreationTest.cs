using NUnit.Framework;

namespace addressbook_web_tests_unit_tests
{
        [TestFixture]
        public class ContactCreationTests : BaseTests
        {


        [SetUp]
        public void SetupTest()
        {
            app.mLoginHelper.Login(new LoginData());
        }

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
    }
}
