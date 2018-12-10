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
            this.CanWeight = this.GetCanWeight(belief_dim, requre_num, agent);
        }

        double GetCanWeight(int belief_dim, int requre_num, Agent agent)
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
