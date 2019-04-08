using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Anfis.Models
{
    public class TriangularInfo
    {
        public double A { get; set; }
        public double M { get; set; }

        public double Value(double input)
        {
            var B = 2 * M - A;

            if (input <= A)
                return 0;
            if (input <= M)
                return (input - A) / (M - A);
            if (input <= B)
                return (B - input) / (B - M);
            return 0;
        }
    }
}
