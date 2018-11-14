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
        public Matrix<double> Belief { get; set; }

        public Agent(Node node)
        {
            this.AgentID = node.ID;
            this.AgentLinks = new List<AgentLink>();
            this.IsSensor = false;
        }

        public Agent(int node_id)
        {
            this.AgentID = node_id;
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
    }
}
