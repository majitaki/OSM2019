using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Utility
{
    class PythonProxy
    {
        static System.Diagnostics.Process Process;
        static System.IO.StreamWriter StreamWriter;
        public static bool ErrorFlag;

        public static void StartUpPython()
        {
            Process = new System.Diagnostics.Process();
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardInput = true;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.RedirectStandardError = true;
            Process.OutputDataReceived += p_OutputDataReceived;
            Process.ErrorDataReceived += p_ErrorDataReceived;

            Process.StartInfo.FileName =
                System.Environment.GetEnvironmentVariable("ComSpec");
            Process.StartInfo.CreateNoWindow = true;

            Process.Start();

            Process.BeginOutputReadLine();
            Process.BeginErrorReadLine();
            StreamWriter = Process.StandardInput;
            if (StreamWriter.BaseStream.CanWrite)
            {
                StreamWriter.WriteLine(Properties.Settings.Default.AnacondaPath);
                //StreamWriter.WriteLine(@"activate " + Properties.Settings.Default.AnacondaEnv);
            }
        }

        public static void ExecutePythonScript(string args)
        {
            ErrorFlag = false;
            if (StreamWriter.BaseStream.CanWrite)
            {
                StreamWriter.WriteLine(@"python " + args);
            }
        }

        static void p_OutputDataReceived(object sender,
            System.Diagnostics.DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        static void p_ErrorDataReceived(object sender,
            System.Diagnostics.DataReceivedEventArgs e)
        {
            Console.WriteLine("ERR>{0}", e.Data);
            ErrorFlag = true;
        }
    }
}
