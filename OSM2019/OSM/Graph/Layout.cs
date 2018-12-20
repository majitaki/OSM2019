using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class Layout
    {
        public LayoutEnum MyLayoutEnum { get; set; }
        public List<Vector2> PosList { get; }

        public Layout()
        {

        }

        public Layout(List<Vector2> pos_list, LayoutEnum layout_enum)
        {
            this.MyLayoutEnum = layout_enum;
            this.PosList = pos_list;
        }

        public Vector2 GetAgentPosition(Agent agent)
        {
            return this.PosList[agent.AgentID];
        }

        public (Vector2 source_pos, Vector2 target_pos) GetLinkPosition(AgentLink agent_link)
        {
            var s_pos = this.GetAgentPosition(agent_link.SourceAgent);
            var t_pos = this.GetAgentPosition(agent_link.TargetAgent);

            return (s_pos, t_pos);
        }
    }
}
