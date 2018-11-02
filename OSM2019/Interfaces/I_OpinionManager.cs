using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Interfaces
{
    interface I_OpinionManager
    {
        void Register(int op_id, I_Opinion op);
        I_Opinion Create(int op_id);
    }
}
