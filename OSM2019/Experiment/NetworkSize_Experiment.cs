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
            for (int size = this.StartSize; size <= FinalSize; size += DurationSize)
            {
                GraphGeneratorBase graph_generator;
                graph_generator = new WS_GraphGenerator().SetNodeSize(size).SetNearestNeighbors(6).SetRewireP(0.01);
                //graph_generator = new Hexagonal_GraphGenerator().SetNodeSize(size);

                var graph = graph_generator.Generate(0);
                var layout = new Circular_LayoutGenerator(graph).Generate();

                var init_belief_gene = new InitBeliefGenerator()
                                        .SetInitBeliefMode(mode: InitBeliefMode.NormalNarrow);

                var subject_test = new OpinionSubject("test", 2);


                var subject_manager = new SubjectManager();


                var op_form_threshold = 0.9;

                var sample_agent_test = new SampleAgent()
                                    .SetInitBeliefGene(init_belief_gene)
                                    .SetThreshold(op_form_threshold)
                                    .SetSubject(subject_test)
                                    .SetInitOpinion(Vector<double>.Build.Dense(2, 0.0));

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
                    var update_step_rand = new ExtendRandom(update_step_seed);

                    var env_mgr = new EnvironmentManager()
                                    .SetSubject(subject_test)
                                    .SetCorrectDim(0)
                                    .SetSensorRate(0.55);


                    OSMBase<AAT_OSM> osm = new AAT_OSM()
                            .SetRand(update_step_rand)
                            .SetAgentNetwork(agent_network)
                            .SetEnvManager(env_mgr)
                            .SetSubjectManager(subject_manager)
                            //.SetInitWeightsMode(mode: CalcWeightMode.FavorMyOpinion)
                            .SetTargetH(0.9)
                            .SetOpinionIntroInterval(1)
                            .SetOpinionIntroRate(0.1);

                    osm.UpdateRounds(300, 1500);

                    var output_pass = Properties.Settings.Default.OutputLogPath + "/sintyoku_20181210/ws_" + size.ToString() + "_fix10_AAT";
                    Output.OutputRounds(output_pass, osm.MyRecordRounds, seed.ToString());


                    osm = new AATG_OSM()
                           .SetRand(update_step_rand)
                           .SetAgentNetwork(agent_network)
                           .SetEnvManager(env_mgr)
                           .SetSubjectManager(subject_manager)
                           .SetInitWeightsMode(mode: CalcWeightMode.FavorMyOpinion)
                           .SetOpinionIntroInterval(1)
                           .SetOpinionIntroRate(0.1);

                    osm.UpdateRounds(300, 1500);

                    //output_pass = Properties.Settings.Default.OutputLogPath + "/sintyoku_20181210/fix10_AATG";
                    output_pass = Properties.Settings.Default.OutputLogPath + "/sintyoku_20181210/ws_" + size.ToString() + "_fix10_AATG";
                    Output.OutputRounds(output_pass, osm.MyRecordRounds, seed.ToString());
                }
            }

        }
    }
}
