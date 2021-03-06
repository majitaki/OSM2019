﻿using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class Hexagonal_GraphGenerator : GraphGeneratorBase
    {
        int Height;
        int Width;
        public override GraphEnum MyGraphEnum { get; }
        public override string GeneratePath { get; protected set; }
        public override bool SeedEnable { get; protected set; }

        public Hexagonal_GraphGenerator()
        {
            this.MyGraphEnum = GraphEnum.Hexagonal;
            this.SeedEnable = false;
            this.SetGeneratePath();
        }

        protected override void SetGeneratePath()
        {
            var path = Properties.Settings.Default.GraphGeneratorFolderPath + "hexagonal_lattice_graph.py";
            this.GeneratePath = path + " " + this.Height + " " + this.Width;
        }

        public Hexagonal_GraphGenerator SetHeight(int height)
        {
            this.Height = height;
            this.SetGeneratePath();
            return this;
        }

        public Hexagonal_GraphGenerator SetWidth(int width)
        {
            this.Width = width;
            this.SetGeneratePath();
            return this;
        }

        public Hexagonal_GraphGenerator SetNodeSize(int n)
        {
            var upper = Math.Ceiling(Math.Sqrt(1 + (double)n / 2) - 1);
            var lower = Math.Floor(Math.Sqrt(1 + (double)n / 2) - 1);

            this.Height = (int)upper;
            this.Width = (int)lower;
            this.SetGeneratePath();
            return this;
        }
    }
}
