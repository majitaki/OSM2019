using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class AATfunction_OSM : OSMBase
    {
        protected double TargetH;
        protected double Epsilon;
        protected Dictionary<Agent, AATfunction_Candidate> Candidates;
        public int AwaRateWindowSize { get; protected set; }

        public AATfunction_OSM() : base()
        {
            this.Epsilon = 0.01;
            this.AwaRateWindowSize = 1;
        }
        public void SetAwaRateWindowSize(int size)
        {
            this.AwaRateWindowSize = size;
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
        }


        protected virtual void SetCandidate()
        {
            this.Candidates = new Dictionary<Agent, AATfunction_Candidate>();
            foreach (var agent in this.MyAgentNetwork.Agents)
            {
                var can = new AATfunction_Candidate(agent, this.AwaRateWindowSize);
                this.Candidates.Add(agent, can);
                var initial_h = 0.0;
                agent.SetCommonWeight(can.EstimateWeight(initial_h));
                can.CanWeight = can.EstimateWeight(initial_h);
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
                var est_weight = candidate.Value.EstimateWeight(this.CurrentRound);
                var current_weight = candidate.Value.CanWeight;
                candidate.Value.SetTranslation(current_weight - est_weight);
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

        protected virtual void UpdateAveAwaRates(Agent agent, AATfunction_Candidate candidate)
        {
            if (this.IsDetermined(agent))
            {
                candidate.AwaCount = candidate.AwaCount + 1;
            }
            else
            {
                candidate.AwaCount = candidate.AwaCount + 0;
            }
        }

        protected virtual bool IsDetermined(Agent agent)
        {
            return agent.IsDetermined();
        }

    }
}
