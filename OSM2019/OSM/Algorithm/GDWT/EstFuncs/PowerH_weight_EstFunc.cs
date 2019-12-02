using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class PowerH_weight_EstFunc : PowerH_awa_EstFunc
  {
    public PowerH_weight_EstFunc(double learning_rate, double power_t) : base(learning_rate, power_t)
    {
    }

    public override I_EstFunc Copy()
    {
      return new PowerH_weight_EstFunc(this.LearningRate, this.Power_T);
    }

    public override double EvaluateErrorFunction(double cur_weight, double cur_awa)
    {
      this.Error = cur_weight - this.EvaluateInverseFunction(cur_awa);
      return this.Error;
    }
  }
}
