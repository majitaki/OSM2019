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
    class AggregationFunctions
    {

        private Vector<double> GetLikelihoodsForParticleFilter(double weight, int dim, int sample_index, Vector<double> receive_opinions)
        {
            var exist_count = receive_opinions.Count(n => n != 0);
            var turara = MyMath.MakeTurara(exist_count, weight);
            var max = turara.max;
            var other = turara.other;

            var likelihoods = Vector<double>.Build.DenseOfVector(receive_opinions);
            for (int vector_index = 0; vector_index < likelihoods.Count; vector_index++)
            {
                if (vector_index == sample_index)
                {
                    likelihoods[vector_index] = max;
                }
                else
                {
                    if (likelihoods[vector_index] != 0)
                    {
                        likelihoods[vector_index] = other;
                    }
                }
            }
            return likelihoods;
        }

        private Vector<double> GetPostBeliefsByParticleFilter(Vector<double> prior_beliefs, Vector<double> receive_opinions, double weight)
        {
            int dim = prior_beliefs.Count;
            var sample_index = receive_opinions.MaximumIndex();

            var likelihoods = this.GetLikelihoodsForParticleFilter(weight, dim, sample_index, receive_opinions);
            //var weight_dist = likelihoods.PointwiseMultiply(receive_opinions);
            //var post_beliefs = weight_dist.PointwiseMultiply(prior_beliefs);
            var post_beliefs = likelihoods.PointwiseMultiply(prior_beliefs);

            if (post_beliefs.Sum() == 0)
            {
                post_beliefs.Clear();
                return post_beliefs + (1.0 / dim);
            }
            return post_beliefs.Normalize(1.0);
        }

        private Vector<double> GetLikelihoodsForBayseFilter(double weight, int dim, int sample_index)
        {
            var turara = MyMath.MakeTurara(dim, weight);
            var max = turara.max;
            var other = turara.other;

            var likelihoods = Vector<double>.Build.Dense(dim, other);
            likelihoods[sample_index] = max;
            return likelihoods;
        }


        private Vector<double> GetPostBeliefsByBayseFilter(Vector<double> prior_beliefs, Vector<double> receive_opinions, double weight)
        {
            Debug.Assert(!Double.IsNaN(weight));

            int dim = prior_beliefs.Count;

            var likelihoods = Vector<double>.Build.Dense(dim, 0);
            var post_beliefs = prior_beliefs;

            foreach (var sample_index in Enumerable.Range(0, receive_opinions.Count))
            {
                foreach (var i in Enumerable.Range(0, (int)receive_opinions[sample_index]))
                {
                    likelihoods = this.GetLikelihoodsForBayseFilter(weight, dim, sample_index);
                    post_beliefs = post_beliefs.PointwiseMultiply(likelihoods);
                }
            }
            //Debug.Assert(receive_opinion[sample_index] == 1);

            if (post_beliefs.Sum() == 0)
            {
                post_beliefs.Clear();
                return post_beliefs + (1.0 / dim);
            }
            return post_beliefs.Normalize(1.0);
        }

        public Vector<double> UpdateBelief(Vector<double> belief, double weight, Vector<double> receive_opinions, BeliefUpdateFunctionEnum func_mode)
        {
            if (Double.IsNaN(weight))
            {
                Console.WriteLine();
            }

            switch (func_mode)
            {
                case BeliefUpdateFunctionEnum.Bayse:
                    return this.GetPostBeliefsByBayseFilter(belief, receive_opinions, weight);
                case BeliefUpdateFunctionEnum.Particle:
                    return this.GetPostBeliefsByParticleFilter(belief, receive_opinions, weight);
            }
            Debug.Assert(false);
            return null;
        }

        public double CalcSingleBelief(Vector<double> pre_beliefs, int belief_dim, int op_dim, double weight, double op_dust = 0.0)
        {
            var upper = pre_beliefs[belief_dim] * this.ConvertWeight(weight, belief_dim, op_dim, pre_beliefs.Count, op_dust);

            var lower = 0.001;
            foreach (var lower_belief_dim in Enumerable.Range(0, pre_beliefs.Count))
            {
                var pre_belief = pre_beliefs[lower_belief_dim];
                lower += pre_belief * this.ConvertWeight(weight, lower_belief_dim, op_dim, pre_beliefs.Count, op_dust);
            }

            var pos_belief = upper / lower;

            return Math.Round(pos_belief, 4);
        }

        public double ConvertWeight(double weight, int belief_dim, int op_dim, int dim_size, double op_dust)
        {
            if (op_dust != 0.0)
            {
                weight = (weight - 1 / dim_size) * op_dust + 1 / dim_size;
            }

            if (belief_dim == op_dim)
            {
                return weight;
            }
            else
            {
                return (1 - weight) / (dim_size - 1);
            }
        }
    }
}
