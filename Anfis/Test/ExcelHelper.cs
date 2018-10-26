using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Anfis.Models;
using Microsoft.Office.Interop.Excel;

namespace Test
{
    public class ExcelHelper
    {
        public static List<DataItem> ExtractData(int numEl)
        {
            var excelPath = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "*.xls*",
                SearchOption.AllDirectories).FirstOrDefault();
            if (excelPath == null)
                throw new Exception("Excel file does not exist");
            var xlApp = new Application();
            var xlWorkbook = xlApp.Workbooks.Open(excelPath);
            var xlWorksheet = xlWorkbook.Sheets[1];
            var xlRange = xlWorksheet.UsedRange;

            var dataItems = new List<DataItem>();

            for (var i = 2; i <= xlRange.Rows.Count; i++)
            {
                var dataItem = new DataItem
                {
                    Inputs = new List<double>()
                };
                for (var j = 2; j <= xlRange.Columns.Count; j++)
                {
                    if (xlRange.Cells[i, j]?.Value == null) continue;
                    if (j < xlRange.Columns.Count)
                        dataItem.Inputs.Add(double.Parse(xlRange.Cells[i, j].Value.ToString()));
                    else
                        dataItem.Output = double.Parse(xlRange.Cells[i, j].Value.ToString());
                }
                if (dataItem.Inputs.Count == 8 && Math.Abs(dataItem.Output) > 0.000001)
                    dataItems.Add(dataItem);
                if (numEl > 0 && dataItems.Count > numEl)
                    break;
            }
           
            xlWorkbook.Close();
            xlApp.Quit();

            return dataItems;
        }
    }
}
