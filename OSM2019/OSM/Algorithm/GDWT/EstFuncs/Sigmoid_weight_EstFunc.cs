using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class Sigmoid_weight_EstFunc : Sigmoid_awa_EstFunc
  {
    public Sigmoid_weight_EstFunc(double learning_rate, double translation, double slope) : base(learning_rate, translation, slope)
    {
    }

    public override I_EstFunc Copy()
    {
      return new Sigmoid_weight_EstFunc(this.LearningRate, this.Translation, this.Slope);
    }

    public override double EvaluateErrorFunction(double cur_weight, double cur_awa)
    {
      //this.Error = cur_weight - this.EvaluateInverseFunction(cur_awa);

      if (cur_awa < 0.01) cur_awa = 0.01;
      if (cur_awa > 0.99) cur_awa = 0.99;
      this.Error = (2.0 * Math.Log(cur_awa / (1.0 - cur_awa))) / this.Slope - 2.0 * cur_weight + 2.0 * this.Translation + 1.0;
      this.Error *= -1.0;
      if (Double.IsNaN(this.Error))
      {
        var a = 0;
      }
      return this.Error;
    }
  }
}
