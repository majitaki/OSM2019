using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class OSM_Only : OSMBase
    {
        public void SetCommonWeight(double common_weight)
        {
            foreach (var link in this.MyAgentNetwork.AgentLinks)
            {
                link.SetInitSourceWeight(common_weight);
                link.SetInitTargetWeight(common_weight);
            }
        }
    }
}
