using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM {
  class AggregationFunctions {

    private (double max, double other) MakeTurara(int dim, double weight) {
      double max_weight = 1 / (1 + (dim - 1) * (1 - weight));
      double other_weight = max_weight * (1 - weight);
      return (max_weight, other_weight);
    }

    private Vector<double> GetLikelihoodsForBayseFilter(double weight, int dim, int sample_index) {
      var weights = this.MakeTurara(dim, weight);
      var max_weight = weights.max;
      var other_weight = weights.other;

      var likelihoods = Vector<double>.Build.Dense(dim, other_weight);
      likelihoods[sample_index] = max_weight;
      return likelihoods;
    }


    private Vector<double> GetPostBeliefsByBayseFilter(Vector<double> prior_beliefs, Vector<double> receive_opinions, double weight) {
      int dim = prior_beliefs.Count;

      var likelihoods = Vector<double>.Build.Dense(dim, 0);
      var post_beliefs = prior_beliefs;

      foreach (var sample_index in Enumerable.Range(0, receive_opinions.Count)) {
        foreach (var i in Enumerable.Range(0, (int)receive_opinions[sample_index])) {
          likelihoods = this.GetLikelihoodsForBayseFilter(weight, dim, sample_index);
          post_beliefs = post_beliefs.PointwiseMultiply(likelihoods);
        }
      }
      //Debug.Assert(receive_opinion[sample_index] == 1);

      if (post_beliefs.Sum() == 0) {
        post_beliefs.Clear();
        return post_beliefs + (1.0 / dim);
      }
      return post_beliefs.Normalize(1.0);
    }

    public Vector<double> UpdateBelief(Vector<double> belief, double weight, Vector<double> receive_opinions) {
      if (Double.IsNaN(weight)) {
        Console.WriteLine();
      }

      /*
      var pre_belief_list = belief.Clone();
      var op_list = receive_opinion.Clone();

      var max_op_dim = op_list.Count;
      for (int op_dim = 0; op_dim < max_op_dim; op_dim++) {
        var op = op_list[op_dim];
        int op_num = (int)Math.Floor(op);
        double op_dust = op % 1;

        for (int i = 0; i < op_num; i++) {
          var post_belief_list = pre_belief_list.Clone();
          var max_belief_dim = pre_belief_list.Count;
          for (int belief_dim = 0; belief_dim < max_belief_dim; belief_dim++) {
            double post_belief;
            post_belief = this.CalcSingleBelief(pre_belief_list, belief_dim, op_dim, weight);
            post_belief_list[belief_dim] = post_belief;
          }
          pre_belief_list = post_belief_list;
        }

        if (op_dust > 0) {
          var post_belief_list = pre_belief_list.Clone();
          var max_belief_dim = pre_belief_list.Count;
          for (int belief_dim = 0; belief_dim < max_belief_dim; belief_dim++) {
            double post_belief;
            post_belief = this.CalcSingleBelief(pre_belief_list, belief_dim, op_dim, weight, op_dust);
            post_belief_list[belief_dim] = post_belief;
          }
          pre_belief_list = post_belief_list;
        }
      }

      var new_belief = Vector<double>.Build.Dense(pre_belief_list.ToArray());

      return new_belief;
      
*/
      return this.GetPostBeliefsByBayseFilter(belief, receive_opinions, weight);
    }

    public double CalcSingleBelief(Vector<double> pre_beliefs, int belief_dim, int op_dim, double weight, double op_dust = 0.0) {
      var upper = pre_beliefs[belief_dim] * this.ConvertWeight(weight, belief_dim, op_dim, pre_beliefs.Count, op_dust);

      var lower = 0.001;
      foreach (var lower_belief_dim in Enumerable.Range(0, pre_beliefs.Count)) {
        var pre_belief = pre_beliefs[lower_belief_dim];
        lower += pre_belief * this.ConvertWeight(weight, lower_belief_dim, op_dim, pre_beliefs.Count, op_dust);
      }

      var pos_belief = upper / lower;

      return Math.Round(pos_belief, 4);
    }

    public double ConvertWeight(double weight, int belief_dim, int op_dim, int dim_size, double op_dust) {
      if (op_dust != 0.0) {
        weight = (weight - 1 / dim_size) * op_dust + 1 / dim_size;
      }

      if (belief_dim == op_dim) {
        return weight;
      } else {
        return (1 - weight) / (dim_size - 1);
      }
    }
  }
}
