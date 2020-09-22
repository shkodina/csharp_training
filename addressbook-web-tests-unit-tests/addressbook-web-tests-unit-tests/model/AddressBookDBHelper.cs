using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests_unit_tests.model
{
    public class AddressBookDBHelper
    {
        public static List<GroupData> GetAllGroups()
        {
            List<GroupData> fromDB = null;
            using (AddressBookDB db = new AddressBookDB())
            {
                fromDB = (from g in db.Groups select g).ToList();
            }
            return fromDB;
        }
        public static List<ContactData> GetAllContacts()
        {
            List<ContactData> fromDB = null;
            using (AddressBookDB db = new AddressBookDB())
            {
                fromDB = (from g in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select g).ToList();
            }
            return fromDB;
        }
 
        public static List<ContactData> GetContactsInGroup(GroupData gr)
        {
            List<ContactData> fromDB = null;
            using (AddressBookDB db = new AddressBookDB())
            {
                fromDB = (from c in db.Contacts 
                          from gcr in db.GCR.Where(x => x.GroupId == gr.Id && x.ContactId == c.Id)
                              select c).Distinct().ToList();
            }
            return fromDB;
        }

        public static List<GroupData> GetGroupsByContact(ContactData cd)
        {
            List<GroupData> fromDB = null;
            using (AddressBookDB db = new AddressBookDB())
            {
                fromDB = (from gr in db.Groups
                          from gcr in db.GCR.Where(x => x.GroupId == gr.Id && x.ContactId == cd.Id)
                          select gr).Distinct().ToList();
            }
            return fromDB;
        }
    }
}
