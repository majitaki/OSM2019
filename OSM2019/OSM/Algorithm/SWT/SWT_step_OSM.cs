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
  class SWT_step_OSM : SWT_OSM
  {
    public double InfoLearningRate { get; private set; }

    public void SetInfoLearningRate(double info_learning_rate)
    {
      this.InfoLearningRate = info_learning_rate;
    }
    protected override Dictionary<Agent, double> CalcInfoWeight(Agent agent, double aat_weight)
    {
      var link_info_values = this.LinkInfoValues.Where(info_value => info_value.ReceiveAgent == agent).ToList();
      var log_link_info_values = new Dictionary<Agent, double>();
      var norm_link_info_values = new Dictionary<Agent, double>();
      Dictionary<Agent, double> weights = new Dictionary<Agent, double>();

      foreach (var link_info_value in link_info_values)
      {
        log_link_info_values.Add(link_info_value.SendAgent, Math.Log((link_info_value.GetInfoValueSum() + 2), 2));
      }

      var max = log_link_info_values.Values.Max();
      foreach (var log_link_info_value in log_link_info_values)
      {
        norm_link_info_values.Add(log_link_info_value.Key, log_link_info_value.Value / max);
      }


      var ave = norm_link_info_values.Values.Average();

      foreach (var link_info_value in link_info_values)
      {
        //var info_weight = norm_link_info_values[link_info_value.SendAgent];
        if (link_info_value.SendAgent.AgentID < 0) continue;

        var pre_weight = agent.AgentLinks.Where(l => l.TargetAgent == link_info_value.SendAgent || l.SourceAgent == link_info_value.SendAgent)
            .First().GetWeight(link_info_value.ReceiveAgent);
        var info_weight = pre_weight + this.InfoLearningRate * (norm_link_info_values[link_info_value.SendAgent] - ave);
        info_weight = (info_weight > 1) ? 1 : info_weight;
        info_weight = (info_weight < 0) ? 0 : info_weight;

        var indi_weight = (1 - this.InfoWeightRate) * aat_weight + this.InfoWeightRate * info_weight;
        if (norm_link_info_values.Values.Sum() == 0) indi_weight = aat_weight;
        weights.Add(link_info_value.SendAgent, indi_weight);
        Debug.Assert(!Double.IsNaN(indi_weight));
      }
      return weights;
    }
  }
}
