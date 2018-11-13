using MathNet.Numerics.LinearAlgebra;
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
        InitWeightMode MyInitWeightMode;

        public SampleAgent SetInitBeliefGene(InitBeliefGenerator init_belief_gene)
        {
            this.MyInitBeliefGene = init_belief_gene;
            return this;
        }

        public SampleAgent SetThreshold(double threshold)
        {
            this.OpinionThreshold = threshold;
            return this;
        }

        public SampleAgent SetInitWeightsMode(InitWeightMode mode)
        {
            this.MyInitWeightMode = mode;
            return this;
        }

        public void Generate(ExtendRandom agent_network_rand, Agent agent)
        {
            var init_belief = this.MyInitBeliefGene.Generate(this.InitOpinionMatrix, agent_network_rand);
            agent.SetInitBelief(init_belief);
            agent.SetSubject(this.Subject);
            
        }
    }
}
