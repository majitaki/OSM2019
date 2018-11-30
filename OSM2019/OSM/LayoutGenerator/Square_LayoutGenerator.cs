using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using OSM2019.Utility;

namespace OSM2019.OSM
{
    class Square_LayoutGenerator : LayoutGeneratorBase
    {
        public override LayoutEnum MyLayoutEnum { get; }
        protected override string GeneratePath { get; }
        protected override RawGraph MyGraph { get; }

        public Square_LayoutGenerator(RawGraph graph)
        {
            this.MyGraph = graph;
            this.MyLayoutEnum = LayoutEnum.Square;
        }

        public override Layout Generate()
        {
            var state = 0;
            switch (state)
            {
                case 0:
                    Console.WriteLine("-----");
                    Console.WriteLine("ok Start Layout Generation");
                    var delete_success = this.DeleteLayout();
                    if (!delete_success) goto default;

                    delete_success = this.DeleteTmpGraphJSON();
                    if (!delete_success) goto default;

                    var layout = this.GetSquareLayout();
                    if (layout == null) goto default;

                    Console.WriteLine("ok Success Layout Generation");
                    return layout;
                default:
                    Console.WriteLine("no Failure Layout Generation");
                    return null;
            }
        }

        Layout GetSquareLayout()
        {
            var pos_list = new List<Vector2>();
            int width_nodes = (int)Math.Round(Math.Sqrt(this.MyGraph.Nodes.Count));

            var width_count = 0;
            var height_count = 0;

            foreach (var node in this.MyGraph.Nodes)
            {
                if (width_count < width_nodes)
                {
                    width_count++;
                }
                else
                {
                    width_count = 1;
                    height_count++;
                }
                pos_list.Add(new Vector2(width_count, height_count));
            }
            return new Layout(pos_list, this.MyLayoutEnum);
        }

    }
}
