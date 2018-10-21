using OSM2019.Abstracts;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class Triangular_GraphGenerator : A_GraphGenerator
    {
        int M;
        int N;
        public override GraphEnum MyGraphEnum { get; }
        protected override string GeneratePath { get; }

        public Triangular_GraphGenerator(int m, int n)
        {
            this.M = m;
            this.N = n;
            this.MyGraphEnum = GraphEnum.Triangular;
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "triangular_graph.py";
            this.GeneratePath = path + " " + this.M + " " + this.N;
        }
    }
}
