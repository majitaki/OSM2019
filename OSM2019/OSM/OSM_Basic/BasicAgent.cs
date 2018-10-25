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
        public int OpinionSize { get; }
        public bool IsSensor { get; set; }
        public bool IsEnvironment { get; set; }
        public List<int> MyAgentLinkIDList { get; set; }

        [JsonIgnore]
        public List<I_AgentLink> MyAgentLinkList { get; set; }

        [JsonIgnore]
        public string InitState { get; private set; }

        public BasicAgent(int id, Dictionary<int, double> belief_dic, List<int> agent_link_id_list, int op_size)
        {
            this.AgentID = id;
            this.BeliefDic = belief_dic;
            this.MyAgentLinkIDList = agent_link_id_list;
            this.OpinionSize = op_size;
            this.IsSensor = false;
            this.IsEnvironment = false;
            this.InitState = JsonConvert.SerializeObject(this);
        }

        [JsonConstructor]
        public BasicAgent(int AgentID, Dictionary<int, double> BeliefDic, int Opinion, int OpinionSize, bool IsSensor, bool IsEnvironment, List<int> MyAgentLinkIDList)
        {
            this.AgentID = AgentID;
            this.BeliefDic = BeliefDic;
            this.Opinion = Opinion;
            this.OpinionSize = OpinionSize;
            this.IsSensor = IsSensor;
            this.IsEnvironment = IsEnvironment;
            this.MyAgentLinkIDList = MyAgentLinkIDList;
        }


        public void SetInitState()
        {
            this.InitState = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public void Initialize(List<I_AgentLink> agentlink_list)
        {
            var agent = JsonConvert.DeserializeObject<BasicAgent>(this.InitState);
            this.BeliefDic = agent.BeliefDic;
            this.Opinion = agent.Opinion;
            this.IsSensor = agent.IsSensor;
            this.IsEnvironment = agent.IsEnvironment;
            this.MyAgentLinkIDList = agent.MyAgentLinkIDList;
            this.MyAgentLinkList = agentlink_list.Where(agent_link => this.MyAgentLinkIDList.Contains(agent_link.AgentLinkID)).ToList();

        }

    }
}
