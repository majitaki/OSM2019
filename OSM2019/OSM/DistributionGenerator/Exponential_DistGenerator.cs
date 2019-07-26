using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;

namespace OSM2019.OSM
{
  class Exponential_DistGenerator : I_DistGenerator
  {
    public CustomDistribution MyCustomDistribution { get; set; }
    public Exponential_DistGenerator(int dim, double dist_weight, int main_index)
    {
      var range = Enumerable.Range(0, dim);
      var dist_enumerable = range.Select((item, index) => Math.Pow(1 - dist_weight, index));
      var sum = dist_enumerable.Sum();
      var dist_list = dist_enumerable.Select(item => Math.Round(item / sum, 5));
      var dist = Vector<double>.Build.Dense(dist_list.ToArray());
      var max_index = dist.MaximumIndex();
      var max = dist[max_index];
      dist[max_index] = dist[main_index];
      dist[main_index] = max;

      this.MyCustomDistribution = new CustomDistribution(dist);
    }
    public CustomDistribution Generate()
    {
      return this.MyCustomDistribution;
    }
  }
}
