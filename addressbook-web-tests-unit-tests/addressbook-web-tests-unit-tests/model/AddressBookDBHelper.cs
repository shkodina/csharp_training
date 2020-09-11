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
    }
}
