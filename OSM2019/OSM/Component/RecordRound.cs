using CsvHelper;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Statistics;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class RecordRound
    {
        public int Round { get; private set; }
        public List<int> CorrectSizes { get; private set; }
        public List<int> IncorrectSizes { get; private set; }
        public List<int> UndeterSizes { get; private set; }
        public List<int> StepMessageSizes { get; private set; }
        public List<int> ActiveSensorSizes { get; private set; }
        public List<int> ActiveAgentSizes { get; private set; }
        public List<int> DeterminedSensorSizes { get; private set; }
        public List<int> SensorSizes { get; private set; }
        public List<int> NetworkSizes { get; private set; }
        public List<int> FinalSteps { get; private set; }
        public List<double> AverageWeight { get; private set; }
        public List<double> VarWeight { get; private set; }
        public Dictionary<Agent, Vector<double>> AgentReceiveOpinionsInRound { get; private set; }
        public SubjectManager MySubjectManager { get; private set; }
        public Dictionary<OpinionSubject, Dictionary<int, List<int>>> AllOpinionSizes;


        public RecordRound()
        {

        }

        public RecordRound(int cur_round, List<Agent> agents, SubjectManager subject_manager)
        {
            this.Round = cur_round;
            this.MySubjectManager = subject_manager;
            this.AgentReceiveOpinionsInRound = new Dictionary<Agent, Vector<double>>();
            this.CorrectSizes = new List<int>();
            this.IncorrectSizes = new List<int>();
            this.UndeterSizes = new List<int>();
            this.StepMessageSizes = new List<int>();
            this.ActiveSensorSizes = new List<int>();
            this.ActiveAgentSizes = new List<int>();
            this.DeterminedSensorSizes = new List<int>();
            this.SensorSizes = new List<int>();
            this.NetworkSizes = new List<int>();
            this.FinalSteps = new List<int>();
            this.AverageWeight = new List<double>();
            this.VarWeight = new List<double>();
            this.AllOpinionSizes = new Dictionary<OpinionSubject, Dictionary<int, List<int>>>();

            foreach (var agent in agents)
            {
                var undeter_op = agent.InitOpinion.Clone();
                undeter_op.Clear();
                this.AgentReceiveOpinionsInRound.Add(agent, undeter_op);
            }

            foreach (var subject in this.MySubjectManager.Subjects)
            {
                var dim_dic = new Dictionary<int, List<int>>();
                foreach (var dim in Enumerable.Range(0, subject.SubjectDimSize))
                {
                    dim_dic.Add(dim, new List<int>());
                }
                this.AllOpinionSizes.Add(subject, dim_dic);
            }
        }

        public void RecordFinalStep(int cur_step)
        {
            this.FinalSteps.Add(cur_step);
        }

        public void RecordStepAgentNetwork(List<Agent> agents)
        {
            var cor_dim = this.MySubjectManager.OSM_Env.CorrectDim;
            var cor_subject = this.MySubjectManager.OSM_Env.EnvSubject;
            var correct_size = agents.Where(agent => agent.MySubject.SubjectName == cor_subject.SubjectName && agent.GetOpinionDim() == cor_dim).Count();
            var undeter_size = agents.Where(agent => agent.GetOpinionDim() == -1).Count();
            var network_size = agents.Count;
            var incorrect_size = network_size - correct_size - undeter_size;
            var sensor_size = agents.Where(agent => agent.IsSensor).Count();
            var determined_sensor_size = agents.Where(agent => agent.IsSensor && agent.IsDetermined()).Count();
            var ave_weights = agents.Select(agent => agent.AgentLinks.Average(link => link.GetWeight(agent))).Mean();
            var var_weights = agents.Select(agent => agent.AgentLinks.Average(link => link.GetWeight(agent))).PopulationVariance();

            this.CorrectSizes.Add(correct_size);
            this.UndeterSizes.Add(undeter_size);
            this.NetworkSizes.Add(network_size);
            this.IncorrectSizes.Add(incorrect_size);
            this.SensorSizes.Add(sensor_size);
            this.DeterminedSensorSizes.Add(determined_sensor_size);
            this.AverageWeight.Add(ave_weights);
            this.VarWeight.Add(var_weights);
            this.RecordAllOpinion(agents);
        }

        void RecordAllOpinion(List<Agent> agents)
        {
            foreach (var subject in this.AllOpinionSizes.Keys)
            {
                foreach (var dim in Enumerable.Range(0, subject.SubjectDimSize))
                {
                    var each_op_size = agents.Where(agent => agent.MySubject == subject && agent.GetOpinionDim() == dim).Count();
                    this.AllOpinionSizes[subject][dim].Add(each_op_size);
                }
            }
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

                this.AgentReceiveOpinionsInRound[step_message.ToAgent] += receive_op;
            }

            var active_sensor_size = step_messages.Where(message => message.FromAgent.AgentID < 0).Count();
            var active_agent_size = step_messages.Where(message => message.FromAgent.AgentID >= 0).Count();
            var step_message_size = step_messages.Count;

            this.ActiveSensorSizes.Add(active_sensor_size);
            this.ActiveAgentSizes.Add(active_agent_size);
            this.StepMessageSizes.Add(step_message_size);
        }

        public bool IsReceived(Agent agent)
        {
            if (this.AgentReceiveOpinionsInRound[agent].L2Norm() == 0) return false;
            return true;
        }

        public void PrintRecord()
        {
            double network_size = this.NetworkSizes.Last();

            Console.WriteLine(
               $"round:{this.Round:D4}|" +
               $"cor:{Math.Round(this.CorrectSizes.Last() / network_size, 3):F3}|" +
               $"incor:{Math.Round(this.IncorrectSizes.Last() / network_size, 3):F3}|" +
               $"undeter:{Math.Round(this.UndeterSizes.Last() / network_size, 3):F3}|"
               );

            foreach (var subject in this.MySubjectManager.Subjects)
            {
                Console.WriteLine($" -subject:{subject.SubjectName}|");
                foreach (var dim in Enumerable.Range(0, subject.SubjectDimSize))
                {
                    Console.Write($"  dim {dim}:");
                    Console.WriteLine($"{Math.Round(this.AllOpinionSizes[subject][dim].Last() / network_size, 3):F3}|");
                }
                //Console.WriteLine();
            }
        }
    }
}
