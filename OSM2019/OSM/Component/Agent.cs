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
        public Matrix<double> InitBelief { get; private set; }
        public Matrix<double> Belief { get; private set; }

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

        public Agent SetInitBelief(Matrix<double> init_belief)
        {
            this.InitBelief = init_belief.Clone();
            this.Belief = init_belief.Clone();
            return this;
        }

        public Agent SetBelief(Matrix<double> beliefs)
        {
            if (Belief.RowCount != beliefs.RowCount)
            {
                throw new Exception(nameof(Agent) + " Error irregular beleif dim");
            }

            this.Belief = beliefs.Clone();
            return this;
        }


        public Agent SetBeliefFromList(List<double> belief_list)
        {
            if (Belief.RowCount != belief_list.Count)
            {
                throw new Exception(nameof(Agent) + " Error irregular beleif list");
            }

            var new_belief = Matrix<double>.Build.DenseOfColumnVectors(Vector<double>.Build.Dense(belief_list.ToArray()));
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
    }
}
