using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class AATGfix_OSM : AATG_OSM
    {
        public AATGfix_OSM()
        {
            this.QueueRange = 4;
        }
        protected override void EstimateAwaRate()
        {

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

                //var current_h = candidate.Value.GetCurrentSelectRecord().AwaRate;
                var current_l = candidate.Value.SelectSortedIndex;
                var can_size = candidate.Value.SortedDataBase.Count;

                if (this.UpdateStepRand.NextDouble() < 0.1)
                {
                    if (unchanged_count > changed_count && current_l < can_size - 1)
                    {
                        candidate.Value.SelectSortedIndex++;
                    }
                    else if (unchanged_count < changed_count && current_l > 0)
                    {
                        candidate.Value.SelectSortedIndex--;
                    }
                    else if (unchanged_count == changed_count)
                    {

                    }
                }



                //if (current_l < can_size - 1)
                //{
                //    if (unchanged_count >= changed_count)
                //    {
                //        candidate.Value.SelectSortedIndex++;
                //    }
                //    else
                //    {
                //        candidate.Value.SelectSortedIndex--;
                //    }
                //}
                //else if (current_l > 0)
                //{
                //    if (unchanged_count >= changed_count)
                //    {
                //        //candidate.Value.SelectSortedIndex++;
                //    }
                //    else
                //    {
                //        candidate.Value.SelectSortedIndex--;
                //    }
                //}

                candidate.Key.SetCommonWeight(candidate.Value.GetSelectCanWeight());

            }
        }
    }
}
