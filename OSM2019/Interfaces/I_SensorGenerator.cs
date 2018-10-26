using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Interfaces
{
    interface I_SensorGenerator
    {
        void Generate(List<I_Agent> agent_list);
    }
}
