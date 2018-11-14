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
        public string EnvSubject { get; protected set; }
        public int CorrectDim { get; protected set; }

        public EnvironmentManager SetSubject(string subject)
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
            Agent env_agent = new Agent(-1);
            List<AgentLink> env_links = new List<AgentLink>();
            var sensors = agent_network.Agents.Where(agent => agent.IsSensor).ToList();

            int link_index = -1;
            foreach (var sensor in sensors)
            {
                env_links.Add(new AgentLink(link_index, sensor, env_agent));
                link_index--;
            }

            foreach (var sensor in sensors)
            {
                sensor.AttachAgentLinks(env_links);
            }

            env_agent.AttachAgentLinks(env_links);
        }

    }
}
