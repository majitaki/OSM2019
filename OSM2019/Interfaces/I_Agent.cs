using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Interfaces
{
    interface I_Agent
    {
        int AgentID { get; }
        Dictionary<int, double> BeliefDic { get; set; }
        int Opinion { get; set; }
        bool IsSensor { get; set; }
        bool IsEnvironment { get; set; }
        string InitState { get; }
        List<int> MyAgentLinkIDList { get; }
        List<I_AgentLink> MyAgentLinkList { get; set; }

        void SetInitState();
        void Initialize(List<I_AgentLink> agentlink_list);
    }
}
