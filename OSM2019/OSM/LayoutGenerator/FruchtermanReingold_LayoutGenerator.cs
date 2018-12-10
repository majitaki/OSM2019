using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class FruchtermanReingold_LayoutGenerator : LayoutGeneratorBase
    {
        public override LayoutEnum MyLayoutEnum { get; }
        protected override string GeneratePath { get; }
        protected override RawGraph MyGraph { get; }

        public FruchtermanReingold_LayoutGenerator(RawGraph graph)
        {
            this.MyGraph = graph;
            this.MyLayoutEnum = LayoutEnum.FruchtermanReingold;
            var path = Properties.Settings.Default.LayoutGeneratorFolderPath + "fruchterman_reingold_layout.py";
            this.GeneratePath = path;
        }
    }
}
