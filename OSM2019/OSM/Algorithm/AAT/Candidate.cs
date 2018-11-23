using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class Candidate
    {
        //public List<CandidateRecord> DataBase { get; protected set; }
        public List<CandidateRecord> SortedDataBase { get; protected set; }
        public int SelectSortedIndex;

        public Candidate(Agent agent)
        {
            this.SortedDataBase = new List<CandidateRecord>();
            if (agent.GetNeighbors().Count == 0) return;

            int max_require_num = agent.GetNeighbors().Count;
            int dim_size = agent.Belief.RowCount;

            for (int dim = 0; dim < dim_size; dim++)
            {
                for (int req_num = 1; req_num <= max_require_num; req_num++)
                {
                    this.SortedDataBase.Add(new CandidateRecord(dim, req_num, agent));
                }
            }

            this.SortedDataBase = this.SortedDataBase.OrderBy(record => record.CanWeight).ToList();
            this.SelectSortedIndex = 0;
        }

        public CandidateRecord GetSortedRecord(int index)
        {
            return this.SortedDataBase[index];
        }

        public CandidateRecord GetCurrentSelectRecord()
        {
            return this.SortedDataBase[this.SelectSortedIndex];
        }

        public double GetSelectCanWeight()
        {
            return this.GetCurrentSelectRecord().CanWeight;
        }
    }
}
