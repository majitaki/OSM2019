using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Interfaces
{
    interface I_Opinion : ICloneable
    {
        int OpinionID { get; }
        Matrix<double> Value { get; }

        I_Opinion CreateClone();
    }
}
