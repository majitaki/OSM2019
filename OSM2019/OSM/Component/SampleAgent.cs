using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class SampleAgent : AgentBase<SampleAgent>
    {
        InitBeliefGenerator MyInitBeliefGene;
        InitWeightMode MyInitWeightMode;

        public SampleAgent SetInitBeliefGene(InitBeliefGenerator init_belief_gene)
        {
            this.MyInitBeliefGene = init_belief_gene;
            return this;
        }

        public SampleAgent SetThreshold(double threshold)
        {
            this.Threshold = threshold;
            return this;
        }

        //public SampleAgent SetSubject(string subject)
        //{
        //    this.Subject = subject;
        //    return this;
        //}

        //public SampleAgent SetInitOpinion(Matrix<double> init_op_matrix)
        //{
        //    this.InitOpinionMatrix = init_op_matrix;
        //    return this;
        //}

        public SampleAgent SetInitWeightsMode(InitWeightMode mode)
        {
            this.MyInitWeightMode = mode;
            return this;
        }
    }
}
