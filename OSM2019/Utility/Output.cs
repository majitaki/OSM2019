using CsvHelper;
using OSM2019.OSM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Utility
{
    static class Output
    {
        static public void OutputSteps(string pass, Dictionary<int, RecordStep> record_steps)
        {
            var dt = DateTime.Now;
            var dt_name = dt.ToString("yyyy-MM-dd-HH-mm-ss");

            var record_step_csvs = new List<RecordStepForCsv>();
            foreach (var myrecord in record_steps)
            {
                record_step_csvs.Add(new RecordStepForCsv(myrecord.Value));
            }

            using (var streamWriter = new StreamWriter(pass + "/" + dt_name + @"_steps.csv"))
            using (var csv_writer = new CsvWriter(streamWriter))
            {
                csv_writer.Configuration.HasHeaderRecord = true;
                csv_writer.Configuration.RegisterClassMap<RecordStepMapper>();
                csv_writer.WriteRecords(record_step_csvs);
            }
        }

        static public void OutputRounds(string pass, Dictionary<int, RecordRound> record_rounds, string tag = "")
        {
            SafeCreateDirectory(pass);

            var dt = DateTime.Now;
            var dt_name = dt.ToString("yyyy-MM-dd-HH-mm-ss");

            var record_round_csvs = new List<RecordRoundForCsv>();
            foreach (var myrecord in record_rounds)
            {
                record_round_csvs.Add(new RecordRoundForCsv(myrecord.Value));
            }

            using (var streamWriter = new StreamWriter(pass + "/" + dt_name + "_" + tag + @"_rounds.csv"))
            using (var csv_writer = new CsvWriter(streamWriter))
            {
                csv_writer.Configuration.HasHeaderRecord = true;
                csv_writer.Configuration.RegisterClassMap<RecordRoundMapper>();
                csv_writer.WriteRecords(record_round_csvs);
            }
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
