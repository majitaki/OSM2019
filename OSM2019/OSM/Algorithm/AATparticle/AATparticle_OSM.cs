using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class AATparticle_OSM : AAT_OSM
  {
    public int SampleSize { get; private set; }

    public AATparticle_OSM()
    {
      this.SampleSize = 10;
    }
    public void SetSampleSize(int sample_size)
    {
      this.SampleSize = sample_size;
    }
    public override void SetBeliefUpdater(BeliefUpdater belief_updater)
    {
      this.MyBeliefUpdater = belief_updater.SetBeliefUpdateFunctionMode(BeliefUpdateFunctionEnum.Particle);
    }
    //protected override List<Message> AgentSendMessages(List<Agent> op_formed_agents)
    //{
    //    List<Message> messages = new List<Message>();
    //    foreach (var agent in op_formed_agents)
    //    {
    //        if (agent == null) continue;
    //        //var opinion = agent.Opinion.Clone();
    //        var opinion = Vector<double>.Build.DenseOfVector(agent.Opinion);
    //        opinion.Clear();
    //        var belief_dist = new CustomDistribution(agent.Belief);

    //        foreach (int i in Enumerable.Range(0, this.SampleSize))
    //        {
    //            int sample_index = belief_dist.SampleCustomDistribution(this.UpdateStepRand);
    //            opinion[sample_index] += 1.0;
    //        }

    //        foreach (var to_agent in agent.GetNeighbors())
    //        {
    //            var agent_link = agent.AgentLinks.Where(link => link.SourceAgent == to_agent || link.TargetAgent == to_agent).First();
    //            messages.Add(new Message(agent, to_agent, agent_link, opinion));
    //        }
    //    }
    //    return messages;
    //}

    public override void NextStep()
    {
      //sensor observe
      if (this.CurrentStep % this.OpinionIntroInterval == 0)
      {
        var all_sensors = this.MyAgentNetwork.Agents.Where(agent => agent.IsSensor).ToList();
        var observe_num = (int)Math.Ceiling(all_sensors.Count * this.OpinionIntroRate);
        var observe_sensors = all_sensors.Select(agent => agent.AgentID).OrderBy(a => this.UpdateStepRand.Next()).Take(observe_num).Select(id => this.MyAgentNetwork.Agents[id]).ToList();
        var env_messages = this.MyEnvManager.SendMessages(observe_sensors, this.UpdateStepRand, this.SampleSize);
        Messages.AddRange(env_messages);
      }

      //agent observe
      var op_form_messages = this.AgentSendMessages(OpinionFormedAgents);
      Messages.AddRange(op_form_messages);
      OpinionFormedAgents.Clear();

      //agent receive
      foreach (var message in this.Messages)
      {
        this.UpdateBeliefByMessage(message);
        var op_form_agent = this.UpdateOpinion(message);
        OpinionFormedAgents.Add(op_form_agent);
      }

      this.CurrentStep++;
    }

    //protected override void SetCandidate()
    //{
    //    this.Candidates = new Dictionary<Agent, Candidate>();
    //    foreach (var agent in this.MyAgentNetwork.Agents)
    //    {
    //        var can = new AATparticle_Candidate(agent);
    //        this.Candidates.Add(agent, can);
    //        agent.SetCommonWeight(can.GetSelectCanWeight());
    //    }
    //}
  }
}
