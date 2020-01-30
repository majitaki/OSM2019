using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class Power_awa_EstFunc : I_EstFunc
  {
    public double LearningRate { get; protected set; }
    public double Power_T { get; protected set; }
    public double LearningThreshold { get; protected set; }
    public double Error { get; protected set; }
    public Power_awa_EstFunc(double learning_rate, double power_t)
    {
      this.Power_T = power_t;
      this.LearningRate = learning_rate;
      this.LearningThreshold = 0.01;
    }

    public virtual I_EstFunc Copy()
    {
      return new Power_awa_EstFunc(this.LearningRate, this.Power_T);
    }
    public virtual double EvaluateFunction(double weight)
    {
      var evaluation = Math.Pow(weight, Math.Pow(Math.E, this.Power_T));
      if (evaluation <= 0.0) return 0;
      if (evaluation >= 1.0) return 1;
      return Math.Round(evaluation, 5);
    }

    public virtual double EvaluateInverseFunction(double awa_rate)
    {
      //var evaluation = Math.Pow(Math.E, Math.Log(awa_rate) / Math.Pow(Math.E, this.Power_T));
      var evaluation = Math.Pow(awa_rate, Math.Exp(-1.0 * this.Power_T));
      if (evaluation <= 0.0) return 0;
      if (evaluation >= 1.0) return 1;
      return Math.Round(evaluation, 5);
    }
    public virtual double EvaluateErrorFunction(double cur_weight, double cur_awa)
    {
      //this.Error = this.EvaluateFunction(cur_weight) - cur_awa;
      if (cur_weight < 0.01) cur_weight = 0.01;
      this.Error = -2.0 * Math.Exp(this.Power_T) * Math.Pow(cur_weight, Math.Exp(this.Power_T)) * (cur_awa - Math.Pow(cur_weight, Math.Exp(this.Power_T))) * Math.Log(cur_weight);
      this.Error *= -1.0;
      if (Double.IsNaN(this.Error))
      {
        var a = 0;
      }

      return this.Error;
    }

    public virtual void EstimateParameter(double cur_weight, double cur_awa)
    {
      //var old_est_awa = this.EvaluateFunction(cur_weight);
      double penalty = this.LearningRate * this.EvaluateErrorFunction(cur_weight, cur_awa);
      if (Math.Abs(penalty) > this.LearningThreshold)
      {
        this.Power_T += penalty;
        this.Power_T = Math.Round(this.Power_T, 5);
        //var est_awa = this.EvaluateFunction(cur_weight);
      }
    }

    public virtual void PrintEstFuncInfo()
    {
      Console.WriteLine($"kai: {this.LearningRate:f3} t: {this.Power_T,3} error: {this.Error,3}");
    }
  }
}
