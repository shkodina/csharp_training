using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using addressbook_web_tests_unit_tests;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamWriter writer = new StreamWriter(args[1]);
            //string format = args[2];
            string format = args[1].Split('.').Last();

            List<GroupData> groups = new List<GroupData>(GroupsTests.RandomGroupProvider(Convert.ToInt32(args[0])));

            switch (format)
            {
                case "csv":
                    WriteGroupsToCSVFile(groups, writer);
                    break;
                case "xml":
                    WriteGroupsToXMLFile(groups, writer);
                    break;
               case "json":
                    WriteGroupsToJSONFile(groups, writer);
                    break;
                default:
                    System.Console.Out.WriteLine("Unknown format: " + format);
                    break;
            }
            
            writer.Close();
        }

        static void WriteGroupsToCSVFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData gr in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                   gr.Name, gr.Header, gr.Footer));
            }
        }
        static void WriteGroupsToXMLFile(List<GroupData> groups, StreamWriter writer) 
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }
       static void WriteGroupsToJSONFile(List<GroupData> groups, StreamWriter writer) 
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
