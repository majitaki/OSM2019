using MathNet.Numerics.LinearAlgebra;
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
        //Dictionary<int, RecordRound> MyRecordRounds { get; set; }
        //RecordStep MyRecordStep { get; set; }
        RecordRound MyRecordRound { get; set; }
        List<RecordRound> MyRecordRounds { get; set; }
        AgentNetwork MyAgentNetwork { get; set; }

        //step
        void InitializeToFirstStep();
        void InitializeStep();
        void NextStep();
        void RecordStep(bool final);
        void FinalizeStep();
        void UpdateSteps(int step_count);
        void PrintStepInfo();

        //round
        void InitializeToFirstRound();
        void InitializeRound();
        void NextRound(int step_count);
        void RecordRound();
        void FinalizeRound();
        void UpdateRounds(int round_count, int step_count, ExtendProgressBar pb = null);
        void PrintRoundInfo();

        //agent
        void PrintAgentInfo(Agent agent);

        //common

    }
}
