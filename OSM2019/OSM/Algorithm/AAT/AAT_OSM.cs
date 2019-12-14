using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
  class AAT_OSM : OSMBase
  {
    protected double TargetH;
    protected double Epsilon;
    protected Dictionary<Agent, Candidate> Candidates;
    public int AwaRateWindowSize { get; protected set; }

    public AAT_OSM() : base()
    {
      //this.Epsilon = 0.05;
      this.Epsilon = 0.00;
      this.AwaRateWindowSize = 1;
    }

    public override void PrintAgentInfo(Agent agent)
    {
      base.PrintAgentInfo(agent);

      var is_recived = this.MyRecordRound.IsReceived(agent);
      var receive_rounds = this.MyRecordRounds.Where(record_round => record_round.IsReceived(agent)).Count();
      if (is_recived) receive_rounds++;

      var candidate = this.Candidates[agent];

      int can_index = 0;
      foreach (var record in candidate.SortedDataBase)
      {
        var select = (candidate.GetCurrentSelectRecord() == record) ? "*" : " ";
        var can_weight = record.CanWeight;
        var req_num = record.RequireOpinionNum;
        var awa_count = record.AwaCount;
        var h = record.AwaRate;
        Console.WriteLine($"{select} index: {can_index,3} req: {req_num,3} can_weight: {can_weight:f3} awa_count: {awa_count,3} h_rcv_round: {receive_rounds,3} h: {h:f4} {select}");
        can_index++;
      }
    }


    protected virtual void SetCandidate()
    {
      this.Candidates = new Dictionary<Agent, Candidate>();
      foreach (var agent in this.MyAgentNetwork.Agents)
      {
        var can = new Candidate(agent, this.AwaRateWindowSize);
        this.Candidates.Add(agent, can);
        agent.SetCommonWeight(can.GetSelectCanWeight());
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
        this.UpdateAveAwaRates(candidate.Key, candidate.Value, obs_u);
      }
    }

    protected virtual void SelectionWeight()
    {
      foreach (var candidate in this.Candidates)
      {
        var received_sum_op = this.MyRecordRounds.Last().AgentReceiveOpinionsInRound[candidate.Key];
        double obs_u = this.GetObsU(received_sum_op);
        if (obs_u == 0) continue;


        var current_h = candidate.Value.GetCurrentSelectRecord().AwaRate;
        var current_l = candidate.Value.SelectSortedIndex;
        var can_size = candidate.Value.SortedDataBase.Count;

        if (current_l < can_size - 1 && current_h < this.TargetH)
        {
          candidate.Value.SelectSortedIndex++;
        }
        else if (current_l > 0)
        {
          var pre_h = candidate.Value.GetSortedRecord(current_l - 1).AwaRate;
          if (pre_h >= (this.TargetH + this.Epsilon))
          {
            candidate.Value.SelectSortedIndex--;
          }
        }

        candidate.Key.SetCommonWeight(candidate.Value.GetSelectCanWeight());
      }
    }



    protected virtual void UpdateAveAwaRates(Agent agent, Candidate candidate, double obs_u)
    {
      var select_record = candidate.GetCurrentSelectRecord();
      var current_round = this.CurrentRound;


      foreach (var record in candidate.SortedDataBase)
      {
        var pre_counts = (record.AwaRates.Count == 0) ? 0 : (current_round) * record.AwaRate;

        if (this.IsEvsOpinionFormed(agent, select_record, record, obs_u))
        {
          record.AwaRate = (pre_counts + 1) / (current_round + 1);
          record.AwaCount = (int)pre_counts + 1;
        }
        else
        {
          record.AwaRate = (pre_counts + 0) / (current_round + 1);
          record.AwaCount = (int)pre_counts + 0;
        }
      }

      //foreach (var record in candidate.SortedDataBase)
      //{
      //    if (this.IsEvsOpinionFormed(agent, select_record, record, obs_u))
      //    {
      //        record.AwaCount += 1;
      //    }
      //    record.AwaRate = record.AwaCount / (double)(current_round + 1);
      //}
    }

    protected virtual bool IsEvsOpinionFormed(Agent agent, CandidateRecord select_record, CandidateRecord other_record, double obs_u)
    {
      bool evs1 = this.IsDetermined(agent) && this.IsBiggerWeight(select_record, other_record);
      bool evs2 = this.IsSmallerU(other_record, agent, obs_u) && (other_record.CanWeight != select_record.CanWeight);

      return evs1 || evs2;
    }

    protected virtual bool IsDetermined(Agent agent)
    {
      return agent.IsDetermined();
    }

    protected virtual bool IsBiggerWeight(CandidateRecord select_record, CandidateRecord other_record)
    {
      double other_canwei = other_record.CanWeight;
      double select_canwei = select_record.CanWeight;

      return (other_canwei >= select_canwei) ? true : false;
    }

    protected virtual bool IsSmallerU(CandidateRecord other_record, Agent agent, double obs_u)
    {
      int req_u = other_record.RequireOpinionNum;
      return (obs_u >= req_u) ? true : false;
    }
  }
}
