using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;

namespace OSM2019.OSM
{
    class Turara_DistGenerator : I_DistGenerator
    {
        public CustomDistribution MyCustomDistribution { get; set; }
        public Turara_DistGenerator(int dim, double dist_weight, int main_index)
        {
            var turara = MyMath.MakeTurara(dim, dist_weight);
            var dist = Vector<double>.Build.Dense(dim, turara.other);
            dist[main_index] = turara.max;
            this.MyCustomDistribution = new CustomDistribution(dist, dist_weight);
        }
        public CustomDistribution Generate()
        {
            return this.MyCustomDistribution;
        }
    }
}
