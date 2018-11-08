using Newtonsoft.Json;
using OSM2019.Interfaces;
using OSM2019.OSM;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Abstracts
{
    abstract class A_GraphGenerator : I_GraphGenerator
    {
        public abstract GraphEnum MyGraphEnum { get; }
        public abstract string GeneratePath { get; protected set; }
        public abstract bool SeedEnable { get; protected set; }

        protected abstract void SetGeneratePath();

        public RawGraph Generate(int graph_seed)
        {
            var working_folder_path = Properties.Settings.Default.WorkingFolderPath;

            var state = 0;
            switch (state)
            {
                case 0:
                    Console.WriteLine("-----");
                    Console.WriteLine("ok Start Graph Generation");
                    var delete_success = this.DeleteGraphJSON();
                    if (!delete_success) goto default;

                    var python_success = this.PythonGraphGenerate(graph_seed, this.SeedEnable);
                    if (!python_success) goto default;

                    var raw_graph = this.ReadJSON();
                    var graph_enum = this.MyGraphEnum;
                    raw_graph.MyGraphEnum = graph_enum;
                    if (raw_graph.Links == null || raw_graph.Nodes.Count == 0 || graph_enum == GraphEnum.Void) goto default;

                    Console.WriteLine("ok Load Raw Graph");
                    Console.WriteLine("ok Node: " + raw_graph.Nodes.Count);
                    Console.WriteLine("ok Edge: " + raw_graph.Links.Count);
                    Console.WriteLine("ok GraphEnum: " + graph_enum.ToString());
                    Console.WriteLine("ok Graph Seed: " + graph_seed);
                    Console.WriteLine("ok Success Graph Generation");
                    return raw_graph;

                default:
                    Console.WriteLine("no Failure Graph Generation");
                    return null;
            }
        }

        bool DeleteGraphJSON()
        {
            var path = Properties.Settings.Default.WorkingFolderPath;

            if (!Directory.Exists(path))
            {
                return true;
            }
            string[] filePaths = Directory.GetFiles(path);

            try
            {
                foreach (string filePath in filePaths)
                {
                    var graph = Properties.Settings.Default.RawGraphFile;
                    var graph_flag = Properties.Settings.Default.RawGraphFile;
                    if (filePath.Contains(graph) || filePath.Contains(graph_flag))
                    {
                        File.SetAttributes(filePath, FileAttributes.Normal);
                        File.Delete(filePath);
                    }

                }
                Console.WriteLine("ok Delete Graph JSON");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("no Failure Delete Graph JSON");
                return false;
            }
        }

        bool PythonGraphGenerate(int graph_seed, bool seed_enable)
        {
            if (seed_enable)
            {
                PythonProxy.ExecutePythonScript(this.GeneratePath + " " + graph_seed);
            }
            else
            {
                PythonProxy.ExecutePythonScript(this.GeneratePath);
            }
            bool exist_flag = false;
            while (!exist_flag)
            {
                var working_path = Properties.Settings.Default.WorkingFolderPath;
                var graph_flag = Properties.Settings.Default.RawGraphFile;
                exist_flag = File.Exists(working_path + graph_flag);
                System.Threading.Thread.Sleep(100);
                if (PythonProxy.ErrorFlag)
                {
                    System.Threading.Thread.Sleep(100);
                    return false;
                }
            }
            return true;
        }

        RawGraph ReadJSON()
        {
            var working_path = Properties.Settings.Default.WorkingFolderPath;
            string graph_filepath = Properties.Settings.Default.RawGraphFile;
            string jsonString = System.IO.File.ReadAllText(working_path + graph_filepath, Encoding.UTF8);
            RawGraph raw_graph = JsonConvert.DeserializeObject<RawGraph>(jsonString);
            return raw_graph;
        }
    }
}
