﻿using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class AATfunction_iwt_OSM : AATfunction_OSM
  {
    public List<SelfInformation> SelfInformations { get; private set; }
    public double CommonCuriocity { get; private set; }
    public Dictionary<Agent, double> AgentCuriocities { get; private set; }
    public AATfunction_iwt_OSM() : base()
    {
    }

    public void SetCommonCuriocity(double common_curiocity)
    {
      this.AgentCuriocities = new Dictionary<Agent, double>();
      this.CommonCuriocity = common_curiocity;
    }

    public override void SetAgentNetwork(AgentNetwork agent_network)
    {
      base.SetAgentNetwork(agent_network);
      foreach (var agent in this.MyAgentNetwork.Agents)
      {
        this.AgentCuriocities.Add(agent, this.CommonCuriocity);
      }
      this.SetSelfInformations(agent_network);
      return;
    }

    //round
    public override void InitializeToFirstRound()
    {
      base.InitializeToFirstRound();
      this.SetSelfInformations(this.MyAgentNetwork);
    }

    protected void SetSelfInformations(AgentNetwork agent_network)
    {
      this.SelfInformations = new List<SelfInformation>();

      foreach (var agent in agent_network.Agents)
      {
        foreach (var neighbor_agent in agent.GetNeighbors())
        {
          this.SelfInformations.Add(new SelfInformation(agent, neighbor_agent));
        }
      }
    }

    public override void NextStep()
    {
      //sensor observe
      if (this.CurrentStep % this.OpinionIntroInterval == 0)
      {
        var all_sensors = this.MyAgentNetwork.Agents.Where(agent => agent.IsSensor).ToList();
        var observe_num = (int)Math.Ceiling(all_sensors.Count * this.OpinionIntroRate);
        var observe_sensors = all_sensors.Select(agent => agent.AgentID).OrderBy(a => this.UpdateStepRand.Next()).Take(observe_num).Select(id => this.MyAgentNetwork.Agents[id]).ToList();
        var env_messages = this.MyEnvManager.SendMessages(observe_sensors, this.UpdateStepRand);
        Messages.AddRange(env_messages);
      }

      //agent observe
      var op_form_messages = this.AgentSendMessages(OpinionFormedAgents);
      Messages.AddRange(op_form_messages);
      OpinionFormedAgents.Clear();

      //agent receive
      foreach (var message in this.Messages)
      {
        //update selfinfo
        if (message.FromAgent.AgentID >= 0) this.SelfInformations.First(selfinfo => message.ToAgent == selfinfo.SourceAgent && message.FromAgent == selfinfo.NeighborAgent).UpdateValue(message.Opinion);
        this.UpdateBeliefByMessage(message);
        var op_form_agent = this.UpdateOpinion(message);
        OpinionFormedAgents.Add(op_form_agent);
      }

      this.CurrentStep++;
    }

    protected override void SelectionWeight()
    {
      foreach (var candidate in this.Candidates)
      {
        var received_sum_op = this.MyRecordRounds.Last().AgentReceiveOpinionsInRound[candidate.Key];
        double obs_u = this.GetObsU(received_sum_op);
        if (obs_u == 0) continue;

        var current_h = candidate.Value.GetWindowAwaRate();

        if (current_h < this.TargetH + this.Epsilon || current_h > this.TargetH - this.Epsilon)
        {
          var new_weight = candidate.Value.EstimateWeight(this.TargetH);
          candidate.Value.CanWeight = new_weight;
          candidate.Key.SetCommonWeight(new_weight);
          if (candidate.Key.IsSensor)
          {
            candidate.Key.SetCommonWeight(new_weight);
          }
          else
          {
            var weights = this.CalcIndividualCuriocities(candidate.Key, new_weight);
            candidate.Key.SetWeights(weights);
          }
          Debug.Assert(new_weight >= 0 && new_weight <= 1);
        }
      }
    }

    public override void FinalizeRound()
    {
      base.FinalizeRound();
      foreach (var self_info in this.SelfInformations)
      {
        self_info.ClearValue();
      }
    }

    Dictionary<Agent, double> CalcIndividualCuriocities(Agent agent, double sel_can_weight)
    {
      var agent_self_informations = this.SelfInformations.Where(self_info => self_info.SourceAgent == agent).ToList();
      var total_info_value = agent_self_informations.Select(self_info => self_info.Value).Sum();
      var max_info_value = agent_self_informations.Select(self_info => self_info.Value).Max();
      var ave_info_value = agent_self_informations.Select(self_info => self_info.Value).Average();


      if (agent_self_informations.Where(w => Double.IsNaN(w.Value)).Count() > 0)
      {
        Console.WriteLine();
      }

      Dictionary<Agent, double> weights = new Dictionary<Agent, double>();
      foreach (var self_info in agent_self_informations)
      {
        var indivi_curiocity = 0.0;
        var indivi_weight = 0.0;
        indivi_curiocity = (self_info.Value / max_info_value) * (1 - 1.0 / agent.MySubject.SubjectDimSize) + (1.0 / agent.MySubject.SubjectDimSize);
        indivi_weight = sel_can_weight * (1 - this.AgentCuriocities[agent]) + indivi_curiocity * this.AgentCuriocities[agent];
        indivi_weight = Math.Round(indivi_weight, 4);

        weights.Add(self_info.NeighborAgent, indivi_weight);

        Debug.Assert(!Double.IsNaN(indivi_weight));
      }
      return weights;
    }

  }
}
