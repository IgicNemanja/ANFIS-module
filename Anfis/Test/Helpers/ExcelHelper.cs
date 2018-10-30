using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Anfis.Models;
using Microsoft.Office.Interop.Excel;

namespace Test.Helpers
{
    public class ExcelHelper
    {
        public static DataItem[] ExtractDataItems(int startInputCol, int endInputCol, int outputCol)
        {
            var excelPath = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "*.xls*",
                SearchOption.AllDirectories).FirstOrDefault();
            if (excelPath == null)
                throw new Exception("Excel file does not exist");
            var app = new Application();
            var workbook = app.Workbooks.Open(excelPath);
            var worksheet = (Worksheet)workbook.Sheets[1];

            var dataRange = worksheet.UsedRange;
            Range c1 = worksheet.Cells[2, startInputCol + 1];
            Range c2 = worksheet.Cells[dataRange.Rows.Count, dataRange.Columns.Count];
            var range = worksheet.get_Range(c1, c2);
            object[,] rawValues = range.get_Value(Type.Missing);
            double[,] values = new double[rawValues.GetLength(0), rawValues.GetLength(1)];
            Array.Copy(rawValues, values, rawValues.Length);

            var dataItems = new DataItem[values.GetLength(0)];

            var row = new double[values.GetLength(1)];
            var size = Marshal.SizeOf<double>();

            for (var i = 0; i < dataItems.Length; i++)
            {
                
                Buffer.BlockCopy(values, i * row.Length * size, row, 0, row.Length * size);
                dataItems[i] = new DataItem
                {
                    Inputs = row.Skip(startInputCol - 1).Take(endInputCol - startInputCol + 1).ToArray(),
                    Output = row[outputCol - startInputCol]
                };
            }             

            workbook.Close();
            app.Quit();

            return dataItems;
        }
    }
}
