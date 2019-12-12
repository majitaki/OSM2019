using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class SWT_OSM : AATfunction_OSM
  {
    public double InfoWeightRate { get; private set; }

    public void SetInfoWeightRate(double info_w_rate)
    {
      this.InfoWeightRate = info_w_rate;
    }

    public override void PrintAgentInfo(Agent agent)
    {
      base.PrintAgentInfo(agent);
      foreach (var link_info_value in this.LinkInfoValues.Where(info_value => info_value.ReceiveAgent == agent))
      {
        Console.WriteLine($"sender: {link_info_value.SendAgent.AgentID} sum_info_value: {link_info_value.GetInfoValueSum():f3}");
      }
    }

    protected override void SelectionWeight()
    {
      foreach (var candidate in this.Candidates)
      {
        var received_sum_op = this.MyRecordRounds.Last().AgentReceiveOpinionsInRound[candidate.Key];
        double obs_u = this.GetObsU(received_sum_op);
        if (obs_u == 0) continue;

        var current_h = candidate.Value.GetWindowAwaRate();

        var aat_weight = candidate.Value.EstimateWeight(this.TargetH);
        candidate.Value.CanWeight = aat_weight;
        if (candidate.Key.IsSensor)
        {
          candidate.Key.SetCommonWeight(aat_weight);
        }
        else
        {
          //candidate.Key.SetCommonWeight(aat_weight);
          var weights = this.CalcInfoWeight(candidate.Key, aat_weight);
          candidate.Key.SetWeights(weights);
        }
        Debug.Assert(aat_weight >= 0 && aat_weight <= 1);


        //if (current_h < this.TargetH + this.Epsilon || current_h > this.TargetH - this.Epsilon)
        //{
        //  var aat_weight = candidate.Value.EstimateWeight(this.TargetH);
        //  candidate.Value.CanWeight = aat_weight;
        //  if (candidate.Key.IsSensor)
        //  {
        //    candidate.Key.SetCommonWeight(aat_weight);
        //  }
        //  else
        //  {
        //    //candidate.Key.SetCommonWeight(aat_weight);
        //    var weights = this.CalcInfoWeight(candidate.Key, aat_weight);
        //    candidate.Key.SetWeights(weights);
        //  }
        //  Debug.Assert(aat_weight >= 0 && aat_weight <= 1);
        //}
      }
    }

    protected virtual Dictionary<Agent, double> CalcInfoWeight(Agent agent, double aat_weight)
    {
      var link_info_values = this.LinkInfoValues.Where(info_value => info_value.ReceiveAgent == agent).ToList();
      var log_link_info_values = new Dictionary<Agent, double>();
      var norm_link_info_values = new Dictionary<Agent, double>();
      Dictionary<Agent, double> weights = new Dictionary<Agent, double>();

      foreach (var link_info_value in link_info_values)
      {
        log_link_info_values.Add(link_info_value.SendAgent, Math.Log((link_info_value.GetInfoValueSum() + 2), 2));
      }

      var max = log_link_info_values.Values.ToList().Max();
      foreach (var log_link_info_value in log_link_info_values)
      {
        norm_link_info_values.Add(log_link_info_value.Key, log_link_info_value.Value / max);
      }

      foreach (var link_info_value in link_info_values)
      {
        var info_weight = norm_link_info_values[link_info_value.SendAgent];
        var indi_weight = (1 - this.InfoWeightRate) * aat_weight + this.InfoWeightRate * info_weight;
        if (norm_link_info_values.Values.Sum() == 0) indi_weight = aat_weight;
        weights.Add(link_info_value.SendAgent, indi_weight);
        Debug.Assert(!Double.IsNaN(indi_weight));
      }
      return weights;
    }
  }
}
