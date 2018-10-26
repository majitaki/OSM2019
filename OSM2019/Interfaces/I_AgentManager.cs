using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Interfaces
{
    interface I_AgentManager
    {
        List<I_Agent> AgentList { get; }
        List<I_AgentLink> AgentLinkList { get; }

        void Initialize();
        void SetInitState();
    }
}
