using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class AAT_OSM : OSMBase<AAT_OSM>
    {
        double TargetH;

        public AAT_OSM SetTargetH(double target_h)
        {
            this.TargetH = target_h;
            return this;
        }
    }
}
