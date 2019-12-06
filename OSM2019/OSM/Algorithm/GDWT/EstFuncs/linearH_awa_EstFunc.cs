using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class Linear_awa_EstFunc : I_EstFunc
  {
    public double LearningRate { get; protected set; }
    public double Translation { get; protected set; }
    public double Slope { get; protected set; }
    public double LearningThreshold { get; protected set; }
    public double Error { get; protected set; }
    public Linear_awa_EstFunc(double learning_rate, double slope, double translation)
    {
      this.Slope = slope;
      this.Translation = translation;
      this.LearningRate = learning_rate;
      this.LearningThreshold = 0.01;
    }

    public virtual I_EstFunc Copy()
    {
      return new Linear_awa_EstFunc(this.LearningRate, this.Slope, this.Translation);
    }
    public virtual double EvaluateFunction(double weight)
    {
      var evaluation = this.Slope * this.Translation;
      if (evaluation <= 0.0) return 0;
      if (evaluation >= 1.0) return 1;
      return Math.Round(evaluation, 5);
    }

    public virtual double EvaluateInverseFunction(double awa_rate)
    {
      var evaluation = Math.Pow(Math.E, Math.Log(awa_rate) / Math.Pow(Math.E, this.Translation));
      if (evaluation <= 0.0) return 0;
      if (evaluation >= 1.0) return 1;
      return Math.Round(evaluation, 5);
    }
    public virtual double EvaluateErrorFunction(double cur_weight, double cur_awa)
    {
      this.Error = this.EvaluateFunction(cur_weight) - cur_awa;
      return this.Error;
    }

    public virtual void EstimateParameter(double cur_weight, double cur_awa)
    {
      //var old_est_awa = this.EvaluateFunction(cur_weight);
      double penalty = this.LearningRate * this.EvaluateErrorFunction(cur_weight, cur_awa);
      if (Math.Abs(penalty) > this.LearningThreshold)
      {
        this.Translation += penalty;
        this.Translation = Math.Round(this.Translation, 5);
        //var est_awa = this.EvaluateFunction(cur_weight);
      }
    }

    public virtual void PrintEstFuncInfo()
    {
      Console.WriteLine($"kai: {this.LearningRate:f3} t: {this.Translation,3} error: {this.Error,3}");
    }
  }
}
