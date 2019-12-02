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
    double EvaluateFunction(double weight);
    double EvaluateInverseFunction(double awa);
    double EvaluateErrorFunction(double cur_weight, double cur_awa);
    void EstimateParameter(double cur_weight, double cur_awa);
    void PrintEstFuncInfo();
  }
}
