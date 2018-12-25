using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class RecordStep
    {
        public int Step { get; private set; }
        //public Dictionary<OpinionSubject, List<int>> OpinionSizes { get; private set; }
        public int CorrectSize { get; private set; }
        public int IncorrectSize { get; private set; }
        public int UndeterSize { get; private set; }
        public int StepMessageSize { get; private set; }
        public int ActiveAgentSize { get; private set; }
        public int ActiveSensorSize { get; private set; }
        public int SensorSize { get; private set; }
        public int DeterminedSensorSize { get; private set; }
        public Dictionary<Agent, Vector<double>> AgentReceiveOpinionsInStep { get; private set; }
        public int NetworkSize { get; private set; }

        public RecordStep(int cur_step, List<Agent> agents)
        {
            this.Step = cur_step;
            this.AgentReceiveOpinionsInStep = new Dictionary<Agent, Vector<double>>();
            //this.OpinionSizes = new Dictionary<OpinionSubject, List<int>>();
            this.NetworkSize = agents.Count;

            //foreach (var subject in subject_mgr.Subjects)
            //{
            //    this.OpinionSizes[subject] = new List<int>(subject.SubjectDimSize);
            //}

            foreach (var agent in agents)
            {
                var undeter_op = agent.InitOpinion.Clone();
                undeter_op.Clear();
                this.AgentReceiveOpinionsInStep.Add(agent, undeter_op);
            }

        }

        public void RecordStepAgents(List<Agent> agents, SubjectManager subject_mgr)
        {
            //foreach (var agent in agents)
            //{
            //    this.OpinionSizes[agent.MySubject][agent.GetOpinionDim()]++;
            //}
            var cor_dim = subject_mgr.OSM_Env.CorrectDim;
            var cor_subject = subject_mgr.OSM_Env.EnvSubject;
            this.CorrectSize = agents.Where(agent => agent.MySubject.SubjectName == cor_subject.SubjectName && agent.GetOpinionDim() == cor_dim).Count();
            this.UndeterSize = agents.Where(agent => agent.GetOpinionDim() == -1).Count();
            this.IncorrectSize = this.NetworkSize - this.CorrectSize - this.UndeterSize;
            this.NetworkSize = agents.Count;
            this.SensorSize = agents.Where(agent => agent.IsSensor).Count();
            this.DeterminedSensorSize = agents.Where(agent => agent.IsSensor && agent.IsDetermined()).Count();

            //var same_subject_agents = agents.Where(agent => agent.MySubject.SubjectName == cor_subject.SubjectName).ToList();
            //this.CorrectSize = same_subject_agents.Where(agent => agent.GetOpinionDim() == cor_dim).Count();
            //this.UndeterSize = same_subject_agents.Where(agent => agent.GetOpinionDim() == -1).Count();
            //this.IncorrectSize = this.NetworkSize - this.CorrectSize - this.UndeterSize;

        }

        public void RecordStepMessages(List<Message> step_messages)
        {
            foreach (var step_message in step_messages)
            {
                Vector<double> receive_op = null;
                if (step_message.Subject != step_message.ToAgent.MySubject)
                {
                    var to_subject = step_message.ToAgent.MySubject;
                    receive_op = step_message.Subject.ConvertOpinionForSubject(step_message.Opinion, to_subject);
                }
                else
                {
                    receive_op = step_message.Opinion.Clone();
                }

                this.AgentReceiveOpinionsInStep[step_message.ToAgent] += receive_op;
            }

            this.ActiveSensorSize = step_messages.Where(message => message.FromAgent.AgentID < 0).Select(message => message.ToAgent.AgentID).Count();
            this.ActiveAgentSize = step_messages.Where(message => message.FromAgent.AgentID >= 0).Select(message => message.ToAgent.AgentID).Count();
            this.StepMessageSize = step_messages.Count;
        }

        public bool IsReceived(Agent agent)
        {
            if (this.AgentReceiveOpinionsInStep[agent].L2Norm() == 0) return false;
            return true;
        }

    }
}
