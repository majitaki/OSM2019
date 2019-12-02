using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class GDWT_Candidate
  {
    public I_EstFunc EstFunc;
    public int AwaWindowSize { get; private set; }
    public Queue<int> AwaCounts { get; private set; }
    public int AwaCount {
      get {
        if (this.AwaCounts.Count == 0) return 0;
        return this.AwaCounts.Last();
      }
      set {
        this.AwaCounts.Enqueue(value);
        if (this.AwaCounts.Count > this.AwaWindowSize) this.AwaCounts.Dequeue();
      }
    }
    public double CanWeight { get; set; }

    public GDWT_Candidate(Agent agent, I_EstFunc estfunc, int awa_window_size = 1)
    {
      if (agent.GetNeighbors().Count == 0) return;
      this.AwaCounts = new Queue<int>();
      this.AwaWindowSize = awa_window_size;
      this.CanWeight = 0.0;
      this.EstFunc = estfunc;
    }

    public double EvaluateWeight(double awa_rate)
    {
      if (awa_rate <= 0.0) return 0;
      if (awa_rate >= 1.0) return 1;

      var weight = this.EstFunc.EvaluateInverseFunction(awa_rate);
      return weight;
    }

    public void EstimateParameter(int current_round)
    {
      //this.EstFunc.EstimateParameter(this.CanWeight, this.GetWindowAwaRate());
      this.EstFunc.EstimateParameter(this.CanWeight, this.GetAwaRate(current_round));
    }

    public double GetWindowAwaRate()
    {
      var current_count = this.AwaCount;
      var prepre_count = (this.AwaCounts.Count == 0) ? 0 : this.AwaCounts.Peek();
      double window_h = 0.0;
      if (this.AwaCounts.Count != 0)
      {
        window_h = (current_count - prepre_count) / (double)this.AwaCounts.Count;
      }
      //var window_h = (this.AwaRates.Count == 0) ? 0 : (current_count - prepre_count) / this.AwaRates.Count;
      Debug.Assert(window_h >= 0 && window_h <= 1);
      return window_h;
    }

    public double GetAwaRate(int current_round)
    {
      if (current_round == 0) return 0.0;
      var cur_awa = this.AwaCount / (double)(current_round + 0);
      //Debug.Assert(cur_awa >= 0 && cur_awa <= 1);
      return cur_awa;
    }
  }
}
