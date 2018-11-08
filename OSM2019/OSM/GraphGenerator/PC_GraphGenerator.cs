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
        int NodeSize;
        int M;
        double P;
        public override GraphEnum MyGraphEnum { get; }
        public override string GeneratePath { get; protected set; }
        public override bool SeedEnable { get; protected set; }

        public PC_GraphGenerator()
        {
            this.MyGraphEnum = GraphEnum.PowerLawCluster;
            this.SeedEnable = true;
            this.SetGeneratePath();
        }

        public PC_GraphGenerator SetRandomEdges(int m)
        {
            this.M = m;
            this.SetGeneratePath();
            return this;
        }

        public PC_GraphGenerator SetAddTriangleP(double p)
        {
            this.P = p;
            this.SetGeneratePath();
            return this;
        }

        public PC_GraphGenerator SetNodeSize(int n)
        {
            this.NodeSize = n;
            this.SetGeneratePath();
            return this;
        }

        protected override void SetGeneratePath()
        {
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "powerlaw_cluster_graph.py";
            this.GeneratePath = "" + path + " " + this.NodeSize + " " + this.M + " " + this.P;
        }
    }
}
