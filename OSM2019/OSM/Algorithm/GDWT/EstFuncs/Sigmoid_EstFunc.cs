using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class Sigmoid_EstFunc : I_EstFunc
  {
    public double LearningRate { get; private set; }
    public double Translation { get; private set; }
    public double Slope { get; private set; }
    public double LearningThreshold { get; private set; }
    public double Error { get; private set; }
    public Sigmoid_EstFunc(double learning_rate, double translation, double slope)
    {
      this.Translation = translation;
      this.Slope = slope;
      this.LearningRate = learning_rate;
      this.LearningThreshold = 0.01;
    }

    public I_EstFunc Copy()
    {
      return new Sigmoid_EstFunc(this.LearningRate, this.Translation, this.Slope);
    }
    public double EvaluateFunction(double weight, double param)
    {
      return 1.0 / (1.0 + Math.Pow(Math.E, -1.0 * this.Slope * (weight - param)));
    }

    public double EvaluateInverseFunction(double awa_rate)
    {
      return ((-1.0 * Math.Log(1.0 / awa_rate - 1.0)) / this.Slope) + this.Translation;

    }

    public double EvaluateErrorFunction(double weight, double awa_rate, double param)
    {
      this.Error = awa_rate - this.EvaluateFunction(weight, param);
      return this.Error;
    }

    public void EstimateParameter(double weight, double new_awa_rate)
    {
      double penalty = this.LearningRate * this.EvaluateErrorFunction(weight, new_awa_rate, this.Translation);
      if (Math.Abs(penalty) > this.LearningThreshold)
      {
        this.Translation -= penalty;
      }
    }

    public void PrintEstFuncInfo()
    {
      Console.WriteLine($"kai: {this.LearningRate:f3} trans: {this.Translation,3} slope: {this.Slope,3} error: {this.Error,3}");
    }
  }
}
