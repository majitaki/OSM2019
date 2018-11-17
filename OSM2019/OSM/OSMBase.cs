using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    abstract class OSMBase<T> : I_OSM
    {
        ExtendRandom UpdateStepRand;
        public AgentNetwork MyAgentNetwork { get; protected set; }
        public EnvironmentManager MyEnvManager { get; protected set; }
        public SubjectManager MySubjectManager { get; protected set; }
        public int CurrentStep { get; protected set; }
        public int CurrentRound { get; protected set; }
        public double OpinionIntroRate { get; protected set; }
        public double OpinionIntroInterval { get; protected set; }

        Queue<int> op_formed_agent_ids;

        public OSMBase()
        {
            this.CurrentStep = 0;
            this.CurrentRound = 0;
            this.op_formed_agent_ids = new Queue<int>();
        }

        public T SetRand(ExtendRandom update_step_rand)
        {
            this.UpdateStepRand = update_step_rand;
            return (T)(object)this;
        }

        public T SetAgentNetwork(AgentNetwork agent_network)
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

        public virtual void UpdateSteps(int steps)
        {
            int _cur_step = this.CurrentStep;
            var step_stream =
                Observable.Range(_cur_step, _cur_step + steps).Publish();

            var messages = new List<Message>();
            //count step
            step_stream
                .Subscribe(
                step =>
                {
                    this.CurrentStep = step;
                    //Console.WriteLine(this.CurrentStep);
                });

            //sensor observe
            step_stream
                .Where(step => step % this.OpinionIntroInterval == 0)
                .Subscribe(
                step =>
                {
                    var all_sensors = this.MyAgentNetwork.Agents.Where(agent => agent.IsSensor).ToList();
                    var observe_num = (int)(all_sensors.Count * this.OpinionIntroRate);
                    var observe_sensors = all_sensors.Select(agent => agent.AgentID).OrderBy(a => this.UpdateStepRand.Next()).Take(observe_num).Select(id => this.MyAgentNetwork.Agents[id]).ToList();
                    var env_messages = this.MyEnvManager.SendMessages(observe_sensors, this.UpdateStepRand);
                    messages.AddRange(env_messages);
                });

            //agent observe
            step_stream
                .Subscribe(
                step =>
                {
                    var op_formed_agents = this.op_formed_agent_ids.Select(id => this.MyAgentNetwork.Agents[id]).ToList();
                    var op_form_messages = this.AgentSendMessages(op_formed_agents);
                    messages.AddRange(op_form_messages);
                });

            //agent receive
            step_stream
                .Subscribe(
                step =>
                {


                    messages.Clear();
                });

            var connection = step_stream.Connect();


            //for (int step = 0; step < steps; step++)
            //{
            //    this.UpdateStep();
            //    this.RecordStep();
            //    this.InitializeStep();
            //}
        }

        public virtual void InitializeStep()
        {
        }

        public virtual void InitializeToZeroStep()
        {
            foreach (var agent in this.MyAgentNetwork.Agents)
            {
                agent.Belief = agent.InitBelief.Clone();
                agent.Opinion = agent.InitOpinion.Clone();
            }
            this.CurrentStep = 0;
        }

        public virtual void RecordRound()
        {

        }

        public virtual void UpdateRound(int steps)
        {
            this.UpdateSteps(steps);
            this.InitializeToZeroStep();
            this.CurrentRound++;
        }

        public virtual void UpdateRounds(int rounds, int steps)
        {
            for (int round = 0; round < rounds; round++)
            {
                this.UpdateRound(steps);
                this.RecordRound();
                this.InitializeRound();
            }
        }

        public virtual void InitializeRound()
        {

        }

        public virtual void InitializeToZeroRound()
        {
            this.CurrentRound = 0;
        }

        protected virtual void ReceiveOpinion()
        {
            this.UpdateBelief();
            this.UpdateOpinion();
        }

        protected virtual void UpdateBelief()
        {

        }

        protected virtual void UpdateOpinion()
        {

        }


        protected virtual List<Message> AgentSendMessages(List<Agent> op_formed_agents)
        {
            List<Message> messages = new List<Message>();
            foreach (var agent in op_formed_agents)
            {
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
