using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Interfaces
{
    interface I_InitBeliefGenerator
    {
        Dictionary<int, double> Generate();
    }
}
