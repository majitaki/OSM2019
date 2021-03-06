﻿using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class OpinionEnvironment
  {
    public double SensorWeight { get; protected set; }
    public OpinionSubject EnvSubject { get; protected set; }
    public int CorrectDim { get; protected set; }
    public int MaliciousDim { get; protected set; }

    public Agent EnvironmentAgent { get; protected set; }
    public CustomDistribution MyCustomDistribution { get; protected set; }
    public CustomDistribution MyMaliciousCustomDistribution { get; protected set; }

    public OpinionEnvironment()
    {
    }

    public OpinionEnvironment SetSubject(OpinionSubject subject)
    {
      this.EnvSubject = subject;
      return this;
    }

    public OpinionEnvironment SetCorrectDim(int cor_dim)
    {
      this.CorrectDim = cor_dim;
      return this;
    }

    public OpinionEnvironment SetSensorWeight(double sensor_weight)
    {
      this.SensorWeight = sensor_weight;
      return this;
    }

    public OpinionEnvironment SetCustomDistribution(CustomDistribution custom_dist)
    {
      this.MyCustomDistribution = custom_dist;
      return this;
    }
    public OpinionEnvironment SetMaliciousCustomDistribution(CustomDistribution custom_malicious_dist)
    {
      this.MyMaliciousCustomDistribution = custom_malicious_dist;
      return this;
    }


    public OpinionEnvironment SetMaliciousDim(int malicious_dim)
    {
      this.MaliciousDim = malicious_dim;
      return this;
    }

    public void AddEnvironment(AgentNetwork agent_network)
    {
      this.EnvironmentAgent = new Agent(-1).SetSubject(this.EnvSubject);
      var op_vector = Vector<double>.Build.Dense(this.EnvSubject.SubjectDimSize, 0.0);
      this.EnvironmentAgent.SetInitOpinion(op_vector);
      List<AgentLink> env_links = new List<AgentLink>();
      var sensors = agent_network.Agents.Where(agent => agent.IsSensor).ToList();

      int link_index = -1;
      foreach (var sensor in sensors)
      {
        env_links.Add(new AgentLink(link_index, sensor, this.EnvironmentAgent));
        link_index--;
      }

      foreach (var sensor in sensors)
      {
        sensor.AttachAgentLinks(env_links);
      }

      this.EnvironmentAgent.AttachAgentLinks(env_links);
    }

    public List<Message> SendMessages(List<Agent> sensor_agents, ExtendRandom update_step_rand, int sample_size = 1)
    {
      List<Message> messages = new List<Message>();
      foreach (var sensor_agent in sensor_agents)
      {
        var agent_link = this.EnvironmentAgent.AgentLinks.Where(link => link.SourceAgent == sensor_agent || link.TargetAgent == sensor_agent).First();
        var opinion = this.EnvironmentAgent.Opinion.Clone();
        opinion.Clear();

        foreach (int i in Enumerable.Range(0, sample_size))
        {
          int sample_index = -1;
          if (sensor_agent.IsMalicious)
          {
            sample_index = this.MyMaliciousCustomDistribution.SampleCustomDistribution(update_step_rand);
          }
          else
          {
            sample_index = this.MyCustomDistribution.SampleCustomDistribution(update_step_rand);
          }
          opinion[sample_index] += 1.0;
        }
        messages.Add(new Message(this.EnvironmentAgent, sensor_agent, agent_link, opinion));
      }

      return messages;
    }

  }
}
