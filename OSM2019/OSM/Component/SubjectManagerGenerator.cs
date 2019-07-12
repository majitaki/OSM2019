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
        public SubjectManager Generate(OpinionSubject opinion_subject, double turara_weight, int correct_dim, double sensor_rate)
        {
            var env_dist = new Turara_DistGenerator(opinion_subject.SubjectDimSize, turara_weight, correct_dim).Generate();
            var subject_tv = new OpinionSubject("good_tv", 3);
            var subject_test = new OpinionSubject("test", opinion_subject.SubjectDimSize);
            var subject_company = new OpinionSubject("good_company", 2);
            double[] conv_array = { 1, 0, 0, 1, 1, 0 };
            var conv_matrix = Matrix<double>.Build.DenseOfColumnMajor(2, 3, conv_array);

            var osm_env = new OpinionEnvironment()
                          .SetSubject(subject_test)
                          .SetCorrectDim(correct_dim)
                          .SetSensorRate(sensor_rate)
                          .SetCustomDistribution(env_dist);

            var subject_manager = new SubjectManager()
                              .AddSubject(subject_test)
                              .RegistConversionMatrix(subject_tv, subject_company, conv_matrix)
                              .SetEnvironment(osm_env);
            return subject_manager;
        }
    }
}
