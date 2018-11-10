using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class Random_LayoutGenerator : LayoutGeneratorBase
    {
        public override LayoutEnum MyLayoutEnum { get; }
        protected override string GeneratePath { get; }
        protected override RawGraph MyGraph { get; }

        public Random_LayoutGenerator(RawGraph graph)
        {
            this.MyGraph = graph;
            this.MyLayoutEnum = LayoutEnum.Random;
            var path = Properties.Settings.Default.LayoutGeneratorFolderPath + "random_layout.py";
            this.GeneratePath = path;
        }
    }
}
