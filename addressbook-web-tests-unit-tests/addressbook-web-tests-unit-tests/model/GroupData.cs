using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace addressbook_web_tests_unit_tests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        private string name = null;
        private string header = null;
        private string footer = null;
        private string id = null;
        private string deprecated = null;

        public GroupData()
        {
        }

       public GroupData(string name)
        {
            this.name = name;
        }

        [Column(Name = "group_header")]
        public string Header { get => header; set => header = value; }

        [Column(Name = "group_footer")]
        public string Footer { get => footer; set => footer = value; }

        [Column(Name = "group_name")]
        public string Name { get => name; set => name = value; }
        

        [Column(Name = "deprecated")]
        public string Deprecated { get => deprecated; set => deprecated = value; }

        [Column(Name = "group_id"), PrimaryKey, Identity] 
        public string Id { get => id; set => id = value; }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
                return 1;
            return Name.CompareTo(other.Name);
        }

        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
            if (Object.ReferenceEquals(this, other))
                return true;
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

       
        public override string ToString()
        {
            return System.String.Format("Name({0})Header[{1}]Footer[{2}]",
                Name,
                Header,
                Footer
                );
        }
        
    }
}
