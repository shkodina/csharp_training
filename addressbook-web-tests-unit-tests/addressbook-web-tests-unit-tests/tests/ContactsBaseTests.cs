using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using addressbook_web_tests_unit_tests.model;
using NUnit.Framework;

namespace addressbook_web_tests_unit_tests.tests
{
    public class ContactsBaseTests : BaseTestsAuth
    {
        [TearDown]
        public void CompareContacts_UI_vs_DB()
        {
            if (!PERFORM_LONG_UI_CHECKS) return;

            List<ContactData> fromUI = app.mContactsHelper.GoToContacts().GetContactsList();
            List<ContactData> fromDB = AddressBookDBHelper.GetAllContacts();
            fromUI.Sort();
            fromDB.Sort();
            Assert.AreEqual(fromUI, fromDB);

        }
    }
}
