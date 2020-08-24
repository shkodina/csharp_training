using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests_unit_tests
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        private string name = null;
        private string header = null;
        private string footer = null;
        private string id = null;

        public GroupData(string name)
        {
            this.name = name;
        }

        public string Header { get => header; set => header = value; }
        public string Footer { get => footer; set => footer = value; }
        public string Name { get => name; set => name = value; }
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
    }
}
