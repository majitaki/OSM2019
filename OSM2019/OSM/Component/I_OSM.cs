using MathNet.Numerics.LinearAlgebra;
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
        AgentNetwork MyAgentNetwork { get; set; }
        Dictionary<Agent, Matrix<double>> AgentReceiveOpinionsByStep { get; set; }
        Dictionary<Agent, Matrix<double>> AgentReceiveOpinionsByRound { get; set; }

        void UpdateSteps(int steps);
        void InitializeToZeroStep();
        void InitialReceiveOpinionsByRound();
        void InitialReceiveOpinionsByStep();
        void IntegrateReceiveOpinion();
        void PrintRound();
        void PrintStep();
        void UpdateRoundWithoutSteps();
        void UpdateRounds(int rounds, int steps);
        void InitializeToZeroRound();
        void PrintAgentInfo(Agent agent);
    }
}
