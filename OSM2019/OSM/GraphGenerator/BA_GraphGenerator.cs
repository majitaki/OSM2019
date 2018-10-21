using OSM2019.Abstracts;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class BA_GraphGenerator: A_GraphGenerator
    {
        int NodeNum;
        int M;
        public override GraphEnum MyGraphEnum { get; }
        protected override string GeneratePath { get; }

        public BA_GraphGenerator(int n, int m)
        {
            this.NodeNum = n;
            this.M = m;
            this.MyGraphEnum = GraphEnum.BA;
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "barabasi_albert_graph.py";
            this.GeneratePath = "" + path + " " + this.NodeNum + " " + this.M;
        }
    }
}
