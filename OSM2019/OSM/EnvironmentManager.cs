using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class EnvironmentManager
    {
        double SensorRate;
        string EnvSubject;
        int CorrectDim;

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
    }
}
