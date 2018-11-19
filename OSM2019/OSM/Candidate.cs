﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class Candidate
    {
        public List<CandidateRecord> DataBase { get; protected set; }

        public Candidate(Agent agent)
        {
            this.DataBase = new List<CandidateRecord>();
            if (agent.GetNeighbors().Count == 0) return;

            int max_require_num = agent.GetNeighbors().Count;
            int dim_size = agent.Belief.RowCount;

            for (int dim = 0; dim < dim_size; dim++)
            {
                for (int req_num = 1; req_num <= max_require_num; req_num++)
                {
                    this.DataBase.Add(new CandidateRecord(dim, req_num, agent));
                }
            }

        }
    }
}
