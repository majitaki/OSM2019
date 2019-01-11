using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class CandidateRecord
    {
        public int BeliefDim { get; protected set; }
        public int RequireOpinionNum { get; protected set; }
        public double CanWeight { get; protected set; }

        public int AwaCount { get; set; }
        public double AwaRate { get; set; }

        AggregationFunctions MyAggFuncs;

        public CandidateRecord(int belief_dim, int requre_num, Agent agent)
        {
            this.MyAggFuncs = new AggregationFunctions();
            this.BeliefDim = belief_dim;
            this.RequireOpinionNum = requre_num;
            this.CanWeight = Math.Round(this.CalcCanWeight(belief_dim, requre_num, agent), 4);
        }

        public CandidateRecord(double can_weight, Agent agent)
        {
            this.MyAggFuncs = new AggregationFunctions();
            this.BeliefDim = -1;
            this.RequireOpinionNum = this.CalcRequireNum(can_weight, agent);
            this.CanWeight = Math.Round(can_weight, 4);
        }

        int CalcRequireNum(double can_weight, Agent agent)
        {
            var max_count = agent.GetNeighbors().Count;
            var init_belief = agent.InitBelief;
            Vector<double> receive_op = agent.InitOpinion.Clone();
            Vector<double> belief = agent.InitBelief.Clone();
            receive_op.Clear();
            belief.Clear();

            int require_num = 1;
            for (; belief[0] < agent.OpinionThreshold && require_num < max_count; require_num++)
            {
                receive_op[0] = require_num;
                belief = this.MyAggFuncs.UpdateBelief(init_belief, can_weight, receive_op);
            }

            return require_num;
        }

        double CalcCanWeight(int belief_dim, int requre_num, Agent agent)
        {
            var diff = 0.01;
            var init_can_weight = (1.0 / agent.InitBelief.Count) + diff;
            init_can_weight = Math.Round(init_can_weight, 4);
            var init_belief = agent.InitBelief;
            Vector<double> receive_op = agent.InitOpinion.Clone();
            receive_op.Clear();
            receive_op[belief_dim] = requre_num;

            double can_weight = 0.00;
            for (can_weight = init_can_weight; can_weight < 1.0; can_weight += diff)
            {
                var belief = this.MyAggFuncs.UpdateBelief(init_belief, can_weight, receive_op);
                if (belief[belief_dim] >= agent.OpinionThreshold) break;
            }

            if (can_weight >= 1)
            {
                throw new Exception("error over can weight");
            }

            can_weight = MyMath.Ceiling(can_weight, 4);

            return can_weight;
        }

    }
}
