using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class AATparticle_Candidate : Candidate
    {
        public AATparticle_Candidate(Agent agent, int awa_window_size = 1)
        {
            this.DataBase = new List<CandidateRecord>();
            var min_weight = (1.0 / agent.MySubject.SubjectDimSize) + 0.01;

            for (double weight = min_weight; weight < 1.0; weight += 0.02)
            {
                this.DataBase.Add(new CandidateRecord(weight, agent));
            }

            this.SortedDataBase = this.DataBase.OrderBy(record => record.CanWeight).ToList();
            var max_index = this.SortedDataBase.Count - 1;
            this.SelectSortedIndex = max_index;
            //this.SelectSortedIndex = 0;
        }
    }
}
