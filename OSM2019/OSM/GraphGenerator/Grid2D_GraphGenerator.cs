using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class Grid2D_GraphGenerator : GraphGeneratorBase
    {
        int Height;
        int Width;
        public override GraphEnum MyGraphEnum { get; }
        public override string GeneratePath { get; protected set; }
        public override bool SeedEnable { get; protected set; }

        public Grid2D_GraphGenerator()
        {
            this.MyGraphEnum = GraphEnum.Grid2D;
            this.SeedEnable = false;
            this.SetGeneratePath();
        }

        public Grid2D_GraphGenerator SetHeight(int height)
        {
            this.Height = height;
            this.SetGeneratePath();
            return this;
        }

        public Grid2D_GraphGenerator SetWidth(int width)
        {
            this.Width = width;
            this.SetGeneratePath();
            return this;
        }

        public Grid2D_GraphGenerator SetNodeSize(int n)
        {
            var upper = Math.Ceiling(Math.Sqrt(n));
            var lower = Math.Floor(Math.Sqrt(n));

            this.Height = (int)upper;
            this.Width = (int)lower;
            this.SetGeneratePath();
            return this;
        }

        protected override void SetGeneratePath()
        {
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "grid_2d_graph.py";
            this.GeneratePath = path + " " + this.Height + " " + this.Width;
        }
    }
}
