using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class AATGfix_OSM : AATG_OSM
    {
        int QueueThreshold;
        Dictionary<Agent, int> AgentLifes;
        Dictionary<Agent, int> MaxAgentLifes;

        public AATGfix_OSM()
        {
            this.QueueRange = 5;
            this.QueueThreshold = 3;
            //this.AgentLifeCount = 30;
            this.AgentLifes = new Dictionary<Agent, int>();
            this.MaxAgentLifes = new Dictionary<Agent, int>();
        }

        public override void SetAgentNetwork(AgentNetwork agent_network)
        {
            base.SetAgentNetwork(agent_network);

            this.AgentLifes.Clear();
            this.MaxAgentLifes.Clear();
            foreach (var agent in agent_network.Agents)
            {
                //this.AgentLifes.Add(agent, this.AgentLifeCount);
                this.AgentLifes.Add(agent, 50);
                this.MaxAgentLifes = new Dictionary<Agent, int>(this.AgentLifes);
            }

            //foreach (var candidate in this.Candidates)
            //{
            //    int can_size = candidate.Value.SortedDataBase.Count;
            //    candidate.Value.SelectSortedIndex = can_size - 1;
            //}

            return;
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

            //if (this.MyRecordRounds.Count == 0) return;
            //var cur_record_round = new RecordRound(this.CurrentStep, this.MyAgentNetwork.Agents);
            //var record_steps = new Dictionary<int, RecordStep>();
            //record_steps.Add(0, this.MyRecordStep);
            //cur_record_round.RecordSteps(record_steps);
            ////cur_record_round.RecordSteps(this.MyRecordSteps);
            //var is_recived = cur_record_round.IsReceived(agent);
            var is_received = this.MyRecordStep.IsReceived(agent);
            Console.WriteLine($"Receive Opinion (Received:{is_received})");
            //var receive_op = cur_record_round.AgentReceiveOpinionsInRound[agent];
            var receive_op = this.MyRecordStep.AgentReceiveOpinionsInStep[agent];
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

            Console.Write($"- LifeTime:");
            Console.Write($" {this.AgentLifes[agent]}/{candidate.SortedDataBase.Count}");
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

        protected override void EstimateAwaRate()
        {
        }

        protected override void SelectionWeight()
        {
            foreach (var candidate in this.Candidates)
            {
                var is_received = this.MyRecordStep.IsReceived(candidate.Key);

                //queue
                var agent_queue = this.OpinionChangedQueues[candidate.Value];
                int changed_count = 0;
                int unchanged_count = 0;

                if (agent_queue.Count >= this.QueueRange)
                {
                    agent_queue.Dequeue();
                }
                agent_queue.Enqueue(candidate.Key.IsChanged());

                if (agent_queue.Count < this.QueueRange || !is_received) continue;

                changed_count = agent_queue.Count(q => q == true);
                unchanged_count = agent_queue.Count(q => q == false);


                if (this.AgentLifes[candidate.Key] <= 0) continue;

                //var current_h = candidate.Value.GetCurrentSelectRecord().AwaRate;
                var current_l = candidate.Value.SelectSortedIndex;
                var can_size = candidate.Value.SortedDataBase.Count;


                if (changed_count < this.QueueThreshold && current_l < can_size - 1)
                {
                    candidate.Value.SelectSortedIndex++;
                    this.AgentLifes[candidate.Key]--;
                }
                else if (changed_count > this.QueueThreshold && current_l > 0)
                {
                    candidate.Value.SelectSortedIndex--;
                    this.AgentLifes[candidate.Key]--;
                }

                candidate.Key.SetCommonWeight(candidate.Value.GetSelectCanWeight());

            }
        }
    }
}
