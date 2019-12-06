using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class GDWT_OSM : OSMBase
  {
    protected double TargetH;
    protected double Epsilon;
    protected Dictionary<Agent, GDWT_Candidate> Candidates;
    protected I_EstFunc BaseEstFunc;
    public int AwaRateWindowSize { get; protected set; }

    public GDWT_OSM() : base()
    {
      this.Epsilon = 0.001;
      this.AwaRateWindowSize = 10;
    }
    public void SetAwaRateWindowSize(int size)
    {
      this.AwaRateWindowSize = size;
    }

    public void SetEstimateFunction(I_EstFunc base_est_func)
    {
      Debug.Assert(base_est_func != null);
      this.BaseEstFunc = base_est_func;
    }

    public override void PrintAgentInfo(Agent agent)
    {
      base.PrintAgentInfo(agent);

      var is_recived = this.MyRecordRound.IsReceived(agent);
      var receive_rounds = this.MyRecordRounds.Where(record_round => record_round.IsReceived(agent)).Count();
      if (is_recived) receive_rounds++;

      var candidate = this.Candidates[agent];

      var can_weight = candidate.CanWeight;
      var window_h = candidate.GetWindowAwaRate();
      var awa_count = candidate.AwaCount;
      var h = candidate.GetAwaRate(this.CurrentRound);

      Console.WriteLine($"can_weight: {can_weight:f3} awa_count: {awa_count,3} h_rcv_round: {receive_rounds,3} cur_round: {this.CurrentRound,3} h: {h:f4} wh: {window_h:f4}");
      candidate.EstFunc.PrintEstFuncInfo();
    }


    protected virtual void SetCandidate()
    {
      this.Candidates = new Dictionary<Agent, GDWT_Candidate>();
      foreach (var agent in this.MyAgentNetwork.Agents)
      {
        var can = new GDWT_Candidate(agent, this.BaseEstFunc.Copy(), this.AwaRateWindowSize);
        this.Candidates.Add(agent, can);
        var initial_h = 0.0;
        var est_weight = can.EstimateWeight(initial_h);
        agent.SetCommonWeight(est_weight);
        can.CanWeight = est_weight;
      }
    }

    public override void SetAgentNetwork(AgentNetwork agent_network)
    {
      base.SetAgentNetwork(agent_network);
      this.SetCandidate();

      return;
    }

    public void SetTargetH(double target_h)
    {
      this.TargetH = target_h;
      return;
    }

    //step

    //round
    public override void InitializeToFirstRound()
    {
      base.InitializeToFirstRound();
      this.SetCandidate();
    }

    public override void FinalizeRound()
    {
      this.EstimateAwaRate();
      this.EstimateFuncParameter();
      this.SelectionWeight();
      base.FinalizeRound();
    }

    protected virtual void EstimateAwaRate()
    {
      foreach (var candidate in this.Candidates)
      {
        var received_sum_op = this.MyRecordRounds.Last().AgentReceiveOpinionsInRound[candidate.Key];
        double obs_u = this.GetObsU(received_sum_op);
        if (obs_u == 0) continue;
        this.UpdateAveAwaRates(candidate.Key, candidate.Value);
      }
    }

    protected virtual void EstimateFuncParameter()
    {
      foreach (var candidate in this.Candidates)
      {
        var received_sum_op = this.MyRecordRounds.Last().AgentReceiveOpinionsInRound[candidate.Key];
        double obs_u = this.GetObsU(received_sum_op);
        if (obs_u == 0) continue;

        candidate.Value.EstimateParameter(this.CurrentRound);
      }
    }

    protected virtual void SelectionWeight()
    {
      foreach (var candidate in this.Candidates)
      {
        var received_sum_op = this.MyRecordRounds.Last().AgentReceiveOpinionsInRound[candidate.Key];
        double obs_u = this.GetObsU(received_sum_op);
        if (obs_u == 0) continue;

        var current_h = candidate.Value.GetWindowAwaRate();
        //var current_h = candidate.Value.GetAwaRate(this.CurrentRound);

        if (current_h < this.TargetH + this.Epsilon || current_h > this.TargetH - this.Epsilon)
        {
          var new_weight = candidate.Value.EstimateWeight(this.TargetH);
          candidate.Value.CanWeight = new_weight;
          candidate.Key.SetCommonWeight(new_weight);
          Debug.Assert(new_weight >= 0 && new_weight <= 1);
        }
        //Debug.Assert(candidate.Value.GetAwaRate(this.CurrentRound) == candidate.Value.GetWindowAwaRate());
      }
    }

    protected virtual void UpdateAveAwaRates(Agent agent, GDWT_Candidate candidate)
    {
      if (this.IsDetermined(agent))
      {
        candidate.AwaCount += 1;
      }
      else
      {
        candidate.AwaCount += 0;
      }
    }

    protected virtual bool IsDetermined(Agent agent)
    {
      return agent.IsDetermined();
    }

  }
}
