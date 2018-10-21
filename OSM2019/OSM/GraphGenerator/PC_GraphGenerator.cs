using OSM2019.Abstracts;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class PC_GraphGenerator : A_GraphGenerator
    {
        int NodeNum;
        int M;
        double P;
        public override GraphEnum MyGraphEnum { get; }
        protected override string GeneratePath { get; }

        public PC_GraphGenerator(int n, int m, double p)
        {
            this.NodeNum = n;
            this.M = m;
            this.P = p;
            this.MyGraphEnum = GraphEnum.PowerLawCluster;
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "powerlaw_cluster_graph.py";
            this.GeneratePath = "" + path + " " + this.NodeNum + " " + this.M + " " + this.P;
        }
    }
}
