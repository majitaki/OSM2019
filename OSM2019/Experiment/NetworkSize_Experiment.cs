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

        public NetworkSize_Experiment(int start_size, int final_size, int duration_size)
        {
            this.StartSize = start_size;
            this.FinalSize = final_size;
            this.DurationSize = duration_size;
        }

        public void Run()
        {
            string save_folder = "/sintyoku_20181219_dim3/";
            //List<GraphEnum> graphs = new List<GraphEnum>() { GraphEnum.WS, GraphEnum.BA, GraphEnum.Hexagonal, GraphEnum.Grid2D, GraphEnum.Triangular };
            List<GraphEnum> graphs = new List<GraphEnum>() { GraphEnum.WS, GraphEnum.Hexagonal, GraphEnum.Triangular };
            List<AlgoEnum> algos = new List<AlgoEnum>() { AlgoEnum.AAT, AlgoEnum.AATG };

            int op_dim_size = 3;
            double sensor_rate = 0.55;

            for (int size = this.StartSize; size <= FinalSize; size += DurationSize)
            {
                GraphGeneratorBase graph_generator = new Null_GraphGenerator();

                foreach (var select_graph in graphs)
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


                    var graph = graph_generator.Generate(0);
                    var layout = new Circular_LayoutGenerator(graph).Generate();

                    var init_belief_gene = new InitBeliefGenerator()
                                            .SetInitBeliefMode(mode: InitBeliefMode.NormalNarrow);

                    var subject_test = new OpinionSubject("test", op_dim_size);


                    var osm_env = new OSM_Environment()
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

                    var sensor_gene = new SensorGenerator()
                                    //.SetSensorSize((int)(0.1 * graph.Nodes.Count));
                                    .SetSensorSize((int)10);

                    int agent_gene_seed = 0;
                    var agent_gene_rand = new ExtendRandom(agent_gene_seed);


                    var agent_network = new AgentNetwork()
                                            .SetRand(agent_gene_rand)
                                            .GenerateNetworkFrame(graph)
                                            .ApplySampleAgent(sample_agent_test, mode: SampleAgentSetMode.RemainSet)
                                            .GenerateSensor(sensor_gene)
                                            .SetLayout(layout);

                    for (int seed = 0; seed < 3; seed++)
                    {

                        int update_step_seed = seed;
                        var output_pass = Properties.Settings.Default.OutputLogPath + save_folder + select_graph.ToString() + "_" + size.ToString() + "_fix10_" + op_dim_size.ToString() + "_" + sensor_rate.ToString() + "_";

                        foreach (var algo in algos)
                        {
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

                                    osm_aat.UpdateRounds(300, 1500);

                                    var output_pass_aat = output_pass + algo.ToString();
                                    Output.OutputRounds(output_pass_aat, osm_aat.MyRecordRounds, seed.ToString());

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

                                    osm_aatg.UpdateRounds(300, 1500);

                                    var output_pass_aatg = output_pass + algo.ToString();
                                    Output.OutputRounds(output_pass_aatg, osm_aatg.MyRecordRounds, seed.ToString());

                                    break;
                                default:
                                    break;
                            }
                        }


                    }

                }
            }

        }
    }
}
