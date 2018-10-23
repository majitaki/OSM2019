using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Interfaces
{
    interface I_AgentManagerGenerator
    {
        I_AgentManager Generate(int agent_seed, I_InitBeliefGenerator init_belief_generator, I_SensorGenerator sensor_generator);
    }
}
