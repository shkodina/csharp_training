using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;


namespace addressbook_web_tests_unit_tests
{
    [Table(Name = "address_in_groups")]
    public class GroupContactRelationData : IEquatable<GroupContactRelationData>, IComparable<GroupContactRelationData>
    {

        [Column(Name = "group_id")]
        public string GroupId { get; }
        [Column(Name = "id")]
        public string ContactId { get; }
        public int CompareTo(GroupContactRelationData other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(GroupContactRelationData other)
        {
            throw new NotImplementedException();
        }
    }
}
