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
        public List<int> CorrectAgentIDs { get; private set; }
        public List<int> IncorrectAgentIDs { get; private set; }
        public List<int> UndeterAgentIDs { get; private set; }
        public List<Message> StepMessages { get; private set; }
        public Dictionary<Agent, Matrix<double>> AgentReceiveOpinionsInStep { get; private set; }
        public int NetworkSize { get; private set; }

        public RecordStep(int cur_step, List<Agent> agents)
        {
            this.Step = cur_step;
            this.CorrectAgentIDs = new List<int>();
            this.IncorrectAgentIDs = new List<int>();
            this.UndeterAgentIDs = new List<int>();
            this.StepMessages = new List<Message>();
            this.AgentReceiveOpinionsInStep = new Dictionary<Agent, Matrix<double>>();

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
            var same_subject_agents = agents.Where(agent => agent.MySubject == cor_subject).ToList();
            var cor_agents = same_subject_agents.Where(agent => agent.OpinionDim() == cor_dim).ToList();
            var undeter_agents = same_subject_agents.Where(agent => agent.OpinionDim() == -1).ToList();
            var incor_agents = same_subject_agents.Except(cor_agents).Except(undeter_agents).ToList();
            var network_size = agents.Count;

            this.CorrectAgentIDs = cor_agents.Select(agent => agent.AgentID).ToList();
            this.IncorrectAgentIDs = incor_agents.Select(agent => agent.AgentID).ToList();
            this.UndeterAgentIDs = undeter_agents.Select(agent => agent.AgentID).ToList();
            this.NetworkSize = network_size;
        }

        public void RecordStepMessages(List<Message> step_messages)
        {
            this.StepMessages = step_messages;

            foreach (var step_message in step_messages)
            {
                Matrix<double> receive_op = null;
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

        }

        public List<int> GetActiveSensors()
        {
            return this.StepMessages.Where(message => message.FromAgent.AgentID < 0).Select(message => message.ToAgent.AgentID).ToList();
        }

        public List<int> GetActiveAgents()
        {
            return this.StepMessages.Where(message => message.FromAgent.AgentID >= 0).Select(message => message.FromAgent.AgentID).ToList();
        }

    }
}
