﻿using MathNet.Numerics.LinearAlgebra;
using OSM2019.OSM;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019
{
  class SubjectManagerGenerator
  {
    public SubjectManager Generate(OpinionSubject opinion_subject, double dist_weight, int correct_dim, double sensor_weight, EnvDistributionEnum env_dis_mode, int malicious_dim = 0, double malicious_dist_weight = 0.0)
    {
      CustomDistribution env_dist = null;
      CustomDistribution env_malicious_dist = null;
      switch (env_dis_mode)
      {
        case EnvDistributionEnum.Turara:
          env_dist = new Turara_DistGenerator(opinion_subject.SubjectDimSize, dist_weight, correct_dim).Generate();
          env_malicious_dist = new Turara_DistGenerator(opinion_subject.SubjectDimSize, malicious_dist_weight, malicious_dim).Generate();
          break;
        case EnvDistributionEnum.Exponential:
          env_dist = new Exponential_DistGenerator(opinion_subject.SubjectDimSize, dist_weight, correct_dim).Generate();
          env_malicious_dist = new Exponential_DistGenerator(opinion_subject.SubjectDimSize, malicious_dist_weight, malicious_dim).Generate();
          break;
      }
      Debug.Assert(env_dist != null);
      Debug.Assert(env_malicious_dist != null);

      var subject_tv = new OpinionSubject("good_tv", 3);
      var subject_test = new OpinionSubject("test", opinion_subject.SubjectDimSize);
      var subject_company = new OpinionSubject("good_company", 2);
      double[] conv_array = { 1, 0, 0, 1, 1, 0 };
      var conv_matrix = Matrix<double>.Build.DenseOfColumnMajor(2, 3, conv_array);

      var osm_env = new OpinionEnvironment()
                    .SetSubject(subject_test)
                    .SetCorrectDim(correct_dim)
                    .SetMaliciousDim(malicious_dim)
                    .SetSensorWeight(sensor_weight)
                    .SetCustomDistribution(env_dist)
                    .SetMaliciousCustomDistribution(env_malicious_dist);


      var subject_manager = new SubjectManager()
                              .AddSubject(subject_test)
                              .RegistConversionMatrix(subject_tv, subject_company, conv_matrix)
                              .SetEnvironment(osm_env);
      return subject_manager;
    }
  }
}
