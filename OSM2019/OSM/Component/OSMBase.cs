using Konsole;
using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Dictionary<int, SubjectManager> MySubjectManagerDic { get; protected set; }
        public List<int> MySensorChangeRoundList { get; protected set; }
        public SubjectManager MySubjectManager { get; protected set; }
        public double OpinionIntroRate { get; protected set; }
        public double OpinionIntroInterval { get; protected set; }
        public CalcWeightMode MyCalcWeightMode { get; protected set; }
        public AggregationFunctions MyAggFuncs { get; protected set; }
        protected List<Message> Messages;
        protected List<Agent> OpinionFormedAgents;
        //public RecordStep MyRecordStep { get; set; }
        //public Dictionary<int, RecordRound> MyRecordRounds { get; set; }
        public double CommonWeight { get; private set; }
        public bool CommonWeightMode { get; private set; }
        public RecordRound MyRecordRound { get; set; }
        public List<RecordRound> MyRecordRounds { get; set; }
        public bool SimpleRecordFlag { get; set; }
        public BeliefUpdater MyBeliefUpdater { get; protected set; }

        public OSMBase()
        {
            this.CommonWeight = 0.0;
            this.CommonWeightMode = false;
            this.CurrentStep = 0;
            this.CurrentRound = 0;
            //this.SensorCommonWeightMode = false;
            this.SimpleRecordFlag = false;
            this.MyAggFuncs = new AggregationFunctions();
            Messages = new List<Message>();
            OpinionFormedAgents = new List<Agent>();
            //this.MyRecordRounds = new Dictionary<int, RecordRound>();
            //this.MyRecordSteps = new Dictionary<int, RecordStep>();
            this.MyRecordRound = new RecordRound();
            this.MyRecordRounds = new List<RecordRound>();
        }

        public void SetRand(ExtendRandom update_step_rand)
        {
            this.UpdateStepRand = update_step_rand;
            return;
        }

        public virtual void SetAgentNetwork(AgentNetwork agent_network)
        {
            this.MyAgentNetwork = agent_network;

            if (this.CommonWeightMode)
            {
                foreach (var link in this.MyAgentNetwork.AgentLinks)
                {
                    link.SetInitSourceWeight(this.CommonWeight);
                    link.SetInitTargetWeight(this.CommonWeight);
                }
            }

            return;
        }

        public virtual void SetAgentNetwork()
        {
            Debug.Assert(this.MyAgentNetwork != null);
            this.MyAgentNetwork.GenerateSensor();
        }

        public void SetSubjectManagerDic(Dictionary<int, SubjectManager> subject_manager_dic)
        {
            this.MySubjectManagerDic = subject_manager_dic;
        }

        public void SetSensorChangeRoundList(List<int> sensor_change_round_list)
        {
            this.MySensorChangeRoundList = sensor_change_round_list;
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

        public virtual void SetCommonWeight(double common_weight)
        {
            this.CommonWeight = common_weight;
            this.CommonWeightMode = true;


        }

        public virtual void SetBeliefUpdater(BeliefUpdater belief_updater)
        {
            this.MyBeliefUpdater = belief_updater;
        }

        //step
        public virtual void InitializeToFirstStep()
        {
            if (this.MySubjectManagerDic.ContainsKey(this.CurrentRound))
            {
                this.SetAgentNetwork();
                this.SetSubjectManager(this.MySubjectManagerDic[this.CurrentRound]);
            }

            foreach (var agent in this.MyAgentNetwork.Agents)
            {
                agent.SetBelief(agent.InitBelief.Clone());
                agent.Opinion = agent.InitOpinion.Clone();
            }
            this.CurrentStep = 0;
            this.Messages.Clear();
            this.OpinionFormedAgents.Clear();
            this.MyRecordRound = new RecordRound(this.CurrentRound, this.MyAgentNetwork.Agents, this.MySubjectManager);

        }

        public virtual void InitializeStep()
        {
            this.Messages.Clear();
        }

        public virtual void NextStep()
        {
            //sensor observe
            if (this.CurrentStep % this.OpinionIntroInterval == 0)
            {
                var all_sensors = this.MyAgentNetwork.Agents.Where(agent => agent.IsSensor).ToList();
                var observe_num = (int)Math.Ceiling(all_sensors.Count * this.OpinionIntroRate);
                var observe_sensors = all_sensors.Select(agent => agent.AgentID).OrderBy(a => this.UpdateStepRand.Next()).Take(observe_num).Select(id => this.MyAgentNetwork.Agents[id]).ToList();
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

            this.CurrentStep++;
        }

        public virtual void RecordStep(bool final)
        {
            if (this.SimpleRecordFlag)
            {
                if (final) this.MyRecordRound.RecordFinalStep(this.CurrentStep);
                this.MyRecordRound.RecordStepMessages(this.Messages);
                if (final) this.MyRecordRound.RecordStepAgentNetwork(this.MyAgentNetwork.Agents);
            }
            else
            {
                this.MyRecordRound.RecordFinalStep(this.CurrentStep);
                this.MyRecordRound.RecordStepMessages(this.Messages);
                this.MyRecordRound.RecordStepAgentNetwork(this.MyAgentNetwork.Agents);
            }
        }

        public virtual void FinalizeStep()
        {
        }

        public virtual void UpdateSteps(int step_count)
        {


            foreach (var step in Enumerable.Range(0, step_count))
            {
                var final = (step == (step_count - 1));
                this.InitializeStep();
                this.NextStep();
                this.RecordStep(final);
                this.FinalizeStep();
            }
        }


        public virtual void PrintStepInfo()
        {
        }

        //round
        public virtual void InitializeToFirstRound()
        {
            this.CurrentRound = 0;
            this.InitializeToFirstStep();
            this.MyRecordRounds = new List<RecordRound>();
        }

        public virtual void InitializeRound()
        {
            this.InitializeToFirstStep();
        }

        public virtual void NextRound(int step_count)
        {
            this.UpdateSteps(step_count);
        }

        public virtual void RecordRound()
        {
            this.MyRecordRounds.Add(this.MyRecordRound);
        }

        public virtual void FinalizeRound()
        {
            this.CurrentRound++;
        }

        public virtual void UpdateRounds(int round_count, int step_count, ExtendProgressBar pb = null)
        {
            string ori_tag = "";
            if (pb != null) ori_tag = pb.Tag;

            foreach (var round in Enumerable.Range(0, round_count))
            {
                this.InitializeRound();
                this.NextRound(step_count);
                this.RecordRound();
                pb.RefreshWithoutChange($"{pb.Tag} {this.CurrentRound} ");
                this.FinalizeRound();
            }
        }

        public virtual void PrintRoundInfo()
        {
            this.MyRecordRound.PrintRecord();
        }


        //agent
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

            foreach (var link in agent.AgentLinks)
            {
                Console.WriteLine($"- Weight: {link.AgentLinkID} Value {link.GetWeight(agent)}");
            }

            if (this.MyRecordRounds.Count == 0) return;
            var is_recived = this.MyRecordRound.IsReceived(agent);
            Console.WriteLine($"Receive Opinion (Received:{is_recived})");
            var receive_op = this.MyRecordRound.AgentReceiveOpinionsInRound[agent];
            dim = 0;
            foreach (var op in receive_op.ToList())
            {
                Console.WriteLine($"- Dim: {dim} Value {op}");
                dim++;
            }
        }

        //osm
        protected virtual void UpdateBeliefByMessage(Message message)
        {
            var updated_belief = this.MyBeliefUpdater.UpdateBelief(this, message);

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

            //messages.RemoveAll(message => message.ToAgent.IsSensor && message.FromAgent.AgentID >= 0);

            return messages;
        }


        protected double GetObsU(Vector<double> received_sum_op)
        {
            return received_sum_op.Sum();

            //var max_op_len = received_sum_op.Max();
            //var max_index = received_sum_op.MaximumIndex();

            //for (int index = 0; index < received_sum_op.Count; index++)
            //{
            //    if (index == max_index) continue;
            //    max_op_len -= received_sum_op[index];
            //    if (max_op_len <= 0) return 0;
            //}
            //return max_op_len;
        }
    }
}
