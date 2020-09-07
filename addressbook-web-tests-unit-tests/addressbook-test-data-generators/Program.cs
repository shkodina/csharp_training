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
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        public delegate void XMLWriterDelegate<T>(System.Collections.IList elements, StreamWriter writer); //declaring a delegate
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                System.Console.Out.WriteLine("Descr: util for generating random data for addressbook tests");
                System.Console.Out.WriteLine("Usage: app.exe typeOfData countOfElements outPutFileName [typeOfFile]");
                System.Console.Out.WriteLine("typeOfData : groups | contacts");
                System.Console.Out.WriteLine("countOfElements : count of elements to create (int32)");
                System.Console.Out.WriteLine("outPutFileName : full path to file to save to");
                System.Console.Out.WriteLine("typeOfFile : xml | json : if not set used postfix of filename (.xml | .json)");
                System.Environment.Exit(1);
            }

            string typeOfData = args[0];
            int countOfElementsToCreate = Convert.ToInt32(args[1]);
            string fileName = args[2];
            string format = null;
            if (args.Length > 3)
                format = args.ElementAt(3);
            else
                format = fileName.Split('.').Last();

            System.Collections.IList elements = null;

            StreamWriter writer = new StreamWriter(fileName);
            XMLWriterDelegate<BaseData> xmlWriter = null;

            switch (typeOfData)
            {
                case "contacts":
                    elements = new List<ContactData>(ContactsTests.RandomContactProvider(countOfElementsToCreate));
                    xmlWriter = WriteToXMLFile<ContactData>;
                    break;
                case "groups":
                    elements = new List<GroupData>(GroupsTests.RandomGroupProvider(countOfElementsToCreate));
                    xmlWriter = WriteToXMLFile<GroupData>; 
                    break;
                default:
                    System.Console.Out.WriteLine("Unknown dataType: " + typeOfData);
                    System.Environment.Exit(1);
                    break;
            }

            switch (format)
            {
                case "xml":
                    xmlWriter(elements, writer);
                    break;
                case "json":
                    WriteToJSONFile(elements, writer);
                    break;
                default:
                    System.Console.Out.WriteLine("Unknown format: " + format);
                    System.Environment.Exit(1);
                    break;
            }

            writer.Close();
        }

        static void WriteToXMLFile<T>(System.Collections.IList elements, StreamWriter writer) 
        {
            if (elements == null || writer == null) return;
            new XmlSerializer(typeof(List<T>)).Serialize(writer, elements);
        }
       static void WriteToJSONFile(System.Collections.IList elements, StreamWriter writer) 
       {
            if (elements == null || writer == null) return;
            writer.Write(JsonConvert.SerializeObject(elements, Newtonsoft.Json.Formatting.Indented));
        }






        static void WriteGroupsToCSVFile(System.Collections.IList groups, StreamWriter writer)
        {
            foreach (GroupData gr in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                   gr.Name, gr.Header, gr.Footer));
            }
        }
        static void WriteGroupsToExcelFile(System.Collections.IList groups, string fileName) 
       {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (GroupData gr in groups)
            {
                sheet.Cells[row, 1] = gr.Name;
                sheet.Cells[row, 2] = gr.Header;
                sheet.Cells[row, 3] = gr.Footer;
                row++;
            }

            string fullPath = Path.Combine(new BaseData().TestDataBaseAddress, fileName);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }
    }
}
