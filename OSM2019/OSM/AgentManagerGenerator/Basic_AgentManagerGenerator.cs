using OSM2019.Interfaces;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class Basic_AgentManagerGenerator : I_AgentManagerGenerator
    {
        RawGraph MyGraph;
        int OpinionSize;
        int InitOpinion;
        double OpinionThreshold;

        public Basic_AgentManagerGenerator(RawGraph graph, int opinion_size, int init_opinion, double op_threshold)
        {
            this.MyGraph = graph;
            this.InitOpinion = init_opinion;
            this.OpinionSize = opinion_size;
            this.OpinionThreshold = op_threshold;
        }

        public I_AgentManager Generate(int agent_seed, I_InitBeliefGenerator init_belief_generator, I_SensorGenerator sensor_generator)
        {
            List<I_Agent> agent_list = new List<I_Agent>();
            List<I_AgentLink> agentlink_list = new List<I_AgentLink>();

            RandomPool.Declare(SeedEnum.AgentGenerateSeed, agent_seed);

            int index = 0;
            foreach (var node in this.MyGraph.Nodes)
            {
                var belief_dic = init_belief_generator.Generate(this.OpinionSize);
                var link_list = this.MyGraph.GetLinksOfSource(node.ID);
                var local_agentlink_list = new List<I_AgentLink>();

                foreach (var link in link_list)
                {
                    var agent_link = new BasicAgentLink(index++, link, this.OpinionSize);
                    local_agentlink_list.Add(agent_link);
                }

                var agent = new BasicAgent(node.ID, belief_dic, local_agentlink_list.Select(a_link => a_link.AgentLinkID).ToList(), this.OpinionSize);
                agent_list.Add(agent);
                agentlink_list.AddRange(local_agentlink_list);
            }

            return new BasicAgentManager(agent_list, agentlink_list);
        }
    }
}
