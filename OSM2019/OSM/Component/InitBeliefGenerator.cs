using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class InitBeliefGenerator
    {
        InitBeliefMode Mode;
        int NarrowDimSize;
        int NormalDimSize;
        int WideDimSize;

        public InitBeliefGenerator SetInitBeliefMode(InitBeliefMode mode)
        {
            this.Mode = mode;
            this.NarrowDimSize = 30;
            this.NormalDimSize = 5;
            this.WideDimSize = 1;
            return this;
        }

        public Matrix<double> Generate(Matrix<double> init_op, ExtendRandom agent_gene_rand)
        {
            var op_dim_size = init_op.RowCount;
            var init_belief_list = new List<double>();
            var last_belief = 0.0;
            var remain_belief = 1.0;
            var bound_rate = 0.5;
            var mu = remain_belief / op_dim_size;
            double stddev = 0.0;
            Matrix<double> init_belief_matrix = init_op.Clone();

            switch (this.Mode)
            {
                case InitBeliefMode.NormalNarrow:
                    stddev = mu / this.NarrowDimSize;
                    init_belief_list = agent_gene_rand.NextNormals(mu, stddev, op_dim_size - 1, bound_rate);
                    last_belief = remain_belief - init_belief_list.Sum();
                    last_belief = Math.Round(last_belief, 4);
                    init_belief_list.Add(last_belief);
                    break;
                case InitBeliefMode.Normal:
                    stddev = mu / this.NormalDimSize;
                    init_belief_list = agent_gene_rand.NextNormals(mu, stddev, op_dim_size - 1, bound_rate);
                    last_belief = remain_belief - init_belief_list.Sum();
                    last_belief = Math.Round(last_belief, 4);
                    init_belief_list.Add(last_belief);
                    break;
                case InitBeliefMode.NormalWide:
                    stddev = mu / this.WideDimSize;
                    init_belief_list = agent_gene_rand.NextNormals(mu, stddev, op_dim_size - 1, bound_rate);
                    last_belief = remain_belief - init_belief_list.Sum();
                    last_belief = Math.Round(last_belief, 4);
                    init_belief_list.Add(last_belief);
                    break;
                case InitBeliefMode.NoRandom:
                    var one_init_belief = remain_belief / op_dim_size;
                    one_init_belief = Math.Round(one_init_belief, 4);
                    init_belief_list = Enumerable.Repeat(one_init_belief, op_dim_size - 1).ToList();
                    last_belief = remain_belief - init_belief_list.Sum();
                    last_belief = Math.Round(last_belief, 4);
                    init_belief_list.Add(last_belief);
                    break;
                default:
                    break;
            }

            for (int index = 0; index < init_belief_list.Count; index++)
            {
                init_belief_matrix[index, 0] = init_belief_list[index];
            }

            return init_belief_matrix;
        }
    }
}
