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
        static public void OutputSteps(string pass, List<RecordRound> record_rounds, string tag = "")
        {
            foreach (var record_round in record_rounds)
            {
                var round_pass = pass + $"/{record_round.Round}";
                SafeCreateDirectory(round_pass);

                var keys = record_rounds.First().AllOpinionSizes.Values.SelectMany(value => value.Keys).Select(key => key.ToString());
                var headers = new[] {
                    "Step",
                    "CorrectRate",
                    "IncorrectRate",
                    "UndeterRate",
                    "ActiveSensorRate",
                    "ActiveAgentRate",
                    "DeterminedSensorRate",
                    "StepMessageSize",
                    "SensorSize",
                    "NetworkSize",
                    "FinalSteps",
                    "AverageWeight",
                    "VarWeight",
                    "SimpsonsD",
                    "BetterSimpsonD"
                     }.Concat(keys).ToList();


                using (var streamWriter = new StreamWriter(round_pass + "/" + tag + @"_steps.csv"))
                using (var csv_writer = new CsvWriter(streamWriter))
                {
                    //header
                    foreach (var header in headers)
                    {
                        csv_writer.WriteField(header);
                    }
                    csv_writer.NextRecord();

                    //step
                    string step = "";
                    string correct_rate = "";
                    string incorrect_rate = "";
                    string undeter_rate = "";
                    string active_sensor_count = "";
                    string active_agent_count = "";
                    string determined_sensor_rate = "";
                    string step_message_size = "";
                    string sensor_size = "";
                    string network_size = "";
                    string final_step = "";
                    string ave_weight = "";
                    string var_weight = "";
                    string simpsons_d = "";
                    string better_simpsons_d = "";

                    foreach (var step_num in record_round.FinalSteps)
                    {
                        step = step_num.ToString();
                        correct_rate = Math.Round(record_round.CorrectSizes[step_num] / (double)record_round.NetworkSizes[step_num], 4).ToString();
                        incorrect_rate = Math.Round(record_round.IncorrectSizes[step_num] / (double)record_round.NetworkSizes[step_num], 4).ToString();
                        undeter_rate = Math.Round(record_round.UndeterSizes[step_num] / (double)record_round.NetworkSizes[step_num], 4).ToString();
                        active_sensor_count = record_round.ActiveSensorSizes[step_num].ToString();
                        active_agent_count = record_round.ActiveAgentSizes[step_num].ToString();
                        determined_sensor_rate = Math.Round(record_round.DeterminedSensorSizes[step_num] / (double)record_round.SensorSizes[step_num], 4).ToString();
                        step_message_size = record_round.StepMessageSizes[step_num].ToString();
                        sensor_size = record_round.SensorSizes[step_num].ToString();
                        network_size = record_round.NetworkSizes[step_num].ToString();
                        final_step = record_round.FinalSteps[step_num].ToString();
                        ave_weight = record_round.AverageWeight[step_num].ToString();
                        var_weight = record_round.VarWeight[step_num].ToString();
                        simpsons_d = record_round.SimpsonsDs[step_num].ToString();
                        better_simpsons_d = (double.Parse(simpsons_d) * (1 - double.Parse(undeter_rate))).ToString();

                        csv_writer.WriteField(step);
                        csv_writer.WriteField(correct_rate);
                        csv_writer.WriteField(incorrect_rate);
                        csv_writer.WriteField(undeter_rate);
                        csv_writer.WriteField(active_sensor_count);
                        csv_writer.WriteField(active_agent_count);
                        csv_writer.WriteField(determined_sensor_rate);
                        csv_writer.WriteField(step_message_size);
                        csv_writer.WriteField(sensor_size);
                        csv_writer.WriteField(network_size);
                        csv_writer.WriteField(final_step);
                        csv_writer.WriteField(ave_weight);
                        csv_writer.WriteField(var_weight);
                        csv_writer.WriteField(simpsons_d);
                        csv_writer.WriteField(better_simpsons_d);

                        foreach (var subject in record_round.MySubjectManager.Subjects)
                        {
                            foreach (var dim in Enumerable.Range(0, subject.SubjectDimSize))
                            {
                                var each_dim_rate = Math.Round(record_round.AllOpinionSizes[subject][dim][step_num] / (double)record_round.NetworkSizes[step_num], 4).ToString();
                                csv_writer.WriteField(each_dim_rate);
                            }
                            //Console.WriteLine();
                        }

                        csv_writer.NextRecord();
                    }

                }
            }
        }

        static public void OutputRounds(string pass, List<RecordRound> record_rounds, string tag = "")
        {
            SafeCreateDirectory(pass);

            var dt = DateTime.Now;
            var dt_name = dt.ToString("yyyy-MM-dd-HH-mm-ss");


            var keys = record_rounds.First().AllOpinionSizes.Values.SelectMany(value => value.Keys).Select(key => key.ToString());
            var headers = new[] {
                    "Round",
                    "CorrectRate",
                    "IncorrectRate",
                    "UndeterRate",
                    "ActiveSensorRate",
                    "ActiveAgentRate",
                    "DeterminedSensorRate",
                    "StepMessageSize",
                    "SensorSize",
                    "NetworkSize",
                    "FinalSteps",
                    "AverageWeight",
                    "VarWeight",
                    "SimpsonsD",
                    "BetterSimpsonD"
                     }.Concat(keys).ToList();

            using (var streamWriter = new StreamWriter(pass + "/" + dt_name + "_" + tag + @"_rounds.csv"))
            using (var csv_writer = new CsvWriter(streamWriter))
            {
                //header
                foreach (var header in headers)
                {
                    csv_writer.WriteField(header);
                }
                csv_writer.NextRecord();

                //record
                string round = "";
                string correct_rate = "";
                string incorrect_rate = "";
                string undeter_rate = "";
                string active_sensor_count = "";
                string active_agent_count = "";
                string determined_sensor_rate = "";
                string step_message_size = "";
                string sensor_size = "";
                string network_size = "";
                string final_step = "";
                string ave_weight = "";
                string var_weight = "";
                string simpsons_d = "";
                string better_simpsons_d = "";

                foreach (var record_round in record_rounds)
                {
                    round = record_round.Round.ToString();
                    correct_rate = Math.Round(record_round.CorrectSizes.Last() / (double)record_round.NetworkSizes.Last(), 4).ToString();
                    incorrect_rate = Math.Round(record_round.IncorrectSizes.Last() / (double)record_round.NetworkSizes.Last(), 4).ToString();
                    undeter_rate = Math.Round(record_round.UndeterSizes.Last() / (double)record_round.NetworkSizes.Last(), 4).ToString();
                    active_sensor_count = record_round.ActiveSensorSizes.Sum().ToString();
                    active_agent_count = record_round.ActiveAgentSizes.Sum().ToString();
                    determined_sensor_rate = Math.Round(record_round.DeterminedSensorSizes.Last() / (double)record_round.SensorSizes.Last(), 4).ToString();
                    step_message_size = record_round.StepMessageSizes.Sum().ToString();
                    sensor_size = record_round.SensorSizes.Last().ToString();
                    network_size = record_round.NetworkSizes.Last().ToString();
                    final_step = record_round.FinalSteps.Last().ToString();
                    ave_weight = record_round.AverageWeight.Last().ToString();
                    var_weight = record_round.VarWeight.Last().ToString();
                    simpsons_d = record_round.SimpsonsDs.Last().ToString();
                    better_simpsons_d = (double.Parse(simpsons_d) * (1 - double.Parse(undeter_rate))).ToString();

                    csv_writer.WriteField(round);
                    csv_writer.WriteField(correct_rate);
                    csv_writer.WriteField(incorrect_rate);
                    csv_writer.WriteField(undeter_rate);
                    csv_writer.WriteField(active_sensor_count);
                    csv_writer.WriteField(active_agent_count);
                    csv_writer.WriteField(determined_sensor_rate);
                    csv_writer.WriteField(step_message_size);
                    csv_writer.WriteField(sensor_size);
                    csv_writer.WriteField(network_size);
                    csv_writer.WriteField(final_step);
                    csv_writer.WriteField(ave_weight);
                    csv_writer.WriteField(var_weight);
                    csv_writer.WriteField(simpsons_d);
                    csv_writer.WriteField(better_simpsons_d);

                    foreach (var subject in record_round.MySubjectManager.Subjects)
                    {
                        foreach (var dim in Enumerable.Range(0, subject.SubjectDimSize))
                        {
                            var each_dim_rate = Math.Round(record_round.AllOpinionSizes[subject][dim].Last() / (double)record_round.NetworkSizes.Last(), 4).ToString();
                            csv_writer.WriteField(each_dim_rate);
                        }
                        //Console.WriteLine();
                    }

                    csv_writer.NextRecord();
                }

            }

            //csv_writer.Configuration.HasHeaderRecord = true;
            //csv_writer.Configuration.RegisterClassMap<RecordRoundMapper>();
            //csv_writer.WriteRecords(record_round_csvs);
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
