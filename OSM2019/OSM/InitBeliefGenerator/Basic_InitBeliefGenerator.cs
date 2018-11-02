using OSM2019.Interfaces;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class Basic_InitBeliefGenerator : I_InitBeliefGenerator
    {
        InitBeliefMode InitBeliefMode;

        public Basic_InitBeliefGenerator(InitBeliefMode init_mode)
        {
            this.InitBeliefMode = init_mode;
        }

        public Dictionary<int, double> Generate(int opinion_size, ExtendRandom ex_rand)
        {
            var init_belief_dic = new Dictionary<int, double>();
            double remain_belief = 1.0;
            double bound_rate = 0.5;
            var init_belief_list = new List<double>();
            double last_belief = 0.0;

            int narrow_div_num = 30;
            int normal_div_num = 5;
            int wide_div_num = 1;
            var mu = remain_belief / opinion_size;
            double stddev;

            switch (this.InitBeliefMode)
            {
                case InitBeliefMode.NormalNarrowRandom:
                    stddev = mu / narrow_div_num;
                    init_belief_list = ex_rand.NextNormals(mu, stddev, opinion_size - 1, bound_rate);
                    last_belief = remain_belief - init_belief_list.Sum();
                    init_belief_list.Add(last_belief);
                    break;
                case InitBeliefMode.NormalRandom:
                    stddev = mu / normal_div_num;
                    init_belief_list = ex_rand.NextNormals(mu, stddev, opinion_size - 1, bound_rate);
                    last_belief = remain_belief - init_belief_list.Sum();
                    init_belief_list.Add(last_belief);
                    break;
                case InitBeliefMode.NormalWideRandom:
                    stddev = mu / wide_div_num;
                    init_belief_list = ex_rand.NextNormals(mu, stddev, opinion_size - 1, bound_rate);
                    last_belief = remain_belief - init_belief_list.Sum();
                    init_belief_list.Add(last_belief);
                    break;
                case InitBeliefMode.NoRandom:
                    var one_init_belief = remain_belief / opinion_size;
                    init_belief_list = Enumerable.Repeat(one_init_belief, opinion_size).ToList();
                    break;
                default:
                    break;
            }

            int op_key = 1;
            foreach (var init_belief in init_belief_list)
            {
                init_belief_dic.Add(op_key, init_belief);
                op_key++;
            }


            return init_belief_dic;
        }
    }
}
