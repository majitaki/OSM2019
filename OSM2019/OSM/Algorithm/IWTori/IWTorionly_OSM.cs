﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class IWTorionly_OSM : OSM_Only
    {
        public List<SelfInformation> SelfInformations { get; private set; }
        public double CommonCuriocity { get; private set; }
        public Dictionary<Agent, double> AgentCuriocities { get; private set; }
        public bool IsRandomCommonCuriocity { get; set; }

        public void SetCommonCuriocity(double common_curiocity)
        {
            this.AgentCuriocities = new Dictionary<Agent, double>();
            this.CommonCuriocity = common_curiocity;
        }

        public override void SetAgentNetwork(AgentNetwork agent_network)
        {
            base.SetAgentNetwork(agent_network);
            foreach (var agent in this.MyAgentNetwork.Agents)
            {
                if (this.IsRandomCommonCuriocity)
                {
                    var random_cc = this.MyAgentNetwork.AgentGenerateRand.NextDouble();
                    random_cc = Math.Round(random_cc, 4);
                    this.AgentCuriocities.Add(agent, random_cc);
                }
                else
                {
                    this.AgentCuriocities.Add(agent, this.CommonCuriocity);
                }
            }
            this.SetSelfInformations(agent_network);
        }

        public override void PrintAgentInfo(Agent agent)
        {
            base.PrintAgentInfo(agent);

            var cc = this.AgentCuriocities[agent];
            Console.Write($"Curiocity {cc}");
            Console.WriteLine();
        }



        public override void InitializeToFirstRound()
        {
            base.InitializeToFirstRound();
            this.SetSelfInformations(this.MyAgentNetwork);
        }

        protected void SetSelfInformations(AgentNetwork agent_network)
        {
            this.SelfInformations = new List<SelfInformation>();

            foreach (var agent in agent_network.Agents)
            {
                foreach (var neighbor_agent in agent.GetNeighbors())
                {
                    this.SelfInformations.Add(new SelfInformation(agent, neighbor_agent));
                }
            }
        }

        public override void NextStep()
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
                //update selfinfo
                if (message.FromAgent.AgentID >= 0) this.SelfInformations.First(selfinfo => message.ToAgent == selfinfo.SourceAgent && message.FromAgent == selfinfo.NeighborAgent).UpdateValue(message.Opinion);
                this.UpdateBeliefByMessage(message);
                var op_form_agent = this.UpdateOpinion(message);
                OpinionFormedAgents.Add(op_form_agent);
            }

            this.CurrentStep++;
        }


        protected void SelectionWeight()
        {



            foreach (var agent in this.MyAgentNetwork.Agents)
            {
                var received_sum_op = this.MyRecordRounds.Last().AgentReceiveOpinionsInRound[agent];
                double obs_u = this.GetObsU(received_sum_op);
                if (obs_u == 0) continue;


                //var weights = this.CalcIndividualCuriocities(agent, this.CommonWeight);
                //if (weights.Where(w => Double.IsNaN(w.Value)).Count() > 0)
                //{
                //    Console.WriteLine();
                //}
                //agent.SetWeights(weights);

                if (agent.IsSensor)
                {
                    agent.SetCommonWeight(this.CommonWeight);
                }
                else
                {
                    var weights = this.CalcIndividualCuriocities(agent, this.CommonWeight);
                    if (weights.Where(w => Double.IsNaN(w.Value)).Count() > 0)
                    {
                        Console.WriteLine();
                    }
                    agent.SetWeights(weights);
                }
            }
        }

        public override void FinalizeRound()
        {
            this.SelectionWeight();
            foreach (var self_info in this.SelfInformations)
            {
                self_info.ClearValue();
            }
            base.FinalizeRound();

        }

        Dictionary<Agent, double> CalcIndividualCuriocities(Agent agent, double sel_can_weight)
        {
            var agent_self_informations = this.SelfInformations.Where(self_info => self_info.SourceAgent == agent).ToList();
            var total_info_value = agent_self_informations.Select(self_info => self_info.Value).Sum();
            var max_info_value = agent_self_informations.Select(self_info => self_info.Value).Max();
            var ave_info_value = agent_self_informations.Select(self_info => self_info.Value).Average();


            if (agent_self_informations.Where(w => Double.IsNaN(w.Value)).Count() > 0)
            {
                Console.WriteLine();
            }

            Dictionary<Agent, double> weights = new Dictionary<Agent, double>();
            foreach (var self_info in agent_self_informations)
            {
                var indivi_curiocity = 0.0;
                var indivi_weight = 0.0;

                indivi_curiocity = (self_info.Value / max_info_value) * (1 - 1.0 / agent.MySubject.SubjectDimSize) + (1.0 / agent.MySubject.SubjectDimSize);
                indivi_weight = sel_can_weight * (1 - this.AgentCuriocities[agent]) + indivi_curiocity * this.AgentCuriocities[agent];
                indivi_weight = Math.Round(indivi_weight, 4);

                //if (max_info_value == 0)
                //{
                //    indivi_weight = sel_can_weight;
                //}
                //else
                //{
                //    indivi_curiocity = (self_info.Value / max_info_value) * (1 - 1.0 / agent.MySubject.SubjectDimSize) + (1.0 / agent.MySubject.SubjectDimSize);
                //    indivi_weight = sel_can_weight * (1 - this.AgentCuriocities[agent]) + indivi_curiocity * this.AgentCuriocities[agent];
                //    indivi_weight = Math.Round(indivi_weight, 4);

                //}

                weights.Add(self_info.NeighborAgent, indivi_weight);

                if (Double.IsNaN(indivi_weight))
                {
                    Console.WriteLine();
                }
            }
            return weights;
        }

    }

}
