using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class AATfix_Candidate : Candidate
    {
        public AATfix_Candidate(Agent agent)
        {
            this.DataBase = new List<CandidateRecord>();
            //var min_weight = (1.0 / agent.MySubject.SubjectDimSize) + 0.001;
            var min_weight = 0.0;

            for (double weight = min_weight; weight < 1.0; weight += 0.05)
            {
                this.DataBase.Add(new CandidateRecord(weight, agent));
            }

            this.SortedDataBase = this.DataBase.OrderBy(record => record.CanWeight).ToList();
            var max_index = this.SortedDataBase.Count - 1;
            this.SelectSortedIndex = max_index;
            //this.SelectSortedIndex = 0;
        }

        public AATfix_Candidate(Agent agent, int awa_window_size = 1)
        {
            this.DataBase = new List<CandidateRecord>();
            if (agent.GetNeighbors().Count == 0) return;

            int max_require_num = agent.GetNeighbors().Count;
            int dim_size = agent.Belief.Count;

            for (int dim = 0; dim < dim_size; dim++)
            {
                for (int req_num = 1; req_num <= max_require_num; req_num++)
                {
                    this.DataBase.Add(new CandidateRecord(dim, req_num, agent, awa_window_size));
                }
            }

            this.SortedDataBase = this.DataBase.OrderBy(record => record.CanWeight).ToList();
            var max_index = this.SortedDataBase.Count - 1;
            this.SelectSortedIndex = max_index;
        }
    }
}
