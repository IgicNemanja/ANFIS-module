using System;
using System.Collections.Generic;
using Anfis.Models;

namespace Anfis.Services
{
    public class AnfisService
    {

        public void Train(List<DataItem> items)
        {
            Console.WriteLine(items);
        }

        public double Test(List<DataItem> items)
        {
            return 0;
        }

        public double Predict(List<double> inputs)
        {
            return 0;
        }
    }
}
