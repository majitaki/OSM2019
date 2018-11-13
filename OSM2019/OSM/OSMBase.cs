using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    abstract class OSMBase<T>
    {
        ExtendRandom UpdateStepRand;
        public AgentNetwork MyAgentNetwork { get; protected set; }

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

        public virtual void RecordStep()
        {

        }


        public virtual void UpdateStep()
        {

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
                agent.MyBelief = agent.MyInitBelief.Clone();
                agent.MyOpinion = agent.InitOpinion.Clone();
            }

        }

        public virtual void RecordRound()
        {

        }

        public virtual void UpdateRound(int steps)
        {
            this.UpdateSteps(steps);
            this.InitializeToZeroStep();
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

        }

    }
}
