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
        public Matrix<double> InitOpinion { get; protected set; }
        public Matrix<double> Opinion { get; set; }
        public List<AgentLink> AgentLinks { get; protected set; }
        public double OpinionThreshold { get; protected set; }

        public int OpinionDim()
        {
            var op_list = Opinion.Column(0).ToList();

            for (int dim = 0; dim < op_list.Count; dim++)
            {
                if (op_list[dim] == 1) return dim;
            }
            return -1;
        }

        public List<T> GetNeighbors()
        {
            var neighbors = new List<T>();
            foreach (var agent_link in this.AgentLinks)
            {
                if (agent_link.TargetAgent.AgentID == -1) continue;
                Agent neighbor_agent;
                neighbor_agent = agent_link.TargetAgent.AgentID == this.AgentID ? agent_link.SourceAgent : agent_link.TargetAgent;

                neighbors.Add((T)(object)neighbor_agent);
            }
            return neighbors;
        }

        public T SetSubject(OpinionSubject subject)
        {
            this.MySubject = subject;
            var op_matrix = Matrix<double>.Build.Dense(this.MySubject.SubjectDimSize, 1, 0.0);
            this.SetInitOpinion(op_matrix);
            return (T)(object)this;
        }

        public T SetInitOpinion(Matrix<double> init_op_matrix)
        {
            if (this.MySubject.SubjectDimSize != init_op_matrix.RowCount)
            {
                throw new Exception("error not equal subject dim and init op dim");
            }
            this.InitOpinion = init_op_matrix.Clone();
            this.Opinion = init_op_matrix.Clone();
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
