using MathNet.Numerics.LinearAlgebra;
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
        public string Subject { get; protected set; }
        public Matrix<double> InitOpinion { get; protected set; }
        public Matrix<double> MyOpinion { get; set; }
        public List<AgentLink> AgentLinks { get; protected set; }
        public double OpinionThreshold { get; protected set; }

        public List<T> GetNeighbors()
        {
            var neighbors = new List<T>();
            foreach (var agent_link in this.AgentLinks)
            {
                neighbors.Add((T)(object)agent_link.TargetAgent);
            }
            return neighbors;
        }

        public T SetSubject(string subject)
        {
            this.Subject = subject;
            return (T)(object)this;
        }

        public T SetInitOpinion(Matrix<double> init_op_matrix)
        {
            this.InitOpinion = init_op_matrix.Clone();
            this.MyOpinion = init_op_matrix.Clone();
            return (T)(object)this;
        }

        public T SetSensor(bool is_sensor)
        {
            this.IsSensor = is_sensor;
            return (T)(object)this;
        }
    }
}
