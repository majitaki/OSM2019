using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    abstract class OSMBase<T> : I_OSM
    {
        public AgentNetwork MyAgentNetwork { get; set; }
        public int CurrentStep { get; set; }
        public int CurrentRound { get; set; }

        ExtendRandom UpdateStepRand;
        public EnvironmentManager MyEnvManager { get; protected set; }
        public SubjectManager MySubjectManager { get; protected set; }
        public double OpinionIntroRate { get; protected set; }
        public double OpinionIntroInterval { get; protected set; }
        public CalcWeightMode MyCalcWeightMode { get; protected set; }
        AggregationFunctions MyAggFuncs;


        public OSMBase()
        {
            this.CurrentStep = 0;
            this.CurrentRound = 0;
            this.MyAggFuncs = new AggregationFunctions();
        }

        public T SetRand(ExtendRandom update_step_rand)
        {
            this.UpdateStepRand = update_step_rand;
            return (T)(object)this;
        }

        public virtual T SetAgentNetwork(AgentNetwork agent_network)
        {
            this.MyAgentNetwork = agent_network;
            return (T)(object)this;
        }

        public T SetEnvManager(EnvironmentManager env_mgr)
        {
            this.MyEnvManager = env_mgr;
            this.MyEnvManager.AddEnvironment(this.MyAgentNetwork);
            return (T)(object)this;
        }

        public T SetSubjectManager(SubjectManager subject_mgr)
        {
            this.MySubjectManager = subject_mgr;
            return (T)(object)this;
        }

        public T SetOpinionIntroRate(double op_intro_rate)
        {
            this.OpinionIntroRate = op_intro_rate;
            return (T)(object)this;
        }

        public T SetOpinionIntroInterval(int interval_step)
        {
            this.OpinionIntroInterval = interval_step;
            return (T)(object)this;
        }

        public T SetInitWeightsMode(CalcWeightMode mode)
        {
            this.MyCalcWeightMode = mode;
            return (T)(object)this;
        }

        public virtual void UpdateSteps(int steps)
        {
            var messages = new List<Message>();
            var op_form_agents = new List<Agent>();

            int end_step = this.CurrentStep + steps;
            for (; this.CurrentStep < end_step; this.CurrentStep++)
            {
                //sensor observe
                if (this.CurrentStep % this.OpinionIntroInterval == 0)
                {
                    var all_sensors = this.MyAgentNetwork.Agents.Where(agent => agent.IsSensor).ToList();
                    var observe_num = (int)(all_sensors.Count * this.OpinionIntroRate);
                    var observe_sensors = all_sensors.Select(agent => agent.AgentID).OrderBy(a => this.UpdateStepRand.Next()).Take(observe_num).Select(id => this.MyAgentNetwork.Agents[id]).ToList();
                    var env_messages = this.MyEnvManager.SendMessages(observe_sensors, this.UpdateStepRand);
                    messages.AddRange(env_messages);
                }

                //agent observe
                var op_form_messages = this.AgentSendMessages(op_form_agents);
                messages.AddRange(op_form_messages);
                op_form_agents.Clear();

                //agent receive
                foreach (var message in messages)
                {
                    this.UpdateBeliefByMessage(message);
                    var op_form_agent = this.UpdateOpinion(message);
                    op_form_agents.Add(op_form_agent);
                }
                messages.Clear();
                this.RecordStep();
            }

        }

        public virtual void UpdateRounds(int rounds, int steps)
        {
            int end_round = this.CurrentRound + rounds;
            for (; this.CurrentRound < end_round; this.CurrentRound++)
            {
                this.UpdateSteps(steps);
                this.RecordRound();
                this.InitializeToZeroStep();
            }

        }

        public virtual void InitializeToZeroStep()
        {
            foreach (var agent in this.MyAgentNetwork.Agents)
            {
                agent.SetBelief(agent.InitBelief.Clone());
                agent.Opinion = agent.InitOpinion.Clone();
            }
            this.CurrentStep = 0;
        }

        public virtual void RecordStep()
        {
            var cor_dim = this.MyEnvManager.CorrectDim;
            var cor_agents = this.MyAgentNetwork.Agents.Where(agent => agent.OpinionDim() == cor_dim).ToList();
            var undeter_agents = this.MyAgentNetwork.Agents.Where(agent => agent.OpinionDim() != 0).ToList();
            var incor_agents = this.MyAgentNetwork.Agents.Except(cor_agents).Except(undeter_agents).ToList();
            var network_size = this.MyAgentNetwork.Agents.Count;

            Console.WriteLine(
                $"|step:{this.CurrentStep:D4}|" +
                $"|cor:{Math.Round(cor_agents.Count / (double)network_size, 3):F3}|" +
                $"|incor:{Math.Round(incor_agents.Count / (double)network_size, 3):F3}|" +
                $"|undeter:{Math.Round(undeter_agents.Count / (double)network_size, 3):F3}|");
        }

        public virtual void RecordRound()
        {
            //this.MyAgentNetwork.Agents.ForEach(agent => Console.WriteLine(agent.Opinion.ToString()));
            var cor_dim = this.MyEnvManager.CorrectDim;
            var cor_agents = this.MyAgentNetwork.Agents.Where(agent => agent.OpinionDim() == cor_dim).ToList();

            Console.WriteLine($"round: {this.CurrentRound} " + $"acc: {Math.Round(cor_agents.Count / (double)this.MyAgentNetwork.Agents.Count, 3)}");
        }

        public virtual void InitializeToZeroRound()
        {
            this.InitializeToZeroStep();
            this.CurrentRound = 0;
        }

        protected virtual void UpdateBeliefByMessage(Message message)
        {
            Matrix<double> receive_op;
            var pre_belief = message.ToAgent.Belief;
            var weight = message.GetToWeight();

            if (message.Subject != message.ToAgent.MySubject)
            {
                var to_subject = message.ToAgent.MySubject;
                receive_op = message.Subject.ConvertOpinionForSubject(message.Opinion, to_subject);
            }
            else
            {
                receive_op = message.Opinion;
            }

            var updated_belief = this.MyAggFuncs.UpdateBelief(pre_belief, weight, receive_op);
            message.ToAgent.SetBelief(updated_belief);
        }

        protected virtual Agent UpdateOpinion(Message message)
        {
            var belief_list = message.ToAgent.Belief.Column(0).ToList();
            var op_list = message.ToAgent.Opinion.Column(0).ToList();
            var op_threshold = message.ToAgent.OpinionThreshold;

            for (int dim = 0; dim < belief_list.Count; dim++)
            {
                if (belief_list[dim] > op_threshold && op_list[dim] != 1)
                {
                    message.ToAgent.Opinion.Clear();
                    message.ToAgent.Opinion[dim, 0] = 1;
                    return message.ToAgent;
                }
            }
            return null;
        }

        protected virtual List<Message> AgentSendMessages(List<Agent> op_formed_agents)
        {
            List<Message> messages = new List<Message>();
            foreach (var agent in op_formed_agents)
            {
                if (agent == null) continue;
                var opinion = agent.Opinion.Clone();
                foreach (var to_agent in agent.GetNeighbors())
                {
                    var agent_link = agent.AgentLinks.Where(link => link.SourceAgent == to_agent || link.TargetAgent == to_agent).First();
                    messages.Add(new Message(agent, to_agent, agent_link, opinion));
                }
            }

            return messages;
        }



    }
}
