using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    public class Node
    {
        public int ID { get; set; }
    }

    public class Edge
    {
        public int Source { get; set; }
        public int Target { get; set; }
    }

    public class RawGraph
    {
        public bool Directed { get; set; }
        public bool Multigraph { get; set; }
        //public string Graph { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        internal GraphEnum MyGraphEnum { get; set; }
    }
}
