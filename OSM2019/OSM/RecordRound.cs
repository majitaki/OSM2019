using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class RecordRound
    {
        public int Round { get; private set; }
        public Dictionary<int, RecordStep> MyRecordSteps { get; private set; }
        public int NetworkSize { get; private set; }
        public Dictionary<Agent, Matrix<double>> AgentReceiveOpinionsInRound { get; private set; }

        public RecordRound(int cur_round, List<Agent> agents)
        {
            this.Round = cur_round;
            this.MyRecordSteps = new Dictionary<int, RecordStep>();
            this.AgentReceiveOpinionsInRound = new Dictionary<Agent, Matrix<double>>();

            foreach (var agent in agents)
            {
                var undeter_op = agent.InitOpinion.Clone();
                undeter_op.Clear();
                this.AgentReceiveOpinionsInRound.Add(agent, undeter_op);
            }
        }

        public void RecordSteps(Dictionary<int, RecordStep> record_steps)
        {
            this.MyRecordSteps.Union(record_steps);

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

        public List<int> GetCorrectAgentIDs(EnvironmentManager env_mgr)
        {
            return this.MyRecordSteps.Last().Value.CorrectAgentIDs;
        }

        public List<int> GetIncorrectAgentIDs(EnvironmentManager env_mgr)
        {
            return this.MyRecordSteps.Last().Value.IncorrectAgentIDs;
        }

        public List<int> GetUndeterAgentIDs(EnvironmentManager env_mgr)
        {
            return this.MyRecordSteps.Last().Value.UndeterAgentIDs;
        }

        public bool IsReceived(Agent agent)
        {
            if (this.AgentReceiveOpinionsInRound[agent].L2Norm() == 0) return false;
            return true;
        }
    }
}
