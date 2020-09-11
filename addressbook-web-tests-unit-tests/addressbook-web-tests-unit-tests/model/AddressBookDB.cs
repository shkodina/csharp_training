using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace addressbook_web_tests_unit_tests.model
{
    public class AddressBookDB: LinqToDB.Data.DataConnection
    {
        public AddressBookDB() : base("AddressBook") { }

        public ITable<GroupData> Groups { get { return GetTable<GroupData>(); } }
        public ITable<ContactData> Contacts { get { return GetTable<ContactData>(); } }
        public ITable<GroupContactRelationData> GCR { get { return GetTable<GroupContactRelationData>(); } }
    }
}
