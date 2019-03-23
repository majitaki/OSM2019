using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class OpinionEnvironment
    {
        public double SensorRate { get; protected set; }
        public OpinionSubject EnvSubject { get; protected set; }
        public int CorrectDim { get; protected set; }
        Agent EnvironmentAgent;
        bool ManualSensorRateMode;
        public List<double> SensorRates { get; protected set; }

        public OpinionEnvironment()
        {
            this.SensorRates = new List<double>();
            ManualSensorRateMode = false;
        }

        public OpinionEnvironment SetSubject(OpinionSubject subject)
        {
            this.EnvSubject = subject;
            return this;
        }

        public OpinionEnvironment SetCorrectDim(int cor_dim)
        {
            this.CorrectDim = cor_dim;
            return this;
        }

        public OpinionEnvironment SetSensorRate(double sensor_rate)
        {
            this.ManualSensorRateMode = false;
            this.SensorRate = sensor_rate;
            return this;
        }

        public OpinionEnvironment SetSensorRates(List<double> sensor_rates)
        {
            this.ManualSensorRateMode = true;
            this.SensorRates = sensor_rates;
            return this;
        }

        public void AddEnvironment(AgentNetwork agent_network)
        {
            this.EnvironmentAgent = new Agent(-1).SetSubject(this.EnvSubject);
            var op_vector = Vector<double>.Build.Dense(this.EnvSubject.SubjectDimSize, 0.0);
            this.EnvironmentAgent.SetInitOpinion(op_vector);
            List<AgentLink> env_links = new List<AgentLink>();
            var sensors = agent_network.Agents.Where(agent => agent.IsSensor).ToList();

            int link_index = -1;
            foreach (var sensor in sensors)
            {
                env_links.Add(new AgentLink(link_index, sensor, this.EnvironmentAgent));
                link_index--;
            }

            foreach (var sensor in sensors)
            {
                sensor.AttachAgentLinks(env_links);
            }

            this.EnvironmentAgent.AttachAgentLinks(env_links);
        }

        public List<Message> SendMessages(List<Agent> sensor_agents, ExtendRandom update_step_rand)
        {
            List<Message> messages = new List<Message>();
            foreach (var sensor_agent in sensor_agents)
            {
                var agent_link = this.EnvironmentAgent.AgentLinks.Where(link => link.SourceAgent == sensor_agent || link.TargetAgent == sensor_agent).First();
                var opinion = this.EnvironmentAgent.Opinion.Clone();
                opinion.Clear();

                if (this.ManualSensorRateMode)
                {
                    var random_num = update_step_rand.NextDouble();
                    double stock_random_num = 0;
                    int count_num = 0;
                    foreach (var s_r in this.SensorRates)
                    {
                        stock_random_num += s_r;
                        if (stock_random_num >= random_num || s_r ==this.SensorRates.Last()) break;
                        count_num += 1;
                    }
                    opinion[count_num] = 1.0;
                }
                else
                {
                    if (update_step_rand.NextDouble() < this.SensorRate)
                    {
                        opinion[this.CorrectDim] = 1.0;
                    }
                    else
                    {
                        List<int> incor_dim_list = Enumerable.Range(0, opinion.Count).Where(i => i != this.CorrectDim).ToList();
                        int incor_dim = incor_dim_list.OrderBy(_ => update_step_rand.Next()).First();
                        opinion[incor_dim] = 1.0;
                    }
                }
                messages.Add(new Message(this.EnvironmentAgent, sensor_agent, agent_link, opinion));
            }

            return messages;
        }

    }
}
