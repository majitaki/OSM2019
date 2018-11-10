using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class SensorGenerator
    {
        int SensorSize;

        public SensorGenerator SetSensorSize(int sensor_size)
        {
            this.SensorSize = sensor_size;
            return this;
        }
    }
}
