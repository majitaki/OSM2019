using Newtonsoft.Json;
using OSM2019.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class BasicAgent : I_Agent
    {
        public int AgentID { get; }

        public Dictionary<int, double> BeliefDic { get; set; }
        public int Opinion { get; set; }
        public bool IsSensor { get; set; }
        public bool IsEnvironment { get; set; }
        public List<I_AgentLink> MyAgentLinkList { get; set; }
        public string InitState { get; }

        public BasicAgent(int id, Dictionary<int, double> belief_dic, List<I_AgentLink> agent_link_list)
        {
            this.InitState = JsonConvert.SerializeObject(this, Formatting.Indented);
            this.MyAgentLinkList = agent_link_list;
        }

        public void Initialize()
        {
            var agent = JsonConvert.DeserializeObject<BasicAgent>(this.InitState);
            this.BeliefDic = agent.BeliefDic;
            this.Opinion = agent.Opinion;
            this.IsSensor = agent.IsSensor;
            this.IsEnvironment = agent.IsEnvironment;
            this.MyAgentLinkList = agent.MyAgentLinkList;
        }
    }
}
