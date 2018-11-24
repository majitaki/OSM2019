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
        public int MyStep { get; private set; }
        public List<int> CorrectAgentIDs { get; private set; }
        public List<int> IncorrectAgentIDs { get; private set; }
        public List<int> UndeterAgentIDs { get; private set; }
        public List<Message> StepMessages { get; private set; }
        public int NetworkSize { get; private set; }

        public RecordStep(int cur_step)
        {
            this.MyStep = cur_step;
            this.CorrectAgentIDs = new List<int>();
            this.IncorrectAgentIDs = new List<int>();
            this.UndeterAgentIDs = new List<int>();
            this.StepMessages = new List<Message>();
        }

        public void RecordStepAgents(List<Agent> agents, EnvironmentManager env_mgr)
        {
            var cor_dim = env_mgr.CorrectDim;
            var cor_agents = agents.Where(agent => agent.OpinionDim() == cor_dim).ToList();
            var undeter_agents = agents.Where(agent => agent.OpinionDim() == -1).ToList();
            var incor_agents = agents.Except(cor_agents).Except(undeter_agents).ToList();
            var network_size = agents.Count;

            this.CorrectAgentIDs = cor_agents.Select(agent => agent.AgentID).ToList();
            this.IncorrectAgentIDs = incor_agents.Select(agent => agent.AgentID).ToList();
            this.UndeterAgentIDs = undeter_agents.Select(agent => agent.AgentID).ToList();
            this.NetworkSize = network_size;
        }

        public void RecordStepMessages(List<Message> step_messages)
        {
            this.StepMessages = step_messages;
        }

        public List<int> GetActiveSensors()
        {
            return this.StepMessages.Where(message => message.FromAgent.AgentID < 0).Select(message => message.ToAgent.AgentID).ToList();
        }

        public List<int> GetActiveAgents()
        {
            return this.StepMessages.Where(message => message.FromAgent.AgentID >= 0).Select(message => message.FromAgent.AgentID).ToList();
        }

        public Matrix<double> GetReceiveOpinions(Agent agent)
        {
            Matrix<double> receive_opinions = null;
            var receive_messages = this.StepMessages.Where(message => message.ToAgent == agent);

            foreach (var rec_message in receive_messages)
            {
                Matrix<double> receive_op;
                if (rec_message.Subject != rec_message.ToAgent.MySubject)
                {
                    var to_subject = rec_message.ToAgent.MySubject;
                    receive_op = rec_message.Subject.ConvertOpinionForSubject(rec_message.Opinion, to_subject);
                }
                else
                {
                    receive_op = rec_message.Opinion.Clone();
                }

                if (receive_opinions == null)
                {
                    receive_opinions = receive_op;
                }
                else
                {
                    receive_opinions += receive_op;
                }
            }
            return receive_opinions;
        }
    }
}
