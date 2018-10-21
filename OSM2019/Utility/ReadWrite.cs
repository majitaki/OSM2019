using Newtonsoft.Json;
using OSM2019.OSM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Utility
{
    
    public class ReadWrite
    {
        public ReadWrite()
        {
            string filepath = @"graph.json";
            string jsonString = System.IO.File.ReadAllText(filepath, Encoding.UTF8);
            RawGraph raw_graph = JsonConvert.DeserializeObject<RawGraph>(jsonString);

        }

        static public void SafeCreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return;
            }
            Directory.CreateDirectory(path);
        }
    }
}
