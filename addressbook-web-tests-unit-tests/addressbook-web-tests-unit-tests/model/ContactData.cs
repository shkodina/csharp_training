using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests_unit_tests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public ContactData() { }

        public ContactData(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
        public string MobiPhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Fax { get; set; }

        private string allPhones;
        public string AllPhones {
            get {
                if (allPhones == null)
                {
                    allPhones = CleanUpPhone(HomePhone) 
                                + CleanUpPhone(MobiPhone)
                                + CleanUpPhone(WorkPhone).Trim();
                }
                return allPhones;
            }
            
            set { allPhones = value; } 
        }

        private string CleanUpPhone(string phone)
        {
            //return new Regexp()
            return phone.Replace(" ", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("-", "")
                        + "\r\n";
        }

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
