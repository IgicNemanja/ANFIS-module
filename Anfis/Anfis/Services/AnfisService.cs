using System;
using System.Collections.Generic;
using System.Reflection;
using Anfis.Models;

namespace Anfis.Services
{
    public class AnfisService
    {
        public List<DataItem> TrainSet { get; set; }
        public List<DataItem> TestSet { get; set; }
        public TriangularInfo[] TriangularInfoFolds { get; set; }

        public AnfisService()
        {
            TrainSet = new List<DataItem>();
            TestSet = new List<DataItem>();
        }

        public AnfisService(List<DataItem> dataItems) : this (dataItems, 0.7, 0, 1, 3) { }

        public AnfisService(List<DataItem> dataItems, double trainChunk, double minValue, double maxValue, int foldNum) : this()
        {
            if (dataItems.Count == 0)
                throw new Exception("Data items must not be empty");
            if (trainChunk <= 0 || trainChunk >= 1)
                throw  new Exception("Train chunk value must be between 0 and 1");
            if (foldNum <= 1)
                throw new Exception("Number of folds must be greater than 1");

            TrainSet.AddRange(dataItems.GetRange(0, (int)(trainChunk * dataItems.Count)));
            TestSet.AddRange(dataItems.GetRange(
                (int)(trainChunk * dataItems.Count), dataItems.Count - (int)(trainChunk * dataItems.Count)));

            TriangularInfoFolds = new TriangularInfo[foldNum];
            var step = (maxValue - minValue) / (foldNum - 1);
            for (int i = 0; i < foldNum; i++)
            {
                TriangularInfoFolds[i] = new TriangularInfo()
                {
                    A = minValue - step + i * step,
                    M = minValue + i * step
                };
            }
        }

        public void Train()
        {
            foreach (var item in TrainSet)
            {
                //Layer 1

            }
        }

        public double Test()
        {
            return 0;
        }

        public double Predict(double[] row)
        {
            return 0;
        }

        private double[] TriangleValues(List<double> values)
        {
            double[] result = new double[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                result[i] = TriangularInfoFolds[i].Value(values[i]);
            }
            return result;
        }
    }
}
