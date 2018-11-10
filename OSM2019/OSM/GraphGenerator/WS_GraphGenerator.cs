using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class WS_GraphGenerator : GraphGeneratorBase
    {
        int NodeSize;
        int K;
        double RewireP;
        public override GraphEnum MyGraphEnum { get; }
        public override string GeneratePath { get; protected set; }
        public override bool SeedEnable { get; protected set; }

        public WS_GraphGenerator()
        {
            this.MyGraphEnum = GraphEnum.WS;
            this.SeedEnable = true;
            this.SetGeneratePath();
        }

        public WS_GraphGenerator SetNodeSize(int n)
        {
            this.NodeSize = n;
            this.SetGeneratePath();
            return this;
        }

        public WS_GraphGenerator SetNearestNeighbors(int k)
        {
            this.K = k;
            this.SetGeneratePath();
            return this;
        }

        public WS_GraphGenerator SetRewireP(double p)
        {
            this.RewireP = p;
            this.SetGeneratePath();
            return this;
        }

        protected override void SetGeneratePath()
        {
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "watts_strogatz_graph.py";
            this.GeneratePath = path + " " + this.NodeSize + " " + this.K + " " + this.RewireP;
        }
    }
}
