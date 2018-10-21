﻿using OSM2019.Abstracts;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class WS_GraphGenerator:A_GraphGenerator
    {
        int NodeNum;
        int K;
        double RewireP;
        public override GraphEnum MyGraphEnum { get; }
        protected override string GeneratePath { get; }

        public WS_GraphGenerator(int n, int k, double p)
        {
            this.NodeNum = n;
            this.K = k;
            this.RewireP = p;
            this.MyGraphEnum = GraphEnum.WS;
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "watts_strogatz_graph.py";
            this.GeneratePath = path + " " + this.NodeNum + " " + this.K + " " + this.RewireP;
        }

    }
}
