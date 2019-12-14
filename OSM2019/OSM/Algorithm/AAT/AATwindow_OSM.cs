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
  class AATwindow_OSM : AAT_OSM
  {
    public AATwindow_OSM() : base()
    {
      this.AwaRateWindowSize = 10;
    }
    public void SetAwaRateWindowSize(int size)
    {
      this.AwaRateWindowSize = size;
    }

    public override void PrintAgentInfo(Agent agent)
    {
      //base.PrintAgentInfo(agent);

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

        var current_count = (record.AwaRates.Count == 0) ? 0 : this.CurrentRound * record.AwaRates.Last();
        var prepre_count = (record.AwaRates.Count == 0) ? 0 : (this.CurrentRound - record.AwaRates.Count) * record.AwaRates.Peek();
        var window_h = (record.AwaRates.Count == 0) ? 0 : (current_count - prepre_count) / record.AwaRates.Count;

        var awa_count = record.AwaCount;
        var h = record.AwaRate;
        Console.WriteLine($"{select} index: {can_index,3} req: {req_num,3} can_weight: {can_weight:f3} awa_count: {awa_count,3} h_rcv_round: {receive_rounds,3} h: {h:f4} wh: {window_h:f4} index: {can_index,3} {select}");
        can_index++;
      }
    }

    protected override void SelectionWeight()
    {
      foreach (var candidate in this.Candidates)
      {
        var received_sum_op = this.MyRecordRounds.Last().AgentReceiveOpinionsInRound[candidate.Key];
        double obs_u = this.GetObsU(received_sum_op);
        if (obs_u == 0) continue;

        var record = candidate.Value.GetCurrentSelectRecord();
        var current_count = (record.AwaRates.Count == 0) ? 0 : this.CurrentRound * record.AwaRates.Last();
        var prepre_count = (record.AwaRates.Count == 0) ? 0 : (this.CurrentRound - record.AwaRates.Count) * record.AwaRates.Peek();

        var window_h = (record.AwaRates.Count == 0) ? 0 : (current_count - prepre_count) / record.AwaRates.Count;

        //var current_h = candidate.Value.GetCurrentSelectRecord().AwaRate;
        var current_l = candidate.Value.SelectSortedIndex;
        var can_size = candidate.Value.SortedDataBase.Count;

        if (current_l < can_size - 1 && window_h < this.TargetH)
        {
          candidate.Value.SelectSortedIndex++;
        }
        else if (current_l > 0)
        {
          var under_record = candidate.Value.GetSortedRecord(current_l - 1);
          var under_current_count = (under_record.AwaRates.Count == 0) ? 0 : this.CurrentRound * under_record.AwaRates.Last();
          var under_prepre_count = (under_record.AwaRates.Count == 0) ? 0 : (this.CurrentRound - under_record.AwaRates.Count) * under_record.AwaRates.Peek();
          var under_window_h = (under_record.AwaRates.Count == 0) ? 0 : (under_current_count - under_prepre_count) / under_record.AwaRates.Count;

          //var under_h = candidate.Value.GetSortedRecord(current_l - 1).AwaRate;
          if (under_window_h >= (this.TargetH + this.Epsilon))
          {
            candidate.Value.SelectSortedIndex--;
          }
        }

        candidate.Key.SetCommonWeight(candidate.Value.GetSelectCanWeight());
      }
    }
  }
}
