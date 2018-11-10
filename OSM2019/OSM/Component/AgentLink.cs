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

    }
}
