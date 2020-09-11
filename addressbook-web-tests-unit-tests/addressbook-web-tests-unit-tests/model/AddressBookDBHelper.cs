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
                fromDB = (from g in db.Contacts select g).ToList();
            }
            return fromDB;
        }
 
        public static List<ContactData> GetContactsInGroup(GroupData gr)
        {
            List<ContactData> fromDB = null;
            using (AddressBookDB db = new AddressBookDB())
            {
                fromDB = (from c in db.Contacts 
                          from gcr in db.GCR.Where(p => p.GroupId == gr.Id && p.ContactId == c.Id)
                              select c).ToList();
            }
            return fromDB;
        }
 
    }
}
