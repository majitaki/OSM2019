using OSM2019.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class BasicAgentManager : I_AgentManager
    {
        public List<I_Agent> AgentList { get; }
        public List<I_AgentLink> AgentLinkList { get; }

        public BasicAgentManager(List<I_Agent> agent_list, List<I_AgentLink> agentlink_list)
        {
            this.AgentList = agent_list;
            this.AgentLinkList = agentlink_list;
        }

        public void Initialize()
        {
            this.AgentList.ForEach(agent => agent.Initialize(this.AgentLinkList));
            this.AgentLinkList.ForEach(agentlink => agentlink.Initialize(this.AgentList));
        }
    }
}
