using OSM2019.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class Basic_AgentManagerGenerator:I_AgentManagerGenerator
    {
        RawGraph MyGraph;
        int OpinionSize;
        int InitOpinion;
        double OpinionThreshold;

        public Basic_AgentManagerGenerator(RawGraph graph, int opinion_size, int init_opinion, double op_threshold )
        {
            this.MyGraph = graph;
            this.InitOpinion = init_opinion;
            this.OpinionThreshold = op_threshold;
        }

        public I_AgentManager Generate(int agent_seed, I_InitBeliefGenerator init_belief_generator, I_SensorGenerator sensor_generator)
        {

        }
    }
}
