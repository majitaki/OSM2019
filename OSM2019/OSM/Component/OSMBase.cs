using Konsole;
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
    abstract class OSMBase : I_OSM
    {
        public AgentNetwork MyAgentNetwork { get; set; }
        public int CurrentStep { get; set; }
        public int CurrentRound { get; set; }

        protected ExtendRandom UpdateStepRand;
        public OpinionEnvironment MyEnvManager { get; protected set; }
        public SubjectManager MySubjectManager { get; protected set; }
        public double OpinionIntroRate { get; protected set; }
        public double OpinionIntroInterval { get; protected set; }
        public CalcWeightMode MyCalcWeightMode { get; protected set; }
        protected AggregationFunctions MyAggFuncs;
        List<Message> Messages;
        List<Agent> OpinionFormedAgents;
        //public Dictionary<int, RecordStep> MyRecordSteps { get; set; }
        public RecordStep MyRecordStep { get; set; }
        public Dictionary<int, RecordRound> MyRecordRounds { get; set; }
        bool SensorCommonWeightMode;
        public double SensorCommonWeight { get; private set; }

        public OSMBase()
        {
            this.CurrentStep = 0;
            this.CurrentRound = 0;
            this.SensorCommonWeightMode = false;
            this.MyAggFuncs = new AggregationFunctions();
            Messages = new List<Message>();
            OpinionFormedAgents = new List<Agent>();
            this.MyRecordRounds = new Dictionary<int, RecordRound>();
            //this.MyRecordSteps = new Dictionary<int, RecordStep>();
        }

        public void SetRand(ExtendRandom update_step_rand)
        {
            this.UpdateStepRand = update_step_rand;
            return;
        }

        public virtual void SetAgentNetwork(AgentNetwork agent_network)
        {
            this.MyAgentNetwork = agent_network;
            this.MyRecordStep = new RecordStep(0, agent_network.Agents);
            return;
        }

        public void SetSubjectManager(SubjectManager subject_mgr)
        {
            this.MySubjectManager = subject_mgr;
            this.MyEnvManager = subject_mgr.OSM_Env;
            this.MyEnvManager.AddEnvironment(this.MyAgentNetwork);
            return;
        }

        public void SetOpinionIntroRate(double op_intro_rate)
        {
            this.OpinionIntroRate = op_intro_rate;
            return;
        }

        public void SetOpinionIntroInterval(int interval_step)
        {
            this.OpinionIntroInterval = interval_step;
            return;
        }

        public void SetInitWeightsMode(CalcWeightMode mode)
        {
            this.MyCalcWeightMode = mode;
            return;
        }

        public void SetSensorCommonWeight(double sensor_common_weight)
        {
            this.SensorCommonWeightMode = true;
            this.SensorCommonWeight = sensor_common_weight;
        }

        public virtual void PrintAgentInfo(Agent agent)
        {
            Console.WriteLine($"Agent ID: {agent.AgentID}");
            Console.WriteLine($"Sensor: {agent.IsSensor}");
            Console.WriteLine($"Belief");
            int dim = 0;
            foreach (var belief in agent.Belief.ToList())
            {
                Console.WriteLine($"- Dim: {dim} Value {belief}");
                dim++;
            }

            var is_changed = agent.IsChanged();
            Console.WriteLine($"Opinion (Changed:{is_changed})");
            dim = 0;
            foreach (var op in agent.Opinion.ToList())
            {
                Console.WriteLine($"- Dim: {dim} Value {op}");
                dim++;
            }

            if (this.MyRecordRounds.Count == 0) return;
            var cur_record_round = new RecordRound(this.CurrentStep, this.MyAgentNetwork.Agents);
            //cur_record_round.RecordSteps(this.MyRecordSteps);
            var record_steps = new Dictionary<int, RecordStep>();
            record_steps.Add(0, this.MyRecordStep);
            cur_record_round.RecordSteps(record_steps);
            var is_recived = cur_record_round.IsReceived(agent);
            Console.WriteLine($"Receive Opinion (Received:{is_recived})");
            var receive_op = cur_record_round.AgentReceiveOpinionsInRound[agent];
            dim = 0;
            foreach (var op in receive_op.ToList())
            {
                Console.WriteLine($"- Dim: {dim} Value {op}");
                dim++;
            }
        }

        protected virtual void UpdateStep()
        {
            //var record_step = new RecordStep(this.CurrentStep, this.MyAgentNetwork.Agents);

            //sensor observe
            if (this.CurrentStep % this.OpinionIntroInterval == 0)
            {
                var all_sensors = this.MyAgentNetwork.Agents.Where(agent => agent.IsSensor).ToList();
                var observe_num = (int)Math.Ceiling(all_sensors.Count * this.OpinionIntroRate);
                var observe_sensors = all_sensors.Select(agent => agent.AgentID).OrderBy(a => this.UpdateStepRand.Next()).Take(observe_num).Select(id => this.MyAgentNetwork.Agents[id]).ToList();
                //var observe_sensors = all_sensors.Where(sensor => !sensor.IsDetermined()).Select(agent => agent.AgentID).OrderBy(a => this.UpdateStepRand.Next()).Take(observe_num).Select(id => this.MyAgentNetwork.Agents[id]).ToList();
                var env_messages = this.MyEnvManager.SendMessages(observe_sensors, this.UpdateStepRand);
                Messages.AddRange(env_messages);
            }

            //agent observe
            var op_form_messages = this.AgentSendMessages(OpinionFormedAgents);
            Messages.AddRange(op_form_messages);
            OpinionFormedAgents.Clear();

            //agent receive
            foreach (var message in this.Messages)
            {
                this.UpdateBeliefByMessage(message);
                var op_form_agent = this.UpdateOpinion(message);
                OpinionFormedAgents.Add(op_form_agent);
            }

            //record_step.RecordStepMessages(this.Messages);
            //record_step.RecordStepAgents(this.MyAgentNetwork.Agents, this.MySubjectManager);
            //this.MyRecordSteps.Add(this.CurrentStep, record_step);
            this.MyRecordStep.RecordStepMessages(this.Messages);
            this.MyRecordStep.RecordStepAgents(this.MyAgentNetwork.Agents, this.MySubjectManager);

            this.Messages.Clear();
            this.CurrentStep++;
        }

        public virtual void UpdateSteps(int steps)
        {
            int cur_step = this.CurrentStep;
            int end_step = this.CurrentStep + steps;
            for (; cur_step < end_step; cur_step++)
            {
                this.UpdateStep();
            }
        }

        protected virtual void UpdateRound(int steps)
        {
            this.UpdateSteps(steps);
            this.UpdateRecordRound();
            this.UpdateRoundWithoutSteps();
        }

        public virtual void UpdateRecordRound()
        {
            var record_round = new RecordRound(this.CurrentRound, this.MyAgentNetwork.Agents);

            var record_steps = new Dictionary<int, RecordStep>();
            record_steps.Add(0, this.MyRecordStep);
            record_round.RecordSteps(record_steps);
            //record_round.RecordSteps(this.MyRecordSteps);
            this.MyRecordRounds.Add(this.CurrentRound, record_round);
        }

        public virtual void UpdateRoundWithoutSteps()
        {
            //this.PrintRound();
            this.InitializeToZeroStep();
            this.CurrentRound++;
        }

        public virtual void UpdateRounds(int rounds, int steps, ExtendProgressBar pb)
        {
            int cur_round = this.CurrentRound;
            int end_round = this.CurrentRound + rounds;
            var ori_tag = pb.Tag;
            for (; cur_round < end_round; cur_round++)
            {
                this.UpdateRound(steps);
                pb.Refresh($"{ori_tag} {cur_round}");
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
            //this.MyRecordSteps = new Dictionary<int, RecordStep>();
            this.MyRecordStep = new RecordStep(0, this.MyAgentNetwork.Agents);
            this.Messages.Clear();
            this.OpinionFormedAgents.Clear();
        }

        public virtual void InitializeToZeroRound()
        {
            this.InitializeToZeroStep();
            this.CurrentRound = 0;
            this.MyRecordRounds = new Dictionary<int, RecordRound>();
        }

        public virtual void PrintStep()
        {
            var cor_dim = this.MyEnvManager.CorrectDim;
            var cor_agents = this.MyAgentNetwork.Agents.Where(agent => agent.GetOpinionDim() == cor_dim).ToList();
            var undeter_agents = this.MyAgentNetwork.Agents.Where(agent => agent.GetOpinionDim() == -1).ToList();
            var incor_agents = this.MyAgentNetwork.Agents.Except(cor_agents).Except(undeter_agents).ToList();
            var network_size = this.MyAgentNetwork.Agents.Count;

            Console.WriteLine(
                $"|step:{this.CurrentStep:D4}|" +
                $"|cor:{Math.Round(cor_agents.Count / (double)network_size, 3):F3}|" +
                $"|incor:{Math.Round(incor_agents.Count / (double)network_size, 3):F3}|" +
                $"|undeter:{Math.Round(undeter_agents.Count / (double)network_size, 3):F3}|");
        }

        public virtual void PrintRound()
        {
            var cor_dim = this.MyEnvManager.CorrectDim;
            var cor_agents = this.MyAgentNetwork.Agents.Where(agent => agent.GetOpinionDim() == cor_dim).ToList();
            var undeter_agents = this.MyAgentNetwork.Agents.Where(agent => agent.GetOpinionDim() == -1).ToList();
            var incor_agents = this.MyAgentNetwork.Agents.Except(cor_agents).Except(undeter_agents).ToList();
            var network_size = this.MyAgentNetwork.Agents.Count;

            Console.WriteLine(
                $"|round:{this.CurrentRound:D4}|" +
                $"|cor:{Math.Round(cor_agents.Count / (double)network_size, 3):F3}|" +
                $"|incor:{Math.Round(incor_agents.Count / (double)network_size, 3):F3}|" +
                $"|undeter:{Math.Round(undeter_agents.Count / (double)network_size, 3):F3}|");
        }


        protected virtual void UpdateBeliefByMessage(Message message)
        {
            Vector<double> receive_op;
            var pre_belief = message.ToAgent.Belief;
            var weight = message.GetToWeight();

            if (message.Subject != message.ToAgent.MySubject)
            {
                var to_subject = message.ToAgent.MySubject;
                receive_op = message.Subject.ConvertOpinionForSubject(message.Opinion, to_subject);
            }
            else
            {
                receive_op = message.Opinion.Clone();
            }

            var updated_belief = this.MyAggFuncs.UpdateBelief(pre_belief, weight, receive_op);
            if (message.FromAgent.AgentID < 0)
            {
                double sensor_weight;
                if (this.SensorCommonWeightMode)
                {
                    sensor_weight = this.SensorCommonWeight;
                }
                else
                {
                    sensor_weight = this.MyEnvManager.SensorRate;
                }
                updated_belief = this.MyAggFuncs.UpdateBelief(pre_belief, sensor_weight, receive_op);
            }

            message.ToAgent.SetBelief(updated_belief);
        }

        protected virtual Agent UpdateOpinion(Message message)
        {
            var belief_list = message.ToAgent.Belief.ToList();
            var op_list = message.ToAgent.Opinion.ToList();
            var op_threshold = message.ToAgent.OpinionThreshold;

            for (int dim = 0; dim < belief_list.Count; dim++)
            {
                if (belief_list[dim] > op_threshold && op_list[dim] != 1)
                {
                    message.ToAgent.Opinion.Clear();
                    message.ToAgent.Opinion[dim] = 1;
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
