using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests_unit_tests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string name = null;
        private string surname = null;
        private string id = null;
        public ContactData() { }

        public ContactData(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Id { get => id; set => id = value; }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
                return 1;

            if (Name.CompareTo(other.Name) != 0)
                return Name.CompareTo(other.Name);
            else
                return Surname.CompareTo(other.Surname);
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
            if (Object.ReferenceEquals(this, other))
                return true;
            return Name == other.Name && Surname == other.Surname;
        }


        public override string ToString()
        {
            return "Surname " + Surname + " Name: " + Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Surname.GetHashCode();
        }
    }
}
