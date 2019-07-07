using MathNet.Numerics.LinearAlgebra;
using OSM2019.OSM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Utility
{
    class CustomDistribution
    {
        public Vector<double> MyDistribution { get; private set; }
        public CustomDistribution(Vector<double> dist)
        {
            this.MyDistribution = dist;
        }

        public int SampleCustomDistribution(ExtendRandom rand)
        {
            double accumlation_value = 0;
            double rand_value = rand.NextDouble();
            int index = 0;
            foreach (var value in this.MyDistribution)
            {
                accumlation_value += value;
                if (rand_value <= accumlation_value)
                {
                    return index;
                }
                index += 1;
            }
            Debug.Assert(false);
            return -1;
        }
    }
}
