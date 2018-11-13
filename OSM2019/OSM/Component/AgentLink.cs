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
            this.InitSourceWeight = init_target_weight;
            this.TargetWeight = this.InitTargetWeight;
            return this;
        }

    }
}
