using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anfis.Services;
using Test.Helpers;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataItems = ExcelHelper.ExtractDataItems(1,8,9);
            var anfisService = new AnfisService();
            anfisService.Train(dataItems);
        }
    }
}
