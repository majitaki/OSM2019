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
        public int CorrectSize { get; private set; }
        public int IncorrectSize { get; private set; }
        public int UndeterSize { get; private set; }
        public int StepMessageSize { get; private set; }
        public int ActiveAgentSize { get; private set; }
        public int ActiveSensorSize { get; private set; }
        public Dictionary<Agent, Vector<double>> AgentReceiveOpinionsInStep { get; private set; }
        public int NetworkSize { get; private set; }

        public RecordStep(int cur_step, List<Agent> agents)
        {
            this.Step = cur_step;
            this.AgentReceiveOpinionsInStep = new Dictionary<Agent, Vector<double>>();
            this.NetworkSize = agents.Count;

            foreach (var agent in agents)
            {
                var undeter_op = agent.InitOpinion.Clone();
                undeter_op.Clear();
                this.AgentReceiveOpinionsInStep.Add(agent, undeter_op);
            }

        }

        public void RecordStepAgents(List<Agent> agents, EnvironmentManager env_mgr)
        {
            var cor_dim = env_mgr.CorrectDim;
            var cor_subject = env_mgr.EnvSubject;
            var same_subject_agents = agents.Where(agent => agent.MySubject.SubjectName == cor_subject.SubjectName).ToList();
            //var cor_agents = same_subject_agents.Where(agent => agent.GetOpinionDim() == cor_dim).ToList();
            //var undeter_agents = same_subject_agents.Where(agent => agent.GetOpinionDim() == -1).ToList();
            //var incor_agents = same_subject_agents.Except(cor_agents).Except(undeter_agents).ToList();
            //var network_size = agents.Count;

            this.NetworkSize = agents.Count;
            this.CorrectSize = same_subject_agents.Where(agent => agent.GetOpinionDim() == cor_dim).Count();
            this.IncorrectSize = same_subject_agents.Where(agent => agent.GetOpinionDim() == -1).Count();
            this.UndeterSize = this.NetworkSize - this.CorrectSize - this.IncorrectSize;

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

    }
}
