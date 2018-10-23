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

        public Layout(List<Vector2> pos_list, LayoutEnum layout_enum)
        {
            this.MyLayoutEnum = layout_enum;
            this.PosList = pos_list;
        }
    }
}
