using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  interface I_EstFunc
  {
    I_EstFunc Copy();
    double EvaluateFunction(double weight, double param);
    double EvaluateInverseFunction(double awa_rate);
    double EvaluateErrorFunction(double weight, double awa_rate, double param);
    void EstimateParameter(double weight, double new_awa_rate);
    void PrintEstFuncInfo();
  }
}
