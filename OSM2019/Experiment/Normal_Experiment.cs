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
    class Normal_Experiment
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
        bool SensorCommonWeightMode;
        bool CommonWeightMode;
        double CommonWeight;
        double TargetH;
        double CommonCuriocity;
        string LogFolder;
        int Rounds;
        int Steps;
        List<GraphEnum> MyGraphs;
        List<AlgoEnum> MyAlgos;

        static object lock_object = new object();

        public Normal_Experiment()
        {
            this.SensorCommonWeightMode = false;
            this.SensorSizeFixMode = false;
            this.CommonWeightMode = false;
            this.MyGraphs = new List<GraphEnum>();
            this.MyAlgos = new List<AlgoEnum>();
        }

        public Normal_Experiment SetAlgos(List<AlgoEnum> algos)
        {
            this.MyAlgos = algos;
            return this;
        }

        public Normal_Experiment SetGraphs(List<GraphEnum> graphs)
        {
            this.MyGraphs = graphs;
            return this;
        }

        public Normal_Experiment SetNetworkSize(int start_size, int final_size, int duration_size)
        {
            this.StartSize = start_size;
            this.FinalSize = final_size;
            this.DurationSize = duration_size;
            return this;
        }

        public Normal_Experiment SetDimSize(int dim_size)
        {
            this.DimSize = dim_size;
            return this;
        }

        public Normal_Experiment SetSensorRate(double sensor_rate)
        {
            this.SensorRate = sensor_rate;
            return this;
        }

        public Normal_Experiment SetLogFolder(string dt_name, string folder_name = "")
        {
            var sensor_size_comment = this.SensorSizeFixMode ? $"fix{this.SensorSize}" : $"rate{this.SensorSizeRate}";
            this.LogFolder = $"{dt_name}_{"nor"}_dim{this.DimSize}_sr{this.SensorRate}_scw{this.SensorCommonWeight}_{sensor_size_comment}_th{this.TargetH}_cc{this.CommonCuriocity}_cw{this.CommonWeight}_r{this.Rounds}_s{this.Steps}_" + folder_name;
            return this;
        }

        public Normal_Experiment SetSensorCommonWeight(double sensor_common_weight)
        {
            this.SensorCommonWeightMode = true;
            this.SensorCommonWeight = sensor_common_weight;
            return this;
        }

        public Normal_Experiment SetSensorFixSize(int sensor_size)
        {
            this.SensorSizeFixMode = true;
            this.SensorSize = sensor_size;
            return this;
        }

        public Normal_Experiment SetSensorSizeRate(double sensor_size_rate)
        {
            this.SensorSizeFixMode = false;
            this.SensorSizeRate = sensor_size_rate;
            return this;
        }

        public Normal_Experiment SetCommonWeight(double common_weight)
        {
            this.CommonWeightMode = true;
            this.CommonWeight = common_weight;
            return this;
        }

        public Normal_Experiment SetTargetHs(double target_h)
        {
            this.TargetH = target_h;
            return this;
        }

        public Normal_Experiment SetCommonCuriocity(double common_curiocity)
        {
            this.CommonCuriocity = common_curiocity;
            return this;
        }

        public Normal_Experiment SetRounds(int rounds)
        {
            this.Rounds = rounds;
            return this;
        }

        public Normal_Experiment SetSteps(int steps)
        {
            this.Steps = steps;
            return this;
        }

        public void Run(int seed)
        {
            this.Run(seed, seed);
        }


        public void Run(int start_seed, int final_seed)
        {
            string save_folder = this.LogFolder;
            //List<GraphEnum> graphs = new List<GraphEnum>() { GraphEnum.WS, GraphEnum.BA, GraphEnum.Hexagonal, GraphEnum.Grid2D, GraphEnum.Triangular };
            //List<AlgoEnum> algos = new List<AlgoEnum>() { AlgoEnum.AAT, AlgoEnum.AATGfix };
            var graphs = this.MyGraphs;
            var algos = this.MyAlgos;
            //List<AlgoEnum> algos = new List<AlgoEnum>() { AlgoEnum.AAT };

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
                            max++;
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


                            switch (algo)
                            {
                                case AlgoEnum.AAT:
                                    var osm_aat = new AAT_OSM();
                                    osm_aat.SetTargetH(this.TargetH);
                                    osm = osm_aat;
                                    break;
                                case AlgoEnum.AATG:
                                    var osm_aatg = new AATG_OSM();
                                    osm_aatg.SetTargetH(this.TargetH);
                                    osm = osm_aatg;
                                    break;
                                case AlgoEnum.AATfix:
                                    var osm_aatfix = new AATfix_OSM();
                                    osm_aatfix.SetTargetH(this.TargetH);
                                    osm = osm_aatfix;
                                    break;
                                case AlgoEnum.OSMonly:
                                    var osm_only = new OSM_Only();
                                    if (this.CommonWeightMode) osm_only.SetCommonWeight(this.CommonWeight);
                                    osm = osm_only;
                                    break;
                                case AlgoEnum.IWTori:
                                    var osm_iwtori = new IWTori_OSM();
                                    osm_iwtori.SetCommonCuriocity(this.CommonCuriocity);
                                    osm_iwtori.SetTargetH(this.TargetH);
                                    osm = osm_iwtori;
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
                                + $"_{this.Steps}";

                            Output.OutputRounds(output_pass, osm.MyRecordRounds, seed.ToString());
                            pb.Next();
                        }



                    }



                }
            }

        }
    }
}
