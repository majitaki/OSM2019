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
        int NodeNum;
        int K;
        double RewireP;
        public override GraphEnum MyGraphEnum { get; }
        protected override string GeneratePath { get; }

        public ConnectedWS_GraphGenerator(int n, int k, double p)
        {
            this.NodeNum = n;
            this.K = k;
            this.RewireP = p;
            this.MyGraphEnum = GraphEnum.ConnectedWS;
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "connected_watts_strogatz_graph.py";
            this.GeneratePath = path + " " + this.NodeNum + " " + this.K + " " + this.RewireP;
        }
    }
}
