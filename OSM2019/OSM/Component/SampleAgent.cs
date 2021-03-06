﻿using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class SampleAgent : AgentBase<SampleAgent>
  {
    InitBeliefGenerator MyInitBeliefGene;

    public SampleAgent SetInitBeliefGene(InitBeliefGenerator init_belief_gene)
    {
      this.MyInitBeliefGene = init_belief_gene;
      return this;
    }


    public void Generate(ExtendRandom agent_network_rand, Agent agent)
    {
      var init_belief = this.MyInitBeliefGene.Generate(this.InitOpinion, agent_network_rand);
      agent.SetInitBelief(init_belief);
      agent.SetSubject(this.MySubject);
      agent.SetInitOpinion(this.InitOpinion);
      agent.SetThreshold(this.OpinionThreshold);
    }
  }
}
