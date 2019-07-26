using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class LinkInfoValue
  {
    public Agent ReceiveAgent { get; private set; }
    public Agent SendAgent { get; private set; }
    public Queue<double> RecOpinionProbs { get; private set; }
    public double RecOpinionProb {
      get {
        if (RecOpinionProbs.Count == 0) return 1;
        return this.RecOpinionProbs.Last();
      }
      set {
        this.RecOpinionProbs.Enqueue(value);
        if (this.RecOpinionProbs.Count > this.WindowSize) this.RecOpinionProbs.Dequeue();
      }
    }

    public int WindowSize { get; private set; }

    public LinkInfoValue(Agent receiver, Agent sender, int window_size)
    {
      this.ReceiveAgent = receiver;
      this.SendAgent = sender;
      this.RecOpinionProbs = new Queue<double>();
      this.WindowSize = window_size;
    }

    public void Regist(Vector<double> op, Vector<double> belief)
    {
      Debug.Assert(op.Count == belief.Count);
      double p = 1.0;
      double each_p = 0.0;


      foreach (var i in Enumerable.Range(0, op.Count))
      {
        each_p = Math.Round(Math.Pow(belief[i], op[i]), 5);
        if (each_p == 0 || belief[i] == 0 || op[i] == 0) continue;
        p *= each_p;
      }


      //foreach (var op_value in op)
      //{
      //  foreach (var belief_value in belief)
      //  {
      //    if (op_value == 0 || belief_value == 0) continue;
      //    var each_p = Math.Round(Math.Pow(belief_value, op_value), 5);
      //    if (each_p == 0) continue;
      //    p *= each_p;
      //  }
      //}

      Debug.Assert(p != 0);
      this.RecOpinionProb = p;

    }

    public double GetInfoValueSum()
    {
      double info_value_sum = 0.0;
      foreach (var p in this.RecOpinionProbs)
      {
        info_value_sum += -1 * Math.Log(p, 2);
      }
      return info_value_sum;
    }
  }
}
