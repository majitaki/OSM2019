using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class InitBeliefGenerator
  {
    InitBeliefMode Mode;
    int NarrowDimSize;
    int NormalDimSize;
    int WideDimSize;

    public InitBeliefGenerator SetInitBeliefMode(InitBeliefMode mode)
    {
      this.Mode = mode;
      this.NarrowDimSize = 30;
      this.NormalDimSize = 10;
      this.WideDimSize = 2;
      return this;
    }

    public Vector<double> Generate(Vector<double> init_op, ExtendRandom agent_gene_rand)
    {
      var op_dim_size = init_op.Count;
      var init_belief_list = new List<double>();
      var last_belief = 0.0;
      var remain_belief = 1.0;
      var bound_rate = 0.8;
      var mu = remain_belief / op_dim_size;
      double stddev = 0.0;
      Vector<double> init_belief_vector = init_op.Clone();

      switch (this.Mode)
      {
        case InitBeliefMode.NormalNarrow:
          stddev = mu / this.NarrowDimSize;
          init_belief_list = agent_gene_rand.NextNormals(mu, stddev, op_dim_size - 1, bound_rate);
          last_belief = remain_belief - init_belief_list.Sum();
          last_belief = Math.Round(last_belief, 4);
          init_belief_list.Add(last_belief);
          break;
        case InitBeliefMode.Normal:
          stddev = mu / this.NormalDimSize;
          init_belief_list = agent_gene_rand.NextNormals(mu, stddev, op_dim_size - 1, bound_rate);
          last_belief = remain_belief - init_belief_list.Sum();
          last_belief = Math.Round(last_belief, 4);
          init_belief_list.Add(last_belief);
          break;
        case InitBeliefMode.NormalWide:
          //stddev = mu / this.WideDimSize;
          stddev = mu / this.NarrowDimSize;
          init_belief_list = agent_gene_rand.NextNormals(mu, stddev, op_dim_size, bound_rate, 1, 0.5);
          var sum = init_belief_list.Sum();
          init_belief_list = init_belief_list.Select(b => Math.Round(b / sum, 4)).ToList();
          //last_belief = remain_belief - init_belief_list.Sum();
          //last_belief = Math.Round(last_belief, 4);
          //init_belief_list.Add(last_belief);
          break;
        case InitBeliefMode.NoRandom:
          var one_init_belief = remain_belief / op_dim_size;
          one_init_belief = Math.Round(one_init_belief, 4);
          init_belief_list = Enumerable.Repeat(one_init_belief, op_dim_size - 1).ToList();
          last_belief = remain_belief - init_belief_list.Sum();
          last_belief = Math.Round(last_belief, 4);
          init_belief_list.Add(last_belief);
          break;
        default:
          break;
      }

      for (int index = 0; index < init_belief_list.Count; index++)
      {
        init_belief_vector[index] = init_belief_list[index];
      }

      return init_belief_vector;
    }
  }
}
