using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    abstract class AgentBase<T>
    {
        public int AgentID { get; protected set; }
        public bool IsSensor { get; set; }
        public OpinionSubject MySubject { get; protected set; }
        public Vector<double> InitOpinion { get; protected set; }
        public Vector<double> Opinion { get; set; }
        public List<AgentLink> AgentLinks { get; protected set; }
        public double OpinionThreshold { get; protected set; }

        public int GetOpinionDim()
        {
            var max_dim = Opinion.Count;

            for (int dim = 0; dim < max_dim; dim++)
            {
                if (this.Opinion[dim] == 1) return dim;
            }
            return -1;
        }

        public bool IsDetermined()
        {
            var undeter_op = this.Opinion.Clone();
            undeter_op.Clear();

            return (!this.Opinion.Equals(undeter_op)) ? true : false;
        }

        public bool IsChanged()
        {
            var init_op = this.InitOpinion.Clone();

            return (!this.Opinion.Equals(init_op)) ? true : false;
        }

        public List<T> GetNeighbors()
        {
            var neighbors = new List<T>();
            foreach (var agent_link in this.AgentLinks)
            {
                if (agent_link.TargetAgent.AgentID < 0) continue;
                Agent neighbor_agent;
                neighbor_agent = agent_link.TargetAgent.AgentID == this.AgentID ? agent_link.SourceAgent : agent_link.TargetAgent;

                neighbors.Add((T)(object)neighbor_agent);
            }
            return neighbors;
        }

        public T SetSubject(OpinionSubject subject)
        {
            this.MySubject = subject;
            var op_vector = Vector<double>.Build.Dense(this.MySubject.SubjectDimSize, 0.0);
            this.SetInitOpinion(op_vector);
            return (T)(object)this;
        }

        public T SetInitOpinion(Vector<double> init_op_vector)
        {
            if (this.MySubject.SubjectDimSize != init_op_vector.Count)
            {
                throw new Exception("error not equal subject dim and init op dim");
            }
            this.InitOpinion = init_op_vector.Clone();
            this.Opinion = init_op_vector.Clone();
            return (T)(object)this;
        }

        public T SetSensor(bool is_sensor)
        {
            this.IsSensor = is_sensor;
            return (T)(object)this;
        }

        public T SetThreshold(double threshold)
        {
            this.OpinionThreshold = threshold;
            return (T)(object)this;
        }
    }
}
