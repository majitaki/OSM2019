using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;
using OSM2019.Experiment;
using OSM2019.GUI;
using OSM2019.OSM;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSM2019
{
    public partial class MainForm : Form
    {
        internal GUIEnum MyGUIEnum;

        List<UserControl> GUI_List;
        internal GraphGUI MyGraphGUI;
        internal AgentGUI MyAgentGUI;
        internal LearningGUI MyLearningGUI;
        internal ExperimentGUI MyExperimentGUI;
        internal AnimationForm MyAnimationForm;
        I_OSM MyOSM;


        public MainForm()
        {
            this.MyGUIEnum = GUIEnum.MainFormGUI;
            this.InitializeGUIs();
            InitializeComponent();
            this.UserInitialize();
            this.MyAnimationForm = new AnimationForm();
            //Test();
            TestExp();
            this.MyAnimationForm.Show();
            this.MyAnimationForm.Left = this.Right;
        }

        void TestExp()
        {
            //dim, rate
            //2, 0.62
            //3, 0.5
            //4, 0.45
            //5, 0.42
            //10. 0.35

            //Parallel.For(0, 5, seed =>
            //{
            //    new Weight_Experiment()
            //    .SetGraphs(new List<GraphEnum>() { GraphEnum.WS, GraphEnum.BA, GraphEnum.Hexagonal, GraphEnum.Grid2D })
            //    .SetAlgos(new List<AlgoEnum>() { AlgoEnum.OSMonly })
            //    .SetNetworkSize(300, 300, 100)
            //    .SetDimSize(10).SetSensorRate(0.1)
            //    .SetSensorCommonWeight(0.70)
            //    .SetSensorSizeRate(0.1)
            //    //.SetSensorFixSize(10)
            //    .SetWeights(Enumerable.Range(0, 50).Select(i => i / 50.0).ToList())
            //    .SetLogFolder("dim10_rate_commonwight_equal")
            //    .SetRounds(300)
            //    .SetSteps(1500)
            //    .Run(seed);
            //});



            //Parallel.For(0, 5, seed =>
            //{
            //    new TargetH_Experiment()
            //    .SetGraphs(new List<GraphEnum>() { GraphEnum.WS, GraphEnum.BA, GraphEnum.Grid2D })
            //    .SetAlgos(new List<AlgoEnum>() { AlgoEnum.AATfix })
            //    .SetNetworkSize(300, 300, 100)
            //    .SetDimSize(10).SetSensorRate(0.10)
            //    .SetSensorCommonWeight(0.70)
            //    .SetSensorSizeRate(0.1)
            //    //.SetSensorFixSize(10)
            //    .SetTargetHs(Enumerable.Range(80, 21).Select(i => i / 100.0).ToList())
            //    .SetLogFolder("dim10_target_h_sizerate_manygraph_equal")
            //    .SetRounds(300)
            //    .SetSteps(1500)
            //    .Run(seed);
            //});


            Parallel.For(0, 5, seed =>
            {
                new Normal_Experiment()
                .SetGraphs(new List<GraphEnum>() { GraphEnum.WS })
                .SetAlgos(new List<AlgoEnum>() { AlgoEnum.AATfix, AlgoEnum.IWTori })
                .SetNetworkSize(100, 500, 100)
                .SetDimSize(5).SetSensorRate(0.42)
                .SetSensorCommonWeight(0.70)
                .SetCommonCuriocity(1.0)
                .SetTargetHs(0.9)
                .SetSensorSizeRate(0.1)
                .SetLogFolder("dim5_cc0.0")
                .SetRounds(300)
                .SetSteps(1500)
                .Run(seed);
            });


            Environment.Exit(0);
        }

        void Test()
        {
            GraphGeneratorBase graph_generator;
            //graph_generator = new PC_GraphGenerator().SetNodeSize(500).SetRandomEdges(3).SetAddTriangleP(0.1);
            graph_generator = new WS_GraphGenerator().SetNodeSize(300).SetNearestNeighbors(6).SetRewireP(0.01);
            //graph_generator = new Grid2D_GraphGenerator().SetNodeSize(300);

            var pb = new ExtendProgressBar(100);
            var graph = graph_generator.Generate(0, pb);
            var layout = new KamadaKawai_LayoutGenerator(graph).Generate(pb);
            //var layout = new Circular_LayoutGenerator(graph).Generate();

            var init_belief_gene = new InitBeliefGenerator()
                                    .SetInitBeliefMode(mode: InitBeliefMode.NormalNarrow);

            var subject_tv = new OpinionSubject("good_tv", 3);
            var subject_company = new OpinionSubject("good_company", 2);
            var subject_test = new OpinionSubject("test", 5);

            double[] conv_array = { 1, 0, 0, 1, 1, 0 };
            var conv_matrix = Matrix<double>.Build.DenseOfColumnMajor(2, 3, conv_array);

            var osm_env = new OpinionEnvironment()
                            //.SetSubject(subject_tv)
                            .SetSubject(subject_test)
                            .SetCorrectDim(0)
                            .SetSensorRate(0.42);

            var subject_manager = new SubjectManager()
                                .AddSubject(subject_test)
                                .RegistConversionMatrix(subject_tv, subject_company, conv_matrix)
                                .SetEnvironment(osm_env);

            var op_form_threshold = 0.9;
            var sample_agent_1 = new SampleAgent()
                                .SetInitBeliefGene(init_belief_gene)
                                .SetThreshold(op_form_threshold)
                                .SetSubject(subject_tv)
                                .SetInitOpinion(Vector<double>.Build.Dense(3, 0.0));

            var sample_agent_2 = new SampleAgent()
                                .SetInitBeliefGene(init_belief_gene)
                                .SetThreshold(op_form_threshold)
                                .SetSubject(subject_company)
                                .SetInitOpinion(Vector<double>.Build.Dense(2, 0.0));

            var sample_agent_test = new SampleAgent()
                                .SetInitBeliefGene(init_belief_gene)
                                .SetThreshold(op_form_threshold)
                                .SetSubject(subject_test)
                                .SetInitOpinion(Vector<double>.Build.Dense(5, 0.0));

            var sensor_gene = new SensorGenerator()
                            .SetSensorSize((int)(0.1 * graph.Nodes.Count));
            //.SetSensorSize((int)10);

            int agent_gene_seed = 0;
            var agent_gene_rand = new ExtendRandom(agent_gene_seed);


            var agent_network = new AgentNetwork()
                                    .SetRand(agent_gene_rand)
                                    .GenerateNetworkFrame(graph)
                                    //.ApplySampleAgent(sample_agent_1, mode: SampleAgentSetMode.RandomSetRate, random_set_rate: 0.5)
                                    //.ApplySampleAgent(sample_agent_2, mode: SampleAgentSetMode.RemainSet)
                                    .ApplySampleAgent(sample_agent_test, mode: SampleAgentSetMode.RemainSet)
                                    .GenerateSensor(sensor_gene)
                                    .SetLayout(layout);


            int update_step_seed = 0;
            var update_step_rand = new ExtendRandom(update_step_seed);


            var osm = new IWTori_OSM();
            //var osm = new AATfix_OSM();
            //var osm = new OSM_Only(); 
            osm.SetTargetH(0.9);
            osm.SetCommonCuriocity(0.5);
            osm.SetRand(update_step_rand);
            osm.SetAgentNetwork(agent_network);
            osm.SetSubjectManager(subject_manager);
            osm.SetInitWeightsMode(mode: CalcWeightMode.FavorMyOpinion);
            osm.SetOpinionIntroInterval(10);
            osm.SetOpinionIntroRate(0.1);
            osm.SetSensorCommonWeight(0.70);
            //osm.SetCommonWeight(0.90);

            this.MyOSM = osm;
            this.MyAnimationForm.RegistOSM(osm);

            //osm.UpdateRounds(300, 1500);
            //var output_pass = Properties.Settings.Default.OutputLogPath + "/tmp";
            //Output.OutputRounds(output_pass, this.MyOSM.MyRecordRounds);
        }

        void UserInitialize()
        {
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            PythonProxy.StartUpPython();
            var working_folder_path = Properties.Settings.Default.WorkingFolderPath;
            if (!Directory.Exists(working_folder_path))
            {
                Directory.CreateDirectory(working_folder_path);
            }

            this.radioButtonGraphGUI.Checked = true;
            this.radioButtonRoundCheck.Checked = true;
            this.numericUpDownStepsControl.Value = 3000;
            this.numericUpDownSpeedControl.Value = 1;
            this.labelRoundNum.Text = 0.ToString();
            this.PlayStopFlag = true;

        }

        void InitializeGUIs()
        {
            this.GUI_List = new List<UserControl>();
            this.DoubleBuffered = true;

            this.MyGraphGUI = new GraphGUI();
            this.MyGraphGUI.Dock = DockStyle.Fill;
            this.MyGraphGUI.Name = "GraphGUI";
            this.Controls.Add(this.MyGraphGUI);
            this.GUI_List.Add(this.MyGraphGUI);
            this.MyGraphGUI.Visible = true;

            this.MyAgentGUI = new AgentGUI();
            this.MyAgentGUI.Dock = DockStyle.Fill;
            this.MyAgentGUI.Name = "AgentGUI";
            this.Controls.Add(this.MyAgentGUI);
            this.GUI_List.Add(this.MyAgentGUI);
            this.MyAgentGUI.Visible = false;

            this.MyLearningGUI = new LearningGUI();
            this.MyLearningGUI.Dock = DockStyle.Fill;
            this.MyLearningGUI.Name = "LearningGUI";
            this.Controls.Add(this.MyLearningGUI);
            this.GUI_List.Add(this.MyLearningGUI);
            this.MyLearningGUI.Visible = false;

            this.MyExperimentGUI = new ExperimentGUI();
            this.MyExperimentGUI.Dock = DockStyle.Fill;
            this.MyExperimentGUI.Name = "ExperimentGUI";
            this.Controls.Add(this.MyExperimentGUI);
            this.GUI_List.Add(this.MyExperimentGUI);
            this.MyExperimentGUI.Visible = false;
        }


        void SettingChanged(RadioButton b)
        {
            string name = b.Name;

            foreach (var setting in this.GUI_List)
            {
                setting.Visible = false;
            }

            switch (name)
            {
                case "radioButtonGraphGUI":
                    this.GUI_List.First(s => s.Name == "GraphGUI").Visible = true;
                    break;
                case "radioButtonAgentGUI":
                    this.GUI_List.First(s => s.Name == "AgentGUI").Visible = true;
                    break;
                case "radioButtonLearningGUI":
                    this.GUI_List.First(s => s.Name == "LearningGUI").Visible = true;
                    break;
                case "radioButtonExperimentGUI":
                    this.GUI_List.First(s => s.Name == "ExperimentGUI").Visible = true;
                    break;
                default:
                    break;
            }
        }

        #region Event

        private void radioButtonSetting_CheckedChanged(object sender, EventArgs e)
        {
            this.SettingChanged(sender as RadioButton);
        }

        private void checkBoxMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized) return;

            if (this.checkBoxMenu.Checked)
            {
                this.Height = 150;
            }
            else
            {
                this.Height = 800;
            }
        }

        #endregion

        void PlayStep()
        {
            var control_seed = (int)this.numericUpDownControlSeed.Value;
            var control_speed = (int)this.numericUpDownSpeedControl.Value;
            var max_steps = (int)this.numericUpDownStepsControl.Value;

            control_seed += int.Parse(this.labelStepNum.Text);
            control_seed += int.Parse(this.labelRoundNum.Text);

            this.MyOSM.UpdateSteps(control_speed);

            this.labelStepNum.Text = this.MyOSM.CurrentStep.ToString();
            this.labelRoundNum.Text = this.MyOSM.CurrentRound.ToString();
            this.MyAnimationForm.UpdatePictureBox();

            if (this.radioButtonRoundCheck.Checked)
            {
                if (max_steps <= this.MyOSM.CurrentStep)
                {
                    this.PlayRound();
                    this.MyOSM.PrintRoundInfo();
                    this.MyOSM.InitializeRound();
                }
            }

        }

        void PlayRound()
        {
            this.MyOSM.RecordRound();
            this.MyOSM.FinalizeRound();
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            this.PlayStep();
        }


        bool PlayStopFlag;
        void ChangePlayButton(bool turn_mode)
        {
            if (turn_mode)
            {
                this.PlayStopFlag = !this.PlayStopFlag;
            }

            if (!this.PlayStopFlag)
            {
                this.timerAnimation.Enabled = true;
                this.radioButtonPlay.Image = Properties.Resources.icon_pause;
            }
            else
            {
                this.timerAnimation.Enabled = false;
                this.radioButtonPlay.Image = Properties.Resources.icon_play;
            }

        }

        private void radioButtonPlay_Click(object sender, EventArgs e)
        {
            this.ChangePlayButton(true);
        }

        internal void PlayStop()
        {
            this.PlayStopFlag = true;
            this.ChangePlayButton(false);
            if (this.MyOSM == null) return;

            if (this.radioButtonStepCheck.Checked)
            {
                this.MyOSM.InitializeToFirstStep();
                this.labelStepNum.Text = this.MyOSM.CurrentStep.ToString();
            }
            else if (this.radioButtonRoundCheck.Checked)
            {
                this.MyOSM.InitializeToFirstRound();
                this.labelRoundNum.Text = this.MyOSM.CurrentRound.ToString();
                this.labelStepNum.Text = this.MyOSM.CurrentStep.ToString();

            }
            this.MyAnimationForm.UpdatePictureBox();
        }

        private void radioButtonPlayStop_Click(object sender, EventArgs e)
        {
            this.PlayStop();
        }

        private void radioButtonPlayStep_Click(object sender, EventArgs e)
        {
            this.PlayStep();
        }
    }
}
