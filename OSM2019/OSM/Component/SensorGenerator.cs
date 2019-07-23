using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class SensorGenerator
    {
        public int SensorSize { get; private set; }

        public SensorGenerator SetSensorSize(int sensor_size)
        {
            this.SensorSize = sensor_size;
            return this;
        }

        public void Generate(ExtendRandom agent_network_rand, List<Agent> agents)
        {
            //agents.Select(agent => agent.SetSensor(false));
            foreach (var agent in agents)
            {
                agent.SetSensor(false);
            }

            var list = agents.Select(agent => agent.AgentID).OrderBy(id => agent_network_rand.Next()).Take(this.SensorSize)
                       .ToList();
            agents.Where(agent => list.Contains(agent.AgentID)).ToList().ForEach(agent => agent.SetSensor(true));
        }
    }
}
