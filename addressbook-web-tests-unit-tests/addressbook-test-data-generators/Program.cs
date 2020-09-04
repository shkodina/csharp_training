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
        static void Main(string[] args)
        {
            //string format = args[2];
            int countOfGroupsToCreate = Convert.ToInt32(args[0]);
            string fileName = args[1];
            string format = fileName.Split('.').Last();

            List<GroupData> groups = new List<GroupData>(GroupsTests.RandomGroupProvider(countOfGroupsToCreate));
            if (format == "xlsx")
            {
                WriteGroupsToExcelFile(groups, fileName);
            }
            else
            {
                StreamWriter writer = new StreamWriter(fileName);
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
       static void WriteGroupsToExcelFile(List<GroupData> groups, string fileName) 
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
        }
    }
}
