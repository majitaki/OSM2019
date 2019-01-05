using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class AATfix_OSM : AAT_OSM
    {

        public override void PrintAgentInfo(Agent agent)
        {
            base.PrintAgentInfo(agent);
        }

        protected override void EstimateAwaRate()
        {
            foreach (var candidate in this.Candidates)
            {
                var received_sum_op = this.MyRecordRounds.Last().AgentReceiveOpinionsInRound[candidate.Key];
                double obs_u = this.GetObsU(received_sum_op);
                if (!(this.MyRecordRounds.Last().IsReceived(candidate.Key))) continue;
                this.UpdateAveAwaRates(candidate.Key, candidate.Value, obs_u);
            }
        }

        protected override void UpdateAveAwaRates(Agent agent, Candidate candidate, double obs_u)
        {
            var select_record = candidate.GetCurrentSelectRecord();
            var current_round = this.CurrentRound;
            var min_diff_u_record = candidate.SortedDataBase.OrderBy(record => Math.Abs(record.RequireOpinionNum - obs_u)).First();
            var min_diff_u = Math.Abs(min_diff_u_record.RequireOpinionNum - obs_u);
            var obs_weight = candidate.SortedDataBase.Where(record => Math.Abs(record.RequireOpinionNum - obs_u) == min_diff_u).OrderBy(record => record.CanWeight).First().CanWeight;
            var receive_rounds = this.MyRecordRounds.Where(record_round => record_round.IsReceived(agent)).Count();

            foreach (var record in candidate.SortedDataBase)
            {
                if (this.IsEvsOpinionFormed(agent, select_record, record, obs_weight))
                {
                    record.AwaCount += 1;
                }
                //record.AwaRate = record.AwaCount / (double)(current_round + 1);
                record.AwaRate = record.AwaCount / (double)(receive_rounds);
            }
        }

        protected override bool IsEvsOpinionFormed(Agent agent, CandidateRecord select_record, CandidateRecord other_record, double obs_weight)
        {
            bool evs1 = this.IsChanged(agent) && this.IsBiggerWeight(select_record, other_record);
            //bool evs2 = this.IsBiggerWeightThanObs(obs_weight, other_record) && (other_record.CanWeight != select_record.CanWeight);
            bool evs2 = this.IsBiggerWeightThanObs(obs_weight, other_record);

            return evs1 || evs2;
        }

        protected virtual bool IsChanged(Agent agent)
        {
            return agent.IsChanged();
        }

        protected virtual bool IsBiggerWeightThanObs(double obs_weight, CandidateRecord other_record)
        {
            var other_weight = other_record.CanWeight;
            return (obs_weight <= other_weight) ? true : false;
        }
    }
}
