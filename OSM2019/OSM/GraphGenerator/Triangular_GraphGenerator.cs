﻿using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class Triangular_GraphGenerator : GraphGeneratorBase
    {
        int Height;
        int Width;
        public override GraphEnum MyGraphEnum { get; }
        public override string GeneratePath { get; protected set; }
        public override bool SeedEnable { get; protected set; }

        public Triangular_GraphGenerator()
        {
            this.MyGraphEnum = GraphEnum.Triangular;
            this.SeedEnable = false;
            this.SetGeneratePath();
        }

        public Triangular_GraphGenerator SetHeight(int height)
        {
            this.Height = height;
            this.SetGeneratePath();
            return this;
        }

        public Triangular_GraphGenerator SetWidth(int width)
        {
            this.Width = width;
            this.SetGeneratePath();
            return this;
        }

        public Triangular_GraphGenerator SetNodeSize(int n)
        {
            var r = (Math.Sqrt(1 + 8 * n) - 3) / 2;
            var upper = Math.Ceiling(r);
            var lower = Math.Floor(r);

            this.Height = (int)upper;
            this.Width = (int)lower;
            this.SetGeneratePath();
            return this;
        }

        protected override void SetGeneratePath()
        {
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "triangular_graph.py";
            this.GeneratePath = path + " " + this.Height + " " + this.Width;
        }
    }
}
