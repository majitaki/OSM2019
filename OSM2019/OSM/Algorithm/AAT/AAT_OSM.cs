using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class AAT_OSM : OSMBase<AAT_OSM>
    {
        double TargetH;
        Dictionary<Agent, Candidate> Candidates;

        public override AAT_OSM SetAgentNetwork(AgentNetwork agent_network)
        {
            this.MyAgentNetwork = agent_network;
            this.Candidates = new Dictionary<Agent, Candidate>();

            foreach (var agent in this.MyAgentNetwork.Agents)
            {
                this.Candidates.Add(agent, new Candidate(agent));
            }
            return this;
        }

        public AAT_OSM SetTargetH(double target_h)
        {
            this.TargetH = target_h;
            return this;
        }


        public override void UpdateRounds(int rounds, int steps)
        {
            int end_round = this.CurrentRound + rounds;

            for (; this.CurrentRound < end_round; this.CurrentRound++)
            {
                this.UpdateSteps(steps);
                this.RecordRound();

                this.EstimateAwaRate();
                this.SelectionWeight();


                this.InitializeToZeroStep();
            }
        }

        protected void EstimateAwaRate()
        {

        }

        protected void SelectionWeight()
        {

        }


    }
}
