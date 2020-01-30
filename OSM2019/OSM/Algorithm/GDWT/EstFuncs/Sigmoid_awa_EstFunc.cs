using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class Sigmoid_awa_EstFunc : I_EstFunc
  {
    public double LearningRate { get; protected set; }
    public double Translation { get; protected set; }
    public double Slope { get; protected set; }
    public double LearningThreshold { get; protected set; }
    public double Error { get; protected set; }
    public Sigmoid_awa_EstFunc(double learning_rate, double translation, double slope)
    {
      this.Translation = translation;
      this.Slope = slope;
      this.LearningRate = learning_rate;
      this.LearningThreshold = 0.0;
    }

    public virtual I_EstFunc Copy()
    {
      return new Sigmoid_awa_EstFunc(this.LearningRate, this.Translation, this.Slope);
    }
    public virtual double EvaluateFunction(double weight)
    {
      if (weight <= 0.01) return 0.01;
      if (weight >= 0.99) return 0.99;
      //var evaluation = 1.0 / (1.0 + Math.Pow(Math.E, (-2.0 * this.Slope * ((weight - this.Translation) - 0.5))));
      var evaluation = 1.0 / (1.0 + Math.Pow(Math.E, (-1.0 * this.Slope * ((weight - this.Translation) - 0.5))));
      if (evaluation <= 0.0) return 0;
      if (evaluation >= 1.0) return 1;
      return Math.Round(evaluation, 5);
    }

    public virtual double EvaluateInverseFunction(double awa_rate)
    {
      if (awa_rate <= 0.01) return 0.01;
      if (awa_rate >= 0.99) return 0.99;
      var evaluation = (this.Translation + 0.5) + (Math.Log((awa_rate / (1 - awa_rate)))) / this.Slope;
      //var evaluation = ((2 * this.Translation + 0.5) + (Math.Log((awa_rate / (1 - awa_rate)))) / this.Slope) / 2.0;
      if (evaluation <= 0.0) return 0;
      if (evaluation >= 1.0) return 1;
      return Math.Round(evaluation, 5);
    }

    public virtual double EvaluateErrorFunction(double cur_weight, double cur_awa)
    {
      var est_awa = this.EvaluateFunction(cur_weight);
      this.Error = est_awa - cur_awa;
      return this.Error;
    }

    public virtual void EstimateParameter(double cur_weight, double cur_awa)
    {
      double penalty = this.LearningRate * this.EvaluateErrorFunction(cur_weight, cur_awa);
      //this.Translation += penalty;
      this.Translation = penalty;
    }

    public virtual void PrintEstFuncInfo()
    {
      Console.WriteLine($"kai: {this.LearningRate:f3} trans: {this.Translation,3} slope: {this.Slope,3} error: {this.Error,3}");
    }
  }
}
