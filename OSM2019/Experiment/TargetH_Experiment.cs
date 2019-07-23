using Konsole;
using MathNet.Numerics.LinearAlgebra;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OSM2019.OSM;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Experiment
{
    class TargetH_Experiment
    {
        string ExperimentName;
        int StartSize;
        int FinalSize;
        int DurationSize;
        //int DimSize;
        double SensorRate;
        bool SensorSizeFixMode;
        int SensorSize;
        double SensorSizeRate;
        double SensorCommonWeight;
        double EnvDistWeight;
        BeliefUpdater MyBeliefUpdater;
        double CommonWeight;
        double CommonCuriocity;
        string LogFolder;
        int Rounds;
        int Steps;
        List<GraphEnum> MyGraphs;
        List<AlgoEnum> MyAlgos;
        List<int> MyDims;
        List<double> TargetHs;
        double OpinionThreshold;
        string SubjectName;
        public List<bool> IsDynamics;
        List<EnvDistributionEnum> EnvDisModes;

        static object lock_object = new object();

        public TargetH_Experiment()
        {
            //this.SensorCommonWeightMode = false;
            this.SensorSizeFixMode = false;
            this.MyGraphs = new List<GraphEnum>();
            this.MyAlgos = new List<AlgoEnum>();
            this.MyDims = new List<int>();
            this.IsDynamics = new List<bool>();
            this.ExperimentName = "TH";
            this.EnvDisModes = new List<EnvDistributionEnum>();
        }

        public TargetH_Experiment SetAlgos(List<AlgoEnum> algos)
        {
            this.MyAlgos = algos;
            return this;
        }

        public TargetH_Experiment SetDims(List<int> dims)
        {
            this.MyDims = dims;
            return this;
        }

        public TargetH_Experiment SetGraphs(List<GraphEnum> graphs)
        {
            this.MyGraphs = graphs;
            return this;
        }

        public TargetH_Experiment SetNetworkSize(int start_size, int final_size, int duration_size)
        {
            this.StartSize = start_size;
            this.FinalSize = final_size;
            this.DurationSize = duration_size;
            return this;
        }

        public TargetH_Experiment SetSensorRate(double sensor_rate)
        {
            this.SensorRate = sensor_rate;
            return this;
        }

        public TargetH_Experiment SetLogFolder(string dt_name, string folder_name = "")
        {
            var sensor_size_comment = this.SensorSizeFixMode ? $"fix{this.SensorSize}" : $"rate{this.SensorSizeRate}";
            //this.LogFolder = $"{dt_name}_{"th"}_dim{this.DimSize}_sr{this.SensorRate}_scw{this.SensorCommonWeight}_{sensor_size_comment}_cc{this.CommonCuriocity}_r{this.Rounds}_s{this.Steps}_" + folder_name;
            this.LogFolder = $"{dt_name}_{"th"}_" + folder_name;
            return this;
        }

        public TargetH_Experiment SetBeliefUpdater(BeliefUpdater belief_updater)
        {
            this.MyBeliefUpdater = belief_updater;
            return this;
        }

        public TargetH_Experiment SetSensorFixSize(int sensor_size)
        {
            this.SensorSizeFixMode = true;
            this.SensorSize = sensor_size;
            return this;
        }

        public TargetH_Experiment SetSensorSizeRate(double sensor_size_rate)
        {
            this.SensorSizeFixMode = false;
            this.SensorSizeRate = sensor_size_rate;
            return this;
        }

        public TargetH_Experiment SetCommonWeight(double common_weight)
        {
            this.CommonWeight = common_weight;
            return this;
        }
        public TargetH_Experiment SetSubjectName(string subject_name)
        {
            this.SubjectName = subject_name;
            return this;
        }

        public TargetH_Experiment SetEnvDistWeight(double dist_weight)
        {
            this.EnvDistWeight = dist_weight;
            return this;
        }
        public TargetH_Experiment SetCommonCuriocity(double common_curiocity)
        {
            this.CommonCuriocity = common_curiocity;
            return this;
        }

        public TargetH_Experiment SetRounds(int rounds)
        {
            this.Rounds = rounds;
            return this;
        }

        public TargetH_Experiment SetSteps(int steps)
        {
            this.Steps = steps;
            return this;
        }

        public TargetH_Experiment SetTargetHs(List<double> target_hs)
        {
            this.TargetHs = target_hs;
            return this;
        }

        public TargetH_Experiment SetOpinionThreshold(double op_threshold)
        {
            this.OpinionThreshold = op_threshold;
            return this;
        }
        public TargetH_Experiment SetDynamics(List<bool> is_dynamics)
        {
            this.IsDynamics = is_dynamics;
            return this;
        }

        public TargetH_Experiment SetEnvDistModes(List<EnvDistributionEnum> env_dist_modes)
        {
            this.EnvDisModes = env_dist_modes;
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
            var dims = this.MyDims;

            //int op_dim_size = this.DimSize;
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
                            foreach (var dim in dims)
                            {
                                foreach (var target_h in this.TargetHs)
                                {
                                    foreach (var is_dynamic in this.IsDynamics)
                                    {
                                        foreach (var env_dist_mode in this.EnvDisModes)
                                        {
                                            max++;
                                        }
                                    }
                                }
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
                    foreach (var op_dim_size in dims)
                    {
                        foreach (var is_diynamic in this.IsDynamics)
                        {
                            foreach (var env_dist_mode in this.EnvDisModes)
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

                                    var subject_test = new OpinionSubject(this.SubjectName, op_dim_size);

                                    var sample_agent_test = new SampleAgent()
                                                        .SetInitBeliefGene(init_belief_gene)
                                                        .SetThreshold(this.OpinionThreshold)
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
                                        foreach (var target_h in this.TargetHs)
                                        {
                                            var sample_size = 10;
                                            var awa_rate_window = 20;
                                            var op_intro_interval = 10;
                                            var op_intro_rate = 0.1;
                                            var dinamic_interval = 25;

                                            switch (algo)
                                            {
                                                case AlgoEnum.AAT:
                                                    var osm_aat = new AAT_OSM();
                                                    osm_aat.SetTargetH(target_h);
                                                    osm = osm_aat;
                                                    break;
                                                case AlgoEnum.AATG:
                                                    var osm_aatg = new AATG_OSM();
                                                    osm_aatg.SetTargetH(target_h);
                                                    osm = osm_aatg;
                                                    break;
                                                case AlgoEnum.AATfix:
                                                    var osm_aatfix = new AATfix_OSM();
                                                    osm_aatfix.SetTargetH(target_h);
                                                    osm = osm_aatfix;
                                                    break;
                                                case AlgoEnum.IWTori:
                                                    var osm_iwtori = new IWTori_OSM();
                                                    osm_iwtori.SetCommonCuriocity(this.CommonCuriocity);
                                                    osm_iwtori.SetTargetH(target_h);
                                                    osm = osm_iwtori;
                                                    break;
                                                case AlgoEnum.AATparticle:
                                                    var osm_aatpar = new AATparticle_OSM();
                                                    osm_aatpar.SetSampleSize(sample_size);
                                                    osm_aatpar.SetTargetH(target_h);
                                                    osm = osm_aatpar;
                                                    break;
                                                case AlgoEnum.AATwindow:
                                                    var osm_window = new AATwindow_OSM();
                                                    osm_window.SetTargetH(target_h);
                                                    osm_window.SetAwaRateWindowSize(awa_rate_window);
                                                    osm = osm_window;
                                                    break;
                                                case AlgoEnum.AATwindowparticle:
                                                    var osm_window_particle = new AATwindow_particle_OSM();
                                                    osm_window_particle.SetTargetH(target_h);
                                                    osm_window_particle.SetAwaRateWindowSize(awa_rate_window);
                                                    osm_window_particle.SetSampleSize(sample_size);
                                                    osm = osm_window_particle;
                                                    break;
                                                case AlgoEnum.AATfunction:
                                                    var osm_function = new AATfunction_OSM();
                                                    osm_function.SetTargetH(target_h);
                                                    osm_function.SetAwaRateWindowSize(awa_rate_window);
                                                    osm = osm_function;
                                                    break;
                                                case AlgoEnum.AATfunctioniwt:
                                                    var osm_function_iwt = new AATfunction_iwt_OSM();
                                                    osm_function_iwt.SetTargetH(target_h);
                                                    osm_function_iwt.SetCommonCuriocity(this.CommonCuriocity);
                                                    osm_function_iwt.SetAwaRateWindowSize(awa_rate_window);
                                                    osm = osm_function_iwt;
                                                    break;
                                                default:
                                                    break;
                                            }

                                            var update_step_rand_tmp = new ExtendRandom(update_step_seed);
                                            var subject_mgr_dic = new Dictionary<int, SubjectManager>();
                                            if (is_diynamic)
                                            {
                                                for (int i = 0; i < 100; i++)
                                                {
                                                    subject_mgr_dic.Add(i * dinamic_interval, new SubjectManagerGenerator().Generate(subject_test, this.EnvDistWeight, i % op_dim_size, sensor_rate, env_dist_mode));
                                                }

                                            }
                                            else
                                            {
                                                subject_mgr_dic.Add(0, new SubjectManagerGenerator().Generate(subject_test, this.EnvDistWeight, 0, sensor_rate, env_dist_mode));
                                            }
                                            osm.SetSubjectManagerDic(subject_mgr_dic);
                                            osm.SetRand(update_step_rand_tmp);
                                            osm.SetAgentNetwork(agent_network);
                                            osm.SetInitWeightsMode(mode: CalcWeightMode.FavorMyOpinion);
                                            osm.SetOpinionIntroInterval(op_intro_interval);
                                            osm.SetOpinionIntroRate(op_intro_rate);
                                            osm.SimpleRecordFlag = true;
                                            osm.SetBeliefUpdater(this.MyBeliefUpdater);

                                            pb.Tag = $"{select_graph.ToString()} {size.ToString()} {algo.ToString()} {op_dim_size} {seed}";
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

                                            string sensor_weight_mode = $"{this.MyBeliefUpdater.SensorWeightMode}";

                                            var exp_setting = new ExperimentSetting();
                                            exp_setting.Algorithm = algo.ToString();
                                            exp_setting.AwaRateWindowSize = awa_rate_window;
                                            exp_setting.BeliefUpdater = this.MyBeliefUpdater.MyBeliefUpdateFunctionMode.ToString();
                                            exp_setting.CommonCuriocity = this.CommonCuriocity;
                                            exp_setting.Dim = op_dim_size;
                                            exp_setting.EnvDistWeight = this.EnvDistWeight;
                                            exp_setting.EnvDistMode = env_dist_mode.ToString();
                                            exp_setting.GraphType = select_graph.ToString();
                                            exp_setting.IsDynamic = is_diynamic;
                                            exp_setting.NetworkSize = size;
                                            exp_setting.OpinionIntroInteval = op_intro_interval;
                                            exp_setting.OpinionIntroRate = op_intro_rate;
                                            exp_setting.Round = this.Rounds;
                                            exp_setting.SampleSize = sample_size;
                                            exp_setting.SensorRate = sensor_rate;
                                            exp_setting.SensorSize = sensor_gene.SensorSize;
                                            exp_setting.SensorSizeMode = this.SensorSizeFixMode.ToString();
                                            exp_setting.SensorWeightMode = sensor_weight_mode;
                                            exp_setting.Step = this.Steps;
                                            exp_setting.TargetAwareness = target_h;

                                            var json_exp_each_setting = JsonConvert.SerializeObject(exp_setting, Formatting.Indented);

                                            var output_pass = Properties.Settings.Default.OutputLogPath
                                                + $"/{save_folder}/data/"
                                                + select_graph.ToString()
                                                + $"_{size.ToString()}"
                                                + $"_{algo.ToString()}"
                                                + $"_{op_dim_size}"
                                                + $"_{is_diynamic}"
                                                + $"_{target_h}"
                                                + $"_{env_dist_mode.ToString()}"
                                                + $"_";

                                            //Output.OutputRounds(output_pass, osm.MyRecordRounds, seed.ToString());
                                            Output.OutputRounds(output_pass, osm.MyRecordRounds, json_exp_each_setting, seed.ToString());
                                            pb.Next();
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }

        }
    }
}
