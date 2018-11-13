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
        ExtendRandom AgentGenerateRand;
        public RawGraph MyGraph { get; private set; }
        public Layout MyLayout { get; private set; }
        public SubjectManager MySubjectManager { get; private set; }
        public List<Agent> Agents { get; private set; }
        public List<AgentLink> AgentLinks { get; private set; }

        public AgentNetwork()
        {
            this.Agents = new List<Agent>();
            this.AgentLinks = new List<AgentLink>();
        }

        public AgentNetwork SetRand(ExtendRandom agent_gene_rand)
        {
            this.AgentGenerateRand = agent_gene_rand;
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

        public AgentNetwork ApplySampleAgent(SampleAgent sample_agent, SampleAgentSetMode mode, double random_set_rate = 0.0)
        {
            switch (mode)
            {
                case SampleAgentSetMode.RandomSetRate:
                    if (random_set_rate == 0.0) new Exception(nameof(AgentNetwork) + " Error no random set rate");

                    var set_agent_size = (int)(this.MyGraph.Nodes.Count * random_set_rate);
                    var list = this.Agents.Select(agent => agent.AgentID).OrderBy(id => this.AgentGenerateRand.Next()).Take(set_agent_size)
                        .ToList();
                    this.Agents.Where(agent => list.Contains(agent.AgentID)).ToList().ForEach(agent => sample_agent.Generate(this.AgentGenerateRand, agent));
                    break;
                case SampleAgentSetMode.RemainSet:
                    this.Agents.Where(agent => agent.InitBelief == null).ToList().ForEach(agent => sample_agent.Generate(this.AgentGenerateRand, agent));
                    break;
                default:
                    break;
            }
            return this;
        }

        public AgentNetwork GenerateSensor(SensorGenerator sensor_gene)
        {
            sensor_gene.Generate(this.AgentGenerateRand, this.Agents);
            return this;
        }

        public AgentNetwork SetLayout(Layout layout)
        {
            this.MyLayout = layout;
            return this;
        }

        public AgentNetwork SetSubjectManager(SubjectManager subject_manager)
        {
            this.MySubjectManager = subject_manager;
            return this;
        }
    }
}
