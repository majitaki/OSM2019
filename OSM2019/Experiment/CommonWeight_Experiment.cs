using Konsole;
using MathNet.Numerics.LinearAlgebra;
using OSM2019.OSM;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Experiment
{
    class CommonWeight_Experiment
    {
        int StartSize;
        int FinalSize;
        int DurationSize;
        int DimSize;
        double SensorRate;
        bool SensorSizeFixMode;
        int SensorSize;
        double SensorSizeRate;
        double SensorCommonWeight;
        double CommonCuriocity;
        double TargetH;
        bool SensorCommonWeightMode;
        string LogFolder;
        int Rounds;
        int Steps;
        List<GraphEnum> MyGraphs;
        List<AlgoEnum> MyAlgos;
        List<double> CommonWeights;

        static object lock_object = new object();

        public CommonWeight_Experiment()
        {
            this.SensorCommonWeightMode = false;
            this.SensorSizeFixMode = false;
            this.MyGraphs = new List<GraphEnum>();
            this.MyAlgos = new List<AlgoEnum>();
        }

        public CommonWeight_Experiment SetAlgos(List<AlgoEnum> algos)
        {
            this.MyAlgos = algos;
            return this;
        }

        public CommonWeight_Experiment SetGraphs(List<GraphEnum> graphs)
        {
            this.MyGraphs = graphs;
            return this;
        }

        public CommonWeight_Experiment SetNetworkSize(int start_size, int final_size, int duration_size)
        {
            this.StartSize = start_size;
            this.FinalSize = final_size;
            this.DurationSize = duration_size;
            return this;
        }

        public CommonWeight_Experiment SetDimSize(int dim_size)
        {
            this.DimSize = dim_size;
            return this;
        }

        public CommonWeight_Experiment SetSensorRate(double sensor_rate)
        {
            this.SensorRate = sensor_rate;
            return this;
        }

        public CommonWeight_Experiment SetLogFolder(string dt_name, string folder_name = "")
        {
            var sensor_size_comment = this.SensorSizeFixMode ? $"fix{this.SensorSize}" : $"rate{this.SensorSizeRate}";
            this.LogFolder = $"{dt_name}_{"cw"}_dim{this.DimSize}_sr{this.SensorRate}_scw{this.SensorCommonWeight}_{sensor_size_comment}_cc{this.CommonCuriocity}_r{this.Rounds}_s{this.Steps}" + folder_name;
            return this;
        }

        public CommonWeight_Experiment SetSensorCommonWeight(double sensor_common_weight)
        {
            this.SensorCommonWeightMode = true;
            this.SensorCommonWeight = sensor_common_weight;
            return this;
        }

        public CommonWeight_Experiment SetSensorFixSize(int sensor_size)
        {
            this.SensorSizeFixMode = true;
            this.SensorSize = sensor_size;
            return this;
        }

        public CommonWeight_Experiment SetSensorSizeRate(double sensor_size_rate)
        {
            this.SensorSizeFixMode = false;
            this.SensorSizeRate = sensor_size_rate;
            return this;
        }

        public CommonWeight_Experiment SetCommonCuriocity(double common_curiocity)
        {
            this.CommonCuriocity = common_curiocity;
            return this;
        }

        public CommonWeight_Experiment SetTargetHs(double target_h)
        {
            this.TargetH = target_h;
            return this;
        }

        public CommonWeight_Experiment SetRounds(int rounds)
        {
            this.Rounds = rounds;
            return this;
        }

        public CommonWeight_Experiment SetSteps(int steps)
        {
            this.Steps = steps;
            return this;
        }

        public CommonWeight_Experiment SetCommonWeights(List<double> weights)
        {
            this.CommonWeights = weights;
            return this;
        }

        public void Run(int seed)
        {
            this.Run(seed, seed);
        }


        public void Run(int start_seed, int final_seed)
        {
            string save_folder = this.LogFolder;
            var graphs = this.MyGraphs;
            var algos = this.MyAlgos;

            int op_dim_size = this.DimSize;
            double sensor_rate = this.SensorRate;


            int max = 0;
            for (int size = this.StartSize; size <= FinalSize; size += DurationSize)
            {
                foreach (var select_graph in graphs)
                {
                    for (int seed = start_seed; seed <= final_seed; seed++)
                    {
                        foreach (var algo in algos)
                        {
                            foreach (var weight in this.CommonWeights)
                            {
                                max++;
                            }
                        }
                    }
                }
            }

            var pb = new ExtendProgressBar(max);

            for (int size = this.StartSize; size <= FinalSize; size += DurationSize)
            {
                GraphGeneratorBase graph_generator = new Null_GraphGenerator();

                foreach (var select_graph in graphs)
                {
                    for (int seed = start_seed; seed <= final_seed; seed++)
                    {
                        switch (select_graph)
                        {
                            case GraphEnum.WS:
                                graph_generator = new WS_GraphGenerator().SetNodeSize(size).SetNearestNeighbors(6).SetRewireP(0.01);
                                break;
                            case GraphEnum.BA:
                                graph_generator = new BA_GraphGenerator().SetNodeSize(size).SetAttachEdges(2);
                                break;
                            case GraphEnum.Hexagonal:
                                graph_generator = new Hexagonal_GraphGenerator().SetNodeSize(size);
                                break;
                            case GraphEnum.Grid2D:
                                graph_generator = new Grid2D_GraphGenerator().SetNodeSize(size);
                                break;
                            case GraphEnum.Triangular:
                                graph_generator = new Triangular_GraphGenerator().SetNodeSize(size);
                                break;
                            default:
                                new Exception();
                                return;
                        }
                        var graph = new RawGraph();
                        var layout = new Layout();

                        lock (lock_object)
                        {
                            graph = graph_generator.Generate(seed, pb);
                            layout = new Circular_LayoutGenerator(graph).Generate(pb);
                        }


                        var init_belief_gene = new InitBeliefGenerator()
                                                .SetInitBeliefMode(mode: InitBeliefMode.NormalNarrow);

                        var subject_test = new OpinionSubject("test", op_dim_size);


                        var osm_env = new OpinionEnvironment()
                                        .SetSubject(subject_test)
                                        .SetCorrectDim(0)
                                        .SetSensorRate(sensor_rate);

                        var subject_manager = new SubjectManager()
                            .AddSubject(subject_test)
                            .SetEnvironment(osm_env);


                        var op_form_threshold = 0.9;

                        var sample_agent_test = new SampleAgent()
                                            .SetInitBeliefGene(init_belief_gene)
                                            .SetThreshold(op_form_threshold)
                                            .SetSubject(subject_test)
                                            .SetInitOpinion(Vector<double>.Build.Dense(op_dim_size, 0.0));

                        var sensor_gene = new SensorGenerator();
                        if (this.SensorSizeFixMode)
                        {
                            sensor_gene.SetSensorSize(this.SensorSize);
                        }
                        else
                        {
                            sensor_gene.SetSensorSize((int)(this.SensorSizeRate * graph.Nodes.Count));
                        }

                        int agent_gene_seed = seed;
                        var agent_gene_rand = new ExtendRandom(agent_gene_seed);


                        var agent_network = new AgentNetwork()
                                                .SetRand(agent_gene_rand)
                                                .GenerateNetworkFrame(graph)
                                                .ApplySampleAgent(sample_agent_test, mode: SampleAgentSetMode.RemainSet)
                                                .GenerateSensor(sensor_gene)
                                                .SetLayout(layout);

                        int update_step_seed = seed;

                        foreach (var algo in algos)
                        {
                            OSMBase osm = new OSM_Only();

                            foreach (var weight in this.CommonWeights)
                            {

                                switch (algo)
                                {
                                    case AlgoEnum.OSMonly:
                                        var osm_only = new OSM_Only();
                                        osm_only.SetCommonWeight(weight);
                                        osm = osm_only;
                                        break;
                                    case AlgoEnum.IWTorionly:
                                        var osm_iwtorionly = new IWTorionly_OSM();
                                        osm_iwtorionly.SetCommonWeight(weight);
                                        osm_iwtorionly.SetCommonCuriocity(this.CommonCuriocity);
                                        osm = osm_iwtorionly;
                                        break;
                                    default:
                                        break;
                                }

                                var update_step_rand_tmp = new ExtendRandom(update_step_seed);
                                osm.SetRand(update_step_rand_tmp);
                                osm.SetAgentNetwork(agent_network);
                                osm.SetSubjectManager(subject_manager);
                                osm.SetInitWeightsMode(mode: CalcWeightMode.FavorMyOpinion);
                                osm.SetOpinionIntroInterval(10);
                                osm.SetOpinionIntroRate(0.1);
                                osm.SimpleRecordFlag = true;
                                if (this.SensorCommonWeightMode) osm.SetSensorCommonWeight(this.SensorCommonWeight);


                                pb.Tag = $"{select_graph.ToString()} {size.ToString()} {algo.ToString()} {seed}";
                                osm.UpdateRounds(this.Rounds, this.Steps, pb);

                                string sensor_size_mode = "";
                                if (this.SensorSizeFixMode)
                                {
                                    sensor_size_mode = "fix" + this.SensorSize.ToString();
                                }
                                else
                                {
                                    sensor_size_mode = "rate" + Math.Round(this.SensorSizeRate, 3).ToString();
                                }

                                string sensor_weight_mode = "";
                                if (this.SensorCommonWeightMode)
                                {
                                    sensor_weight_mode = $"{this.SensorCommonWeight}";
                                }
                                else
                                {
                                    sensor_weight_mode = $"off";
                                }

                                var output_pass = Properties.Settings.Default.OutputLogPath
                                    + $"/{save_folder}/"
                                    + select_graph.ToString()
                                    + $"_{size.ToString()}"
                                    + $"_{sensor_size_mode}"
                                    + $"_{op_dim_size.ToString()}"
                                    + $"_{sensor_rate.ToString()}"
                                    + $"_{algo.ToString()}"
                                    + $"_{sensor_weight_mode}"
                                    + $"_{this.Rounds}"
                                    + $"_{this.Steps}"
                                    + $"_{weight}";

                                Output.OutputRounds(output_pass, osm.MyRecordRounds, seed.ToString());
                                pb.Next();
                            }

                        }



                    }



                }
            }

        }
    }
}
