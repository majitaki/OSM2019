using OSM2019.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class BasicOpinionManager : I_OpinionManager
    {
        Dictionary<int, I_Opinion> OpinionDictionary;

        public BasicOpinionManager()
        {
            this.OpinionDictionary = new Dictionary<int, I_Opinion>();
        }

        public I_Opinion Create(int op_id)
        {
            I_Opinion op = this.OpinionDictionary[op_id];
            return op.CreateClone();
        }

        public void Register(int op_id, I_Opinion op)
        {
            this.OpinionDictionary.Add(op_id, op);
        }
    }
}
