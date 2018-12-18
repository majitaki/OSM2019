﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class AATG_OSM : AAT_OSM
    {
        protected Dictionary<Candidate, Queue<bool>> OpinionChangedQueues;
        protected int QueueRange;

        public AATG_OSM() : base()
        {
            this.QueueRange = 2;
            this.SetTargetH(1.0);
        }

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
            cur_record_round.RecordSteps(this.MyRecordSteps);
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
            //if (is_recived) receive_rounds++;

            var candidate = this.Candidates[agent];
            var change_queue = this.OpinionChangedQueues[candidate];
            Console.Write($"- Queue:");
            foreach (var value in change_queue)
            {
                Console.Write($" {value}");
            }
            Console.WriteLine();

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

        public override void SetAgentNetwork(AgentNetwork agent_network)
        {
            base.SetAgentNetwork(agent_network);
            this.OpinionChangedQueues = new Dictionary<Candidate, Queue<bool>>();

            foreach (var candidate in this.Candidates)
            {
                var queue = new Queue<bool>();
                this.OpinionChangedQueues.Add(candidate.Value, queue);
            }

            return;
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

        protected override void SelectionWeight()
        {
            foreach (var candidate in this.Candidates)
            {
                //queue
                var agent_queue = this.OpinionChangedQueues[candidate.Value];
                if (agent_queue.Count >= this.QueueRange)
                {
                    agent_queue.Dequeue();
                }
                agent_queue.Enqueue(candidate.Key.IsChanged());

                var changed_count = agent_queue.Count(q => q == true);
                var unchanged_count = agent_queue.Count(q => q == false);

                var current_h = candidate.Value.GetCurrentSelectRecord().AwaRate;
                var current_l = candidate.Value.SelectSortedIndex;
                var can_size = candidate.Value.SortedDataBase.Count;

                if (current_l < can_size - 1 && current_h < this.TargetH)
                {
                    if (unchanged_count >= changed_count)
                    {
                        candidate.Value.SelectSortedIndex++;
                    }
                }
                else if (current_l > 0)
                {
                    var pre_h = candidate.Value.GetSortedRecord(current_l - 1).AwaRate;
                    if (pre_h >= (this.TargetH + this.Epsilon))
                    {
                        if (changed_count > unchanged_count)
                        {
                            candidate.Value.SelectSortedIndex--;
                        }
                    }
                }

                candidate.Key.SetCommonWeight(candidate.Value.GetSelectCanWeight());

            }
        }
    }
}