using OSM2019.Abstracts;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class ER_GraphGenerator : A_GraphGenerator
    {
        int NodeNum;
        double P;
        public override GraphEnum MyGraphEnum { get; }
        protected override string GeneratePath { get; }

        public ER_GraphGenerator(int n, double p)
        {
            this.NodeNum = n;
            this.P = p;
            this.MyGraphEnum = GraphEnum.ER;
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "erdos_renyi_graph.py";
            this.GeneratePath = path + " " + this.NodeNum + " " + this.P;
        }
    }
}
