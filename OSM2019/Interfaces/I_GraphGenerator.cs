using OSM2019.OSM;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Interfaces
{
    interface I_GraphGenerator
    {
        GraphEnum MyGraphEnum { get; }
        RawGraph Generate(int graph_seed);
    }
}
