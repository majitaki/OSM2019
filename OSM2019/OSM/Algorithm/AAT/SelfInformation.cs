using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class SelfInformation
    {
        public Agent SourceAgent { get; private set; }
        public Agent NeighborAgent { get; private set; }
        public double Value { get; private set; }

        public SelfInformation(Agent source, Agent neighbor)
        {
            this.SourceAgent = source;
            this.NeighborAgent = neighbor;
            this.Value = 0;
        }

        public void UpdateValue(Vector<double> opinion)
        {
            foreach (var dim in Enumerable.Range(0, opinion.Count))
            {
                if (SourceAgent.Belief[dim] == 0) continue;
                this.Value += -1.0 * Math.Log(SourceAgent.Belief[dim], 2) * opinion[dim];
            }
        }

        public void ClearValue()
        {
            this.Value = 0;
        }

    }
}
