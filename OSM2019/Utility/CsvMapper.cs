using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using OSM2019.OSM;

namespace OSM2019.Utility
{
    class LayoutCsvMapper : ClassMap<Vector2>
    {
        public LayoutCsvMapper()
        {
            Map(_ => _.X).Name("x");
            Map(_ => _.Y).Name("y");
        }
    }

    class RecordStepForCsv
    {
        public int step;
        public int correct;
        public int incorrect;
        public int undeter;
        public int active_agents;
        public int active_sensors;
        public int messages;
        public double correct_rate;
        public double incorrect_rate;
        public double undeter_rate;

        public RecordStepForCsv(RecordStep record_step)
        {
            this.step = record_step.Step;
            //this.correct = record_step.CorrectSize;
            //this.incorrect = record_step.IncorrectSize;
            //this.undeter = record_step.UndeterSize;
            //this.active_agents = record_step.ActiveAgentSize;
            //this.active_sensors = record_step.ActiveSensorSize;
            //this.messages = record_step.StepMessageSize;
            //this.correct_rate = Math.Round(this.correct / (double)record_step.NetworkSize, 4);
            //this.incorrect_rate = Math.Round(this.incorrect / (double)record_step.NetworkSize, 4);
            //this.undeter_rate = Math.Round(this.undeter / (double)record_step.NetworkSize, 4);
        }
    }

    class RecordStepMapper : ClassMap<RecordStepForCsv>
    {
        public RecordStepMapper()
        {
            Map(_ => _.step).Name("step");
            Map(_ => _.correct).Name("correct");
            Map(_ => _.incorrect).Name("incorrect");
            Map(_ => _.undeter).Name("undeter");
            Map(_ => _.active_agents).Name("active_agents");
            Map(_ => _.active_sensors).Name("active_sensors");
            Map(_ => _.messages).Name("messages");
            Map(_ => _.correct_rate).Name("correct_rate");
            Map(_ => _.incorrect_rate).Name("incorrect_rate");
            Map(_ => _.undeter_rate).Name("undeter_rate");
        }
    }

    class RecordRoundForCsv
    {
        public int round;
        public int correct;
        public int incorrect;
        public int undeter;
        public double correct_rate;
        public double incorrect_rate;
        public double undeter_rate;
        //public Dictionary<OpinionSubject, List<int>> opinion_sizes { get; private set; }
        public double determined_sensor_rate;

        public RecordRoundForCsv(RecordRound record_round)
        {
            this.round = record_round.Round;
            this.correct = record_round.CorrectSize;
            this.incorrect = record_round.IncorrectSize;
            this.undeter = record_round.UndeterSize;
            //this.opinion_sizes = record_round.OpinionSizes;
            this.correct_rate = Math.Round(this.correct / (double)record_round.NetworkSize, 4);
            this.incorrect_rate = Math.Round(this.incorrect / (double)record_round.NetworkSize, 4);
            this.undeter_rate = Math.Round(this.undeter / (double)record_round.NetworkSize, 4);
            this.determined_sensor_rate = Math.Round(record_round.DeterminedSensorSize / (double)record_round.SensorSize, 4);
        }
    }


    class RecordRoundMapper : ClassMap<RecordRoundForCsv>
    {
        public RecordRoundMapper()
        {
            Map(_ => _.round).Name("round");
            Map(_ => _.correct).Name("correct");
            Map(_ => _.incorrect).Name("incorrect");
            Map(_ => _.undeter).Name("undeter");
            Map(_ => _.correct_rate).Name("correct_rate");
            Map(_ => _.incorrect_rate).Name("incorrect_rate");
            Map(_ => _.undeter_rate).Name("undeter_rate");
            Map(_ => _.determined_sensor_rate).Name("determined_sensor_rate");
        }
    }

}
