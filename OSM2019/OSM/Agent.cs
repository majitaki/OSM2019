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
        public Matrix<double> MyInitBelief { get; private set; }
        public Matrix<double> MyBelief { get; set; }

        public Agent(Node node)
        {
            this.AgentID = node.ID;
            this.IsSensor = false;
        }

        public Agent AttachAgentLinks(List<AgentLink> agent_links)
        {
            this.AgentLinks = agent_links.Where(agent_link => agent_link.SourceAgent.AgentID == this.AgentID).ToList();
            return this;
        }

        public Agent SetInitBelief(Matrix<double> init_belief)
        {
            this.MyInitBelief = init_belief.Clone();
            this.MyBelief = init_belief.Clone();
            return this;
        }
    }
}
