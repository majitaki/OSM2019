using CsvHelper;
using Konsole;
using OSM2019.OSM;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    abstract class LayoutGeneratorBase
    {
        public abstract LayoutEnum MyLayoutEnum { get; }
        protected abstract string GeneratePath { get; }
        protected abstract RawGraph MyGraph { get; }

        public virtual Layout Generate(ExtendProgressBar pb_layout)
        {
            var state = 0;
            switch (state)
            {
                case 0:
                    var delete_success = this.DeleteLayout(pb_layout);
                    if (!delete_success) goto default;

                    delete_success = this.DeleteTmpGraphJSON(pb_layout);
                    if (!delete_success) goto default;

                    var python_success = this.PythonLayoutGenerate(this.MyGraph);
                    if (!python_success) goto default;

                    var layout = this.ReadLayout();
                    if (layout == null) goto default;

                    pb_layout.Refresh("success layout generate");
                    return layout;
                default:
                    pb_layout.Refresh($"failure graph generate");
                    return null;
            }
        }

        protected bool DeleteLayout(ExtendProgressBar pb_layout)
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
                    var layout = Properties.Settings.Default.LayoutFile;
                    var layout_flag = Properties.Settings.Default.LayoutFlag;
                    if (filePath.Contains(layout) || filePath.Contains(layout_flag))
                    {
                        File.SetAttributes(filePath, FileAttributes.Normal);
                        File.Delete(filePath);
                    }

                }
                pb_layout.Refresh("delete json graph.");
                return true;
            }
            catch (Exception)
            {
                pb_layout.Refresh("fail to delete json graph");
                return false;
            }
        }

        protected bool DeleteTmpGraphJSON(ExtendProgressBar pb_layout)
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
                    var graph = Properties.Settings.Default.TmpGraphFile;
                    if (filePath.Contains(graph))
                    {
                        File.SetAttributes(filePath, FileAttributes.Normal);
                        File.Delete(filePath);
                    }

                }
                pb_layout.Refresh("delete tmp json");
                return true;
            }
            catch (Exception)
            {
                pb_layout.Refresh("fail to delete tmp json");
                return false;
            }
        }

        protected bool PythonLayoutGenerate(RawGraph graph)
        {
            graph.OutputGraphJSON();
            PythonProxy.ExecutePythonScript(this.GeneratePath);

            bool exist_flag = false;
            while (!exist_flag)
            {
                var working_path = Properties.Settings.Default.WorkingFolderPath;
                var layout_flag = Properties.Settings.Default.LayoutFlag;
                exist_flag = File.Exists(working_path + layout_flag);
                System.Threading.Thread.Sleep(100);
                if (PythonProxy.ErrorFlag)
                {
                    System.Threading.Thread.Sleep(100);
                    return false;
                }
            }
            return true;
        }

        protected Layout ReadLayout()
        {
            var working_path = Properties.Settings.Default.WorkingFolderPath;
            string layout_filepath = Properties.Settings.Default.LayoutFile;
            Layout layout = null;

            using (var streamReader = new StreamReader(working_path + layout_filepath))
            using (var csv = new CsvReader(streamReader))
            {
                csv.Configuration.RegisterClassMap<LayoutCsvMapper>();
                var vec_list = csv.GetRecords<Vector2>();
                layout = new Layout(vec_list.ToList(), this.MyLayoutEnum);
            }
            return layout;
        }


    }
}
