using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class AgentNetwork
    {
        ExtendRandom MyRand;
        RawGraph MyGraph;
        Layout MyLayout;
        List<SampleAgent> SampleAgents;
        SensorGenerator MySensorGene;
        List<Agent> Agents;
        List<AgentLink> AgentLinks;

        public AgentNetwork()
        {
            this.Agents = new List<Agent>();
            this.AgentLinks = new List<AgentLink>();
        }

        public AgentNetwork SetRand(ExtendRandom ex_rand)
        {
            this.MyRand = ex_rand;
            return this;
        }

        public AgentNetwork GenerateNetworkFrame(RawGraph graph)
        {
            this.MyGraph = graph;

            foreach (var node in graph.Nodes)
            {
                this.Agents.Add(new Agent(node));
            }

            int link_index = 0;
            foreach (var link in graph.Links)
            {
                this.AgentLinks.Add(new AgentLink(link_index).SetLink(link, this.Agents));
                link_index++;
            }

            foreach (var agent in this.Agents)
            {
                agent.AttachAgentLinks(this.AgentLinks);
            }

            return this;
        }

        public AgentNetwork ApplySampleAgent(SampleAgent sample_agent, BaseAgentMode mode, double random_set_rate = 0.0)
        {
            switch (mode)
            {
                case BaseAgentMode.RandomSetRate:

                    break;
                case BaseAgentMode.RemainSet:
                    break;
                default:
                    break;
            }
            return this;
        }

        public AgentNetwork SetSensorGene(SensorGenerator sensor_gene)
        {

            return this;
        }

        public AgentNetwork SetLayout(Layout layout)
        {

            return this;
        }

    }
}
