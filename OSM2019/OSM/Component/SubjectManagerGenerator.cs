using MathNet.Numerics.LinearAlgebra;
using OSM2019.OSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019
{
    class SubjectManagerGenerator
    {
        public SubjectManager Generate(int dim, double turara_weight, int correct_dim, double sensor_rate)
        {
            var env_dist = new Turara_DistGenerator(dim, turara_weight, correct_dim).Generate();
            var subject_test = new OpinionSubject("test", dim);
            var osm_env = new OpinionEnvironment()
                          .SetSubject(subject_test)
                          .SetCorrectDim(correct_dim)
                          .SetSensorRate(sensor_rate)
                          .SetCustomDistribution(env_dist);

            var subject_manager = new SubjectManager()
                              .AddSubject(subject_test)
                              //.RegistConversionMatrix(subject_tv, subject_company, conv_matrix)
                              .SetEnvironment(osm_env);
            return subject_manager;
        }
    }
}
