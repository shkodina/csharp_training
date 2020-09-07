using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using addressbook_web_tests_unit_tests.utils;

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
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }
        public string MobiPhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Fax { get; set; }

        public string EMail { get; set; }
        public string EMail2 { get; set; }
        public string EMail3 { get; set; }

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

        private string allEMails;
        public string AllEMails { 
        get {
                if (allEMails == null)
                {
                    allEMails = (
                                  (EMail == null ? "" : (EMail + "\r\n"))
                                + (EMail2 == null ? "" : (EMail2 + "\r\n"))
                                + (EMail3 == null ? "" : (EMail3 + "\r\n"))
                                ).Trim();
                }
                return allEMails;
            }

        set { allEMails = value; } 
        }

        private string CleanUpPhone(string phone)
        {
            if (phone == null)
                return "";

            return Regex.Replace(phone, "[ ()-]", "") + "\r\n";
            /*
            return phone.Replace(" ", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("-", "")
                        + "\r\n";
            */
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
            string endl = "\r\n";
            string asText = "";

            if (Name != null)
                asText = asText.Trim() + " " + Name;
            if (MiddleName != null)
                asText = asText.Trim() + " " + MiddleName;
            if (Surname != null)
                asText = asText.Trim() + " " + Surname;

            asText = asText + endl;

            if (Address != null)
                asText = asText + Address + endl;

            asText = asText + endl;

            if (HomePhone != null)
                asText = asText + "H: " + HomePhone + endl;
            if (MobiPhone != null)
                asText = asText + "M: " + MobiPhone + endl;
            if (WorkPhone != null)
                asText = asText + "W: " + WorkPhone + endl;

            asText = asText + endl;

            if (EMail != null)
                asText = asText + EMail + endl;
            if (EMail2 != null)
                asText = asText + EMail2 + endl;
            if (EMail3 != null)
                asText = asText + EMail3 + endl;

            return asText.Trim();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Surname.GetHashCode();
        }
    }
}
