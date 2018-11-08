using OSM2019.Abstracts;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class ConnectedWS_GraphGenerator : A_GraphGenerator
    {
        int NodeSize;
        int K;
        double RewireP;
        public override GraphEnum MyGraphEnum { get; }
        public override string GeneratePath { get; protected set; }
        public override bool SeedEnable { get; protected set; }

        public ConnectedWS_GraphGenerator()
        {
            this.MyGraphEnum = GraphEnum.ConnectedWS;
            this.SeedEnable = true;
            this.SetGeneratePath();
        }

        public ConnectedWS_GraphGenerator SetNodeSize(int n)
        {
            this.NodeSize = n;
            this.SetGeneratePath();
            return this;
        }

        public ConnectedWS_GraphGenerator SetNearestNeighbors(int k)
        {
            this.K = k;
            this.SetGeneratePath();
            return this;
        }

        public ConnectedWS_GraphGenerator SetRewireP(double p)
        {
            this.RewireP = p;
            this.SetGeneratePath();
            return this;
        }

        protected override void SetGeneratePath()
        {
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "connected_watts_strogatz_graph.py";
            this.GeneratePath = path + " " + this.NodeSize + " " + this.K + " " + this.RewireP;
        }
    }
}
