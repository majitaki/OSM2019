using Newtonsoft.Json;
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

    public class Link
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
        public List<Link> Links { get; set; }
        internal GraphEnum MyGraphEnum { get; set; }

        public void OutputGraphJSON()
        {
            var working_path = Properties.Settings.Default.WorkingFolderPath;
            string graph_filepath = Properties.Settings.Default.TmpGraphFile;
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(working_path + graph_filepath, false, System.Text.Encoding.UTF8);
            sw.Write(json);
            sw.Close();
        }
    }
}
