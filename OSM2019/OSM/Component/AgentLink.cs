using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class AgentLink
    {
        public int AgentLinkID { get; private set; }
        public Agent SourceAgent { get; private set; }
        public Agent TargetAgent { get; private set; }
        public double SourceWeight { get; set; }
        public double TargetWeight { get; set; }
        public double InitSourceWeight { get; private set; }
        public double InitTargetWeight { get; private set; }

        public AgentLink(int link_index)
        {
            this.AgentLinkID = link_index;
            this.SetInitSourceWeight(0.6);
            this.SetInitTargetWeight(0.6);
        }

        public AgentLink(int link_index, Agent source_agent, Agent target_agent)
        {
            this.AgentLinkID = link_index;
            this.SourceAgent = source_agent;
            this.TargetAgent = target_agent;
            this.SetInitSourceWeight(0.6);
            this.SetInitTargetWeight(0.6);
        }

        public AgentLink(AgentLink agent_link)
        {
            this.AgentLinkID = agent_link.AgentLinkID;
            this.SourceAgent = agent_link.SourceAgent;
            this.TargetAgent = agent_link.TargetAgent;
            this.SetInitSourceWeight(agent_link.InitSourceWeight);
            this.SetInitTargetWeight(agent_link.InitTargetWeight);
        }

        public AgentLink SetLink(Link link, List<Agent> agents)
        {
            this.SourceAgent = agents.First(agent => agent.AgentID == link.Source);
            this.TargetAgent = agents.First(agent => agent.AgentID == link.Target);
            return this;
        }

        public AgentLink SetInitSourceWeight(double init_source_weight)
        {
            this.InitSourceWeight = init_source_weight;
            this.SourceWeight = this.InitSourceWeight;
            return this;
        }

        public AgentLink SetInitTargetWeight(double init_target_weight)
        {
            this.InitTargetWeight = init_target_weight;
            this.TargetWeight = init_target_weight;
            return this;
        }

        public void SetWeight(Agent agent, double weight)
        {
            if (this.SourceAgent == agent)
            {
                this.SourceWeight = weight;
            }
            else if (this.TargetAgent == agent)
            {
                this.TargetWeight = weight;
            }
            else
            {
                throw new Exception("error illegal agent");
            }
        }

        public double GetWeight(Agent agent)
        {
            if (this.SourceAgent == agent)
            {
                return this.SourceWeight;
            }
            else if (this.TargetAgent == agent)
            {
                return this.TargetWeight;
            }
            else
            {
                throw new Exception("error illegal agent");
            }
        }

        public bool IsConnect(Agent agent_a, Agent agent_b)
        {
            if (this.SourceAgent == agent_a && this.TargetAgent == agent_b)
            {
                return true;
            }
            else if (this.SourceAgent == agent_b && this.TargetAgent == agent_a)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
