using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class ER_GraphGenerator : GraphGeneratorBase
    {
        int NodeSize;
        double P;
        public override GraphEnum MyGraphEnum { get; }
        public override string GeneratePath { get; protected set; }
        public override bool SeedEnable { get; protected set; }

        public ER_GraphGenerator()
        {
            this.MyGraphEnum = GraphEnum.ER;
            this.SeedEnable = true;
            this.SetGeneratePath();
        }

        public ER_GraphGenerator SetNodeSize(int n)
        {
            this.NodeSize = n;
            this.SetGeneratePath();
            return this;
        }

        public ER_GraphGenerator SetEdgeCreateP(double p)
        {
            this.P = p;
            this.SetGeneratePath();
            return this;
        }

        protected override void SetGeneratePath()
        {
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "erdos_renyi_graph.py";
            this.GeneratePath = path + " " + this.NodeSize + " " + this.P;
        }
    }
}
