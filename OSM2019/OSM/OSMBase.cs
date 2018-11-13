using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    abstract class OSMBase<T> : I_OSM
    {
        ExtendRandom UpdateStepRand;
        public AgentNetwork MyAgentNetwork { get; protected set; }
        public EnvironmentManager MyEnvManager { get; protected set; }
        public int CurrentStep { get; protected set; }
        public int CurrentRound { get; protected set; }

        public OSMBase()
        {
            this.CurrentStep = 0;
            this.CurrentRound = 0;
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
            return (T)(object)this;
        }

        public virtual void RecordStep()
        {

        }

        public virtual void UpdateStep()
        {
            this.SendOpinion();
            this.ReceiveOpinion();
            this.CurrentStep++;
        }

        public virtual void UpdateSteps(int steps)
        {
            for (int step = 0; step < steps; step++)
            {
                this.UpdateStep();
                this.RecordStep();
                this.InitializeStep();
            }
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

        protected virtual void SendOpinion()
        {
            this.AgentSendOpinion();
            this.EnvSendOpinion();
        }

        protected virtual void AgentSendOpinion()
        {

        }

        protected virtual void EnvSendOpinion()
        {

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

    }
}
