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
        bool IsSensor { get; }
        bool IsEnvironment { get;}
    }
}
