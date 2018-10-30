using System;
using System.Collections.Generic;
using Anfis.Models;

namespace Anfis.Services
{
    public class AnfisService
    {

        public void Train(DataItem[] items)
        {
            Console.WriteLine(items);
        }

        public double Test(DataItem[] items)
        {
            return 0;
        }

        public double Predict(double[] inputs)
        {
            return 0;
        }
    }
}
