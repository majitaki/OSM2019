using OSM2019.Interfaces;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class BasicSensorGenerator : I_SensorGenerator
    {
        bool IsRateUse;
        int SensorSize;
        double SensorRate;

        public BasicSensorGenerator(int sensor_size)
        {
            this.IsRateUse = false;
            this.SensorSize = sensor_size;
        }

        public BasicSensorGenerator(double sensor_rate)
        {
            this.IsRateUse = true;
            this.SensorRate = sensor_rate;
        }

        public void Generate(List<I_Agent> agent_list)
        {
            if (IsRateUse)
            {
                this.SensorSize = (int)(agent_list.Count * this.SensorRate);
            }

            var rand = RandomPool.Get(SeedEnum.AgentGenerateSeed);
            var list = agent_list.Select(agent => agent.AgentID).OrderBy(id => rand.Next()).Take(this.SensorSize).ToList();
            agent_list.Where(agent => list.Contains(agent.AgentID)).ToList().ForEach(agent => agent.IsSensor = true);
        }

    }
}
