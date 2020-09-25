using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace mantis_test
{
    [TestFixture]
    public class BaseTests
    {
        static public bool PERFORM_LONG_UI_CHECKS = false;

        protected AppManager app;

        [SetUp]
        public void SetupTest()
        {
            app = AppManager.GetInstance();           
        }

        protected string GenNewSuffixByCurTimeStamp()
        {
            return System.DateTime.Now
                        .ToString()
                        .Replace(":", ".")
                        .Replace("/", ".")
                        .Replace(" ", ".");
        }

        private static Random rnd = new Random();
        public static string GenRndStr(int max, bool isAlpha = false)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder strb = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                int c = 32 + Convert.ToInt32(rnd.NextDouble() * 223);
                if (isAlpha)
                {
                    if (
                        (c > 57 && c < 65)
                        ||
                        (c > 90 && c < 97)
                        ||
                        (c > 122 && c < 192)
                        )
                    {
                        c -= 7;
                    }
                }

                strb.Append(Convert.ToChar(c));
            }
            return strb.ToString();
        }


        public static string GenRndPhone(int min, int max, bool useDelims = true)
        {
            int digi_count = Convert.ToInt32(rnd.NextDouble() * (max - min));
            StringBuilder strb = new StringBuilder();

            for (int i = 0; i < digi_count; i++)
            {
                if (useDelims)
                {
                    if(rnd.NextDouble() > 0.85)
                    { // time for delimeter
                        switch(Convert.ToInt32(rnd.NextDouble() * 4))
                        {
                            case 0: strb.Append(" "); continue;
                            case 1: strb.Append("+"); continue;
                            case 2: strb.Append("("); continue;
                            case 3: strb.Append(")"); continue;
                            default: strb.Append(" "); continue;

                        }
                    }
                }
                strb.Append(GenDigit());
            }
            return strb.ToString();
        }

        private static char GenDigit()
        {
            // didgits codes = [48:57]
            return Convert.ToChar(48 + Convert.ToInt32(rnd.NextDouble() * (57 - 48 + 1)));
        }

        public static IEnumerable<T> ReadDataFromXMLFile<T>(string fileName)
        {
            return (List<T>)
                new XmlSerializer(typeof(List<T>)).
                    Deserialize(new StreamReader(BaseData.TestDataBaseAddress + fileName));
        }
        public static IEnumerable<T> ReadDataFromJSONFile<T>(string fileName)
        {
            return JsonConvert.DeserializeObject<List<T>>
                (
                    File.ReadAllText(BaseData.TestDataBaseAddress + fileName)
                );
        }

    }
}
