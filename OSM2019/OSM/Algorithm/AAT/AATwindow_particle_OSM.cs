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
  class AATwindow_particle_OSM : AATwindow_OSM
  {
    public int SampleSize { get; private set; }
    public AATwindow_particle_OSM() : base()
    {
      this.AwaRateWindowSize = 10;
    }
    public void SetSampleSize(int sample_size)
    {
      this.SampleSize = sample_size;
    }
    public override void SetBeliefUpdater(BeliefUpdater belief_updater)
    {
      this.MyBeliefUpdater = belief_updater.SetBeliefUpdateFunctionMode(BeliefUpdateFunctionEnum.Particle);
    }

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
  }
}
