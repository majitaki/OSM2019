using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class SensorGenerator
  {
    public int SensorSize { get; private set; }
    public int MaliciousSensorSize { get; private set; }

    public SensorGenerator SetSensorSize(int sensor_size, int malicious_sensor_size = 0)
    {
      Debug.Assert(sensor_size >= malicious_sensor_size);
      this.SensorSize = sensor_size;
      this.MaliciousSensorSize = malicious_sensor_size;
      return this;
    }

    public void Generate(ExtendRandom agent_network_rand, List<Agent> agents)
    {
      foreach (var agent in agents)
      {
        agent.SetSensor(false);
      }

      var sensor_list = agents.Select(agent => agent.AgentID).OrderBy(id => agent_network_rand.Next()).Take(this.SensorSize)
                 .ToList();
      var malicious_sensor_list = sensor_list.OrderBy(id => agent_network_rand.Next()).Take(this.MaliciousSensorSize).ToList();

      agents.Where(agent => sensor_list.Contains(agent.AgentID)).ToList().ForEach(agent => agent.SetSensor(true, false));
      agents.Where(agent => malicious_sensor_list.Contains(agent.AgentID)).ToList().ForEach(agent => agent.SetSensor(true, true));
    }
  }
}
