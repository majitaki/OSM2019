using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class SWTkai_OSM : OSMBase
  {

    public SWTkai_OSM() : base()
    {
    }


    public override void InitializeToFirstRound()
    {
      base.InitializeToFirstRound();
    }

    public override void FinalizeRound()
    {
      this.SelectionWeight();
      base.FinalizeRound();
    }
    public override void PrintAgentInfo(Agent agent)
    {
      base.PrintAgentInfo(agent);
      foreach (var link_info_value in this.LinkInfoValues.Where(info_value => info_value.ReceiveAgent == agent))
      {
        Console.WriteLine($"sender: {link_info_value.SendAgent.AgentID} sum_info_value: {link_info_value.GetInfoValueSum():f3}");
      }
    }


    protected virtual void SelectionWeight()
    {
      foreach (var agent in this.MyAgentNetwork.Agents)
      {
        if (agent.IsSensor) continue;
        if (agent.AgentID == 199)
        {
          var a = 0;
        }

        var link_info_values = this.LinkInfoValues.Where(info_value => info_value.ReceiveAgent.AgentID == agent.AgentID).ToList();
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
          var neighbor_agent = link_info_value.SendAgent;
          var link = agent.AgentLinks.First(l => l.IsConnect(agent, neighbor_agent));
          var current_weight = link.GetWeight(agent);
          var info_weight = norm_link_info_values[neighbor_agent];

          var error = info_weight - current_weight;
          if (norm_link_info_values.Values.Sum() == 0) error = 1.0 - current_weight;
          if (norm_link_info_values.Values.All(v => v == 1.0)) error = 0.0 - current_weight;

          var indi_weight = current_weight + 0.3 * error;
          if (indi_weight > 1.0) indi_weight = 1.0;
          if (indi_weight < 0.0) indi_weight = 0.0;
          indi_weight = Math.Round(indi_weight, 5);

          weights.Add(link_info_value.SendAgent, indi_weight);
          Debug.Assert(!Double.IsNaN(indi_weight));
        }

        agent.SetWeights(weights);

      }
    }


  }
}
