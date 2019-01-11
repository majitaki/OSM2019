﻿using System;
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
            var min_weight = (1.0 / agent.MySubject.SubjectDimSize) + 0.001;

            for (double weight = min_weight; weight < 1.0; weight += 0.02)
            {
                this.DataBase.Add(new CandidateRecord(weight, agent));
            }

            this.SortedDataBase = this.DataBase.OrderBy(record => record.CanWeight).ToList();
            var max_index = this.SortedDataBase.Count - 1;
            this.SelectSortedIndex = max_index;
        }
    }
}