﻿using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    interface I_OSM
    {
        int CurrentStep { get; set; }
        int CurrentRound { get; set; }
        //Dictionary<int, RecordStep> MyRecordSteps { get; set; }
        Dictionary<int, RecordRound> MyRecordRounds { get; set; }
        RecordStep MyRecordStep { get; set; }

        AgentNetwork MyAgentNetwork { get; set; }

        void UpdateSteps(int steps);
        void InitializeToZeroStep();
        void PrintRound();
        void PrintStep();
        void UpdateRecordRound();
        void UpdateRoundWithoutSteps();
        void UpdateRounds(int rounds, int steps, ExtendProgressBar pb);
        void InitializeToZeroRound();
        void PrintAgentInfo(Agent agent);
    }
}
