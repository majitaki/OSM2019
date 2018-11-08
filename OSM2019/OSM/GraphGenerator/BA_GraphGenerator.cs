using OSM2019.Abstracts;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class BA_GraphGenerator : A_GraphGenerator
    {
        int NodeSize;
        int AttachEdges;
        public override GraphEnum MyGraphEnum { get; }
        public override string GeneratePath { get; protected set; }
        public override bool SeedEnable { get; protected set; }

        public BA_GraphGenerator()
        {
            this.MyGraphEnum = GraphEnum.BA;
            this.SeedEnable = true;
            this.SetGeneratePath();
        }

        public BA_GraphGenerator SetNodeSize(int n)
        {
            this.NodeSize = n;
            this.SetGeneratePath();
            return this;
        }

        public BA_GraphGenerator SetAttachEdges(int m)
        {
            this.AttachEdges = m;
            this.SetGeneratePath();
            return this;
        }

        protected override void SetGeneratePath()
        {
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "barabasi_albert_graph.py";
            this.GeneratePath = "" + path + " " + this.NodeSize + " " + this.AttachEdges;
        }
    }
}
