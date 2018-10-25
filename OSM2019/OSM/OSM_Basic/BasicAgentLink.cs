using Newtonsoft.Json;
using OSM2019.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class BasicAgentLink : I_AgentLink
    {
        public int AgentLinkID { get; }
        public int SourceAgentID { get; }
        public int TargetAgentID { get; }
        public double SourceWeight { get; set; }
        public double TargetWeight { get; set; }

        [JsonIgnore]
        public I_Agent SourceAgent { get; private set; }
        [JsonIgnore]
        public I_Agent TargetAgent { get; private set; }

        [JsonIgnore]
        public string InitState { get; private set; }


        public BasicAgentLink(int link_id, Link link, int op_size)
        {
            this.AgentLinkID = link_id;
            this.SourceAgentID = link.Source;
            this.TargetAgentID = link.Target;
            this.SourceWeight = this.TargetWeight = 1.0 / op_size;
            this.InitState = JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        [JsonConstructor]
        public BasicAgentLink(int AgentLinkID, int SourceAgentID, int TargetAgentID, double SourceWeight, double TargetWeight)
        {
            this.AgentLinkID = AgentLinkID;
            this.SourceAgentID = SourceAgentID;
            this.TargetAgentID = TargetAgentID;
            this.SourceWeight = SourceWeight;
            this.TargetWeight = TargetWeight;
        }

        public void SetInitState()
        {
            this.InitState = JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public void Initialize(List<I_Agent> agent_list)
        {
            var agentlink = JsonConvert.DeserializeObject<BasicAgentLink>(this.InitState);
            this.SourceWeight = agentlink.SourceWeight;
            this.TargetWeight = agentlink.TargetWeight;

            this.SourceAgent = agent_list.First(agent => agent.AgentID == this.SourceAgentID);
            this.TargetAgent = agent_list.First(agent => agent.AgentID == this.TargetAgentID);
        }
    }
}
