using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    interface I_OSM
    {
        void UpdateSteps(int steps);
        void InitializeStep();
        void InitializeToZeroStep();
        void RecordRound();
        void UpdateRound(int steps);
        void UpdateRounds(int rounds, int steps);
        void InitializeRound();
        void InitializeToZeroRound();
    }
}
