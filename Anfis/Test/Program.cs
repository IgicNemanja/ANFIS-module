using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anfis.Services;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataItems = ExcelHelper.ExtractData(100);
            var anfisService = new AnfisService();
            anfisService.Train(dataItems);
        }
    }
}
