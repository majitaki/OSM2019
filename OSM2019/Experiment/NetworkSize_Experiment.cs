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
    class NetworkSize_Experiment
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
        double CommonWeight;
        string LogFolder;
        int Rounds;
        int Steps;

        static object lock_object = new object();

        public NetworkSize_Experiment()
        {
            this.SensorCommonWeightMode = false;
            this.SensorSizeFixMode = false;
        }

        public NetworkSize_Experiment SetNetworkSize(int start_size, int final_size, int duration_size)
        {
            this.StartSize = start_size;
            this.FinalSize = final_size;
            this.DurationSize = duration_size;
            return this;
        }

        public NetworkSize_Experiment SetDimSize(int dim_size)
        {
            this.DimSize = dim_size;
            return this;
        }

        public NetworkSize_Experiment SetSensorRate(double sensor_rate)
        {
            this.SensorRate = sensor_rate;
            return this;
        }

        public NetworkSize_Experiment SetLogFolder(string folder_name)
        {
            var dt = DateTime.Now;
            var dt_name = dt.ToString("yyyy-MMdd-HHmm");
            this.LogFolder = $"{dt_name}_" + folder_name;
            return this;
        }

        public NetworkSize_Experiment SetSensorCommonWeight(double sensor_common_weight)
        {
            this.SensorCommonWeightMode = true;
            this.SensorCommonWeight = sensor_common_weight;
            return this;
        }

        public NetworkSize_Experiment SetSensorFixSize(int sensor_size)
        {
            this.SensorSizeFixMode = true;
            this.SensorSize = sensor_size;
            return this;
        }

        public NetworkSize_Experiment SetSensorSizeRate(double sensor_size_rate)
        {
            this.SensorSizeFixMode = false;
            this.SensorSizeRate = sensor_size_rate;
            return this;
        }

        public NetworkSize_Experiment SetCommonWeight(double common_weight)
        {
            this.CommonWeight = common_weight;
            return this;
        }

        public NetworkSize_Experiment SetRounds(int rounds)
        {
            this.Rounds = rounds;
            return this;
        }

        public NetworkSize_Experiment SetSteps(int steps)
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
            List<GraphEnum> graphs = new List<GraphEnum>() { GraphEnum.WS, GraphEnum.BA, GraphEnum.Grid2D, GraphEnum.Hexagonal };
            //List<AlgoEnum> algos = new List<AlgoEnum>() { AlgoEnum.AAT, AlgoEnum.AATGfix };
            List<AlgoEnum> algos = new List<AlgoEnum>() { AlgoEnum.AATGfix };

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

                        var subject_manager = new SubjectManager().SetEnvironment(osm_env);


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
                            I_OSM osm = new OSM_Only();
                            switch (algo)
                            {
                                case AlgoEnum.AAT:

                                    var osm_aat = new AAT_OSM();
                                    var update_step_rand_aat = new ExtendRandom(update_step_seed);
                                    osm_aat.SetRand(update_step_rand_aat);
                                    osm_aat.SetAgentNetwork(agent_network);
                                    osm_aat.SetSubjectManager(subject_manager);
                                    osm_aat.SetInitWeightsMode(mode: CalcWeightMode.FavorMyOpinion);
                                    osm_aat.SetTargetH(0.9);
                                    osm_aat.SetOpinionIntroInterval(1);
                                    osm_aat.SetOpinionIntroRate(0.1);
                                    if (this.SensorCommonWeightMode) osm_aat.SetSensorCommonWeight(this.SensorCommonWeight);

                                    osm = osm_aat;
                                    break;
                                case AlgoEnum.AATG:

                                    var osm_aatg = new AATG_OSM();
                                    var update_step_rand_aatg = new ExtendRandom(update_step_seed);
                                    osm_aatg.SetRand(update_step_rand_aatg);
                                    osm_aatg.SetAgentNetwork(agent_network);
                                    osm_aatg.SetSubjectManager(subject_manager);
                                    osm_aatg.SetInitWeightsMode(mode: CalcWeightMode.FavorMyOpinion);
                                    osm_aatg.SetOpinionIntroInterval(1);
                                    osm_aatg.SetOpinionIntroRate(0.1);
                                    if (this.SensorCommonWeightMode) osm_aatg.SetSensorCommonWeight(this.SensorCommonWeight);

                                    osm = osm_aatg;
                                    break;
                                case AlgoEnum.AATGfix:

                                    var osm_aatgfix = new AATGfix_OSM();
                                    var update_step_rand_aatgfix = new ExtendRandom(update_step_seed);
                                    osm_aatgfix.SetRand(update_step_rand_aatgfix);
                                    osm_aatgfix.SetAgentNetwork(agent_network);
                                    osm_aatgfix.SetSubjectManager(subject_manager);
                                    osm_aatgfix.SetInitWeightsMode(mode: CalcWeightMode.FavorMyOpinion);
                                    osm_aatgfix.SetOpinionIntroInterval(1);
                                    osm_aatgfix.SetOpinionIntroRate(0.1);
                                    if (this.SensorCommonWeightMode) osm_aatgfix.SetSensorCommonWeight(this.SensorCommonWeight);

                                    osm = osm_aatgfix;
                                    break;
                                case AlgoEnum.OSMonly:
                                    var osm_only = new OSM_Only();
                                    var update_step_rand_osmonly = new ExtendRandom(update_step_seed);
                                    osm_only.SetRand(update_step_rand_osmonly);
                                    osm_only.SetAgentNetwork(agent_network);
                                    osm_only.SetSubjectManager(subject_manager);
                                    osm_only.SetInitWeightsMode(mode: CalcWeightMode.FavorMyOpinion);
                                    osm_only.SetOpinionIntroInterval(1);
                                    osm_only.SetOpinionIntroRate(0.1);
                                    osm_only.SetCommonWeight(this.CommonWeight);
                                    if (this.SensorCommonWeightMode) osm_only.SetSensorCommonWeight(this.SensorCommonWeight);

                                    osm = osm_only;
                                    break;
                                default:
                                    break;
                            }


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
