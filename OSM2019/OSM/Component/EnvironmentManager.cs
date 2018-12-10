using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class EnvironmentManager
    {
        public double SensorRate { get; protected set; }
        public OpinionSubject EnvSubject { get; protected set; }
        public int CorrectDim { get; protected set; }
        Agent EnvironmentAgent;

        public EnvironmentManager SetSubject(OpinionSubject subject)
        {
            this.EnvSubject = subject;
            return this;
        }

        public EnvironmentManager SetCorrectDim(int cor_dim)
        {
            this.CorrectDim = cor_dim;
            return this;
        }

        public EnvironmentManager SetSensorRate(double sensor_rate)
        {
            this.SensorRate = sensor_rate;
            return this;
        }

        public void AddEnvironment(AgentNetwork agent_network)
        {
            this.EnvironmentAgent = new Agent(-1).SetSubject(this.EnvSubject);
            var op_vector = Vector<double>.Build.Dense(this.EnvSubject.SubjectDimSize, 0.0);
            this.EnvironmentAgent.SetInitOpinion(op_vector);
            List<AgentLink> env_links = new List<AgentLink>();
            var sensors = agent_network.Agents.Where(agent => agent.IsSensor).ToList();

            int link_index = -1;
            foreach (var sensor in sensors)
            {
                env_links.Add(new AgentLink(link_index, sensor, this.EnvironmentAgent));
                link_index--;
            }

            foreach (var sensor in sensors)
            {
                sensor.AttachAgentLinks(env_links);
            }

            this.EnvironmentAgent.AttachAgentLinks(env_links);
        }

        public List<Message> SendMessages(List<Agent> sensor_agents, ExtendRandom update_step_rand)
        {
            List<Message> messages = new List<Message>();
            foreach (var sensor_agent in sensor_agents)
            {
                var agent_link = this.EnvironmentAgent.AgentLinks.Where(link => link.SourceAgent == sensor_agent || link.TargetAgent == sensor_agent).First();
                var opinion = this.EnvironmentAgent.Opinion.Clone();
                opinion.Clear();

                if (update_step_rand.NextDouble() < this.SensorRate)
                {
                    opinion[this.CorrectDim] = 1.0;
                }
                else
                {
                    List<int> incor_dim_list = Enumerable.Range(0, opinion.Count).Where(i => i != this.CorrectDim).ToList();
                    int incor_dim = incor_dim_list.OrderBy(_ => update_step_rand.Next()).First();
                    opinion[incor_dim] = 1.0;
                }

                messages.Add(new Message(this.EnvironmentAgent, sensor_agent, agent_link, opinion));
            }

            return messages;
        }
    }
}
