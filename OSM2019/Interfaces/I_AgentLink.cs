using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Interfaces
{
    interface I_AgentLink
    {
        int AgentLinkID { get; }
        int SourceAgentID { get; }
        int TargetAgentID { get; }
        double SourceWeight { get; set; }
        double TargetWeight { get; set; }
        I_Agent SourceAgent { get; }
        I_Agent TargetAgent { get; }
        string InitState { get; }

        void SetInitState();
        void Initialize(List<I_Agent> agent_list);
    }
}
