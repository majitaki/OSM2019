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
            Console.WriteLine($"Agent ID: {agent.AgentID}");
            Console.WriteLine($"Sensor: {agent.IsSensor}");
            Console.WriteLine($"Belief");
            int dim = 0;
            foreach (var belief in agent.Belief.ToList())
            {
                Console.WriteLine($"- Dim: {dim} Value {belief}");
                dim++;
            }

            var is_changed = agent.IsChanged();
            Console.WriteLine($"Opinion (Changed:{is_changed})");
            dim = 0;
            foreach (var op in agent.Opinion.ToList())
            {
                Console.WriteLine($"- Dim: {dim} Value {op}");
                dim++;
            }

            if (this.MyRecordRounds.Count == 0) return;
            var cur_record_round = new RecordRound(this.CurrentStep, this.MyAgentNetwork.Agents);
            var record_steps = new Dictionary<int, RecordStep>();
            record_steps.Add(0, this.MyRecordStep);
            cur_record_round.RecordSteps(record_steps);
            //cur_record_round.RecordSteps(this.MyRecordSteps);
            var is_recived = cur_record_round.IsReceived(agent);
            Console.WriteLine($"Receive Opinion (Received:{is_recived})");
            var receive_op = cur_record_round.AgentReceiveOpinionsInRound[agent];
            dim = 0;
            foreach (var op in receive_op.ToList())
            {
                Console.WriteLine($"- Dim: {dim} Value {op}");
                dim++;
            }

            var receive_rounds = this.MyRecordRounds.Where(record_round => record_round.Value.IsReceived(agent)).Count();

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

        protected override void EstimateAwaRate()
        {
            foreach (var candidate in this.Candidates)
            {
                var received_sum_op = this.MyRecordRounds.Last().Value.AgentReceiveOpinionsInRound[candidate.Key];
                double obs_u = this.GetObsU(received_sum_op);
                if (!(this.MyRecordRounds.Last().Value.IsReceived(candidate.Key))) continue;
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
            var receive_rounds = this.MyRecordRounds.Where(record_round => record_round.Value.IsReceived(agent)).Count();

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
