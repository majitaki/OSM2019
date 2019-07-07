using MathNet.Numerics.LinearAlgebra;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    interface I_DistGenerator
    {
        CustomDistribution MyCustomDistribution { get; set; }
        CustomDistribution Generate();
    }
}
