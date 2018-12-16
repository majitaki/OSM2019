using CsvHelper;
using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class RecordRound
    {
        public int Round { get; private set; }
        public int NetworkSize { get; private set; }
        public Dictionary<Agent, Vector<double>> AgentReceiveOpinionsInRound { get; private set; }
        //public Dictionary<OpinionSubject, List<int>> OpinionSizes { get; private set; }
        public int CorrectSize { get; private set; }
        public int IncorrectSize { get; private set; }
        public int UndeterSize { get; private set; }
        public int FinalStep { get; private set; }
        public int SensorSize { get; private set; }
        public int DeterminedSensorSize { get; private set; }

        public RecordRound(int cur_round, List<Agent> agents)
        {
            this.Round = cur_round;
            this.AgentReceiveOpinionsInRound = new Dictionary<Agent, Vector<double>>();
            //this.OpinionSizes = new Dictionary<OpinionSubject, List<int>>();

            foreach (var agent in agents)
            {
                var undeter_op = agent.InitOpinion.Clone();
                undeter_op.Clear();
                this.AgentReceiveOpinionsInRound.Add(agent, undeter_op);
            }
        }

        public void RecordSteps(Dictionary<int, RecordStep> record_steps)
        {
            this.CorrectSize = record_steps.Last().Value.CorrectSize;
            this.IncorrectSize = record_steps.Last().Value.IncorrectSize;
            this.UndeterSize = record_steps.Last().Value.UndeterSize;

            //this.OpinionSizes = record_steps.Last().Value.OpinionSizes;
            this.FinalStep = record_steps.Last().Value.Step;
            this.NetworkSize = record_steps.Last().Value.NetworkSize;
            this.SensorSize = record_steps.Last().Value.SensorSize;
            this.DeterminedSensorSize = record_steps.Last().Value.DeterminedSensorSize;

            foreach (var record_step in record_steps)
            {
                foreach (var key_value in record_step.Value.AgentReceiveOpinionsInStep)
                {
                    var agent = key_value.Key;
                    var rec_op = key_value.Value;
                    this.AgentReceiveOpinionsInRound[agent] += rec_op;
                }
            }
        }

        public bool IsReceived(Agent agent)
        {
            if (this.AgentReceiveOpinionsInRound[agent].L2Norm() == 0) return false;
            return true;
        }
    }
}
