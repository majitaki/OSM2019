using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class Agent : AgentBase<Agent>
  {
    public Vector<double> InitBelief { get; private set; }
    public Vector<double> Belief { get; private set; }

    public Agent()
    {
    }

    public Agent(Node node)
    {
      this.AgentID = node.ID;
      this.AgentLinks = new List<AgentLink>();
      this.IsSensor = false;
    }

    public Agent(int node_id)
    {
      this.AgentID = node_id;
      this.AgentLinks = new List<AgentLink>();
      this.IsSensor = false;
    }

    public Agent AttachAgentLinks(List<AgentLink> agent_links)
    {
      this.AgentLinks.AddRange(agent_links.Where(agent_link => agent_link.SourceAgent.AgentID == this.AgentID || agent_link.TargetAgent.AgentID == this.AgentID).ToList());
      return this;
    }

    public Agent SetInitBelief(Vector<double> init_belief)
    {
      this.InitBelief = init_belief.Clone();
      this.Belief = init_belief.Clone();
      return this;
    }

    public Agent SetBelief(Vector<double> belief)
    {
      if (Belief.Count != belief.Count)
      {
        throw new Exception(nameof(Agent) + " Error irregular beleif dim");
      }

      this.Belief = belief.Clone();
      return this;
    }


    public Agent SetBeliefFromList(List<double> belief_list)
    {
      if (Belief.Count != belief_list.Count)
      {
        throw new Exception(nameof(Agent) + " Error irregular beleif list");
      }

      var new_belief = Vector<double>.Build.Dense(belief_list.ToArray());
      this.Belief = new_belief;
      //Console.WriteLine(this.Belief.ToString());
      return this;
    }

    public void SetCommonWeight(double common_weight)
    {
      foreach (var link in this.AgentLinks)
      {
        link.SetWeight(this, common_weight);
      }
    }

    public void SetWeights(Dictionary<Agent, double> weights)
    {
      foreach (var link in this.AgentLinks.Where(l => l.SourceAgent.AgentID >= 0 && l.TargetAgent.AgentID >= 0))
      {
        var weight = weights.First(wei => wei.Key == link.SourceAgent || wei.Key == link.TargetAgent).Value;
        link.SetWeight(this, weight);
      }
    }
  }
}
