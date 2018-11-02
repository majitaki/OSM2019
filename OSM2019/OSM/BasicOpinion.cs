using MathNet.Numerics.LinearAlgebra;
using OSM2019.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class BasicOpinion : I_Opinion
    {
        public int OpinionID { get; }
        public Matrix<double> Value { get; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public I_Opinion CreateClone()
        {
            return (I_Opinion)this.Clone();
        }
    }
}
