using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;
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
            Test();
            this.MyAnimationForm.Show();
            this.MyAnimationForm.Left = this.Right;

        }

        void Test()
        {
            GraphGeneratorBase graph_generator;
            //graph_generator = new PC_GraphGenerator().SetNodeSize(500).SetRandomEdges(3).SetAddTriangleP(0.1);
            //graph_generator = new WS_GraphGenerator().SetNodeSize(300).SetNearestNeighbors(6).SetRewireP(0.05);
            graph_generator = new Grid2D_GraphGenerator().SetNodeSize(300);

            var graph = graph_generator.Generate(0);
            var layout = new KK_LayoutGenerator(graph).Generate();
            //var layout = new Circular_LayoutGenerator(graph).Generate();

            var init_belief_gene = new InitBeliefGenerator()
                                    .SetInitBeliefMode(mode: InitBeliefMode.NormalNarrow);

            var subject_tv = new OpinionSubject("good_tv", 3);
            var subject_company = new OpinionSubject("good_company", 2);
            var subject_test = new OpinionSubject("test", 5);

            double[] conv_array = { 1, 0, 0, 1, 1, 0 };
            var conv_matrix = Matrix<double>.Build.DenseOfColumnMajor(2, 3, conv_array);

            var subject_manager = new SubjectManager()
                .RegistConversionMatrix(subject_tv, subject_company, conv_matrix);


            var op_form_threshold = 0.9;
            var sample_agent_1 = new SampleAgent()
                                .SetInitBeliefGene(init_belief_gene)
                                .SetThreshold(op_form_threshold)
                                .SetSubject(subject_tv)
                                .SetInitOpinion(Matrix<double>.Build.Dense(3, 1, 0.0));


            var sample_agent_2 = new SampleAgent()
                                .SetInitBeliefGene(init_belief_gene)
                                .SetThreshold(op_form_threshold)
                                .SetSubject(subject_company)
                                .SetInitOpinion(Matrix<double>.Build.Dense(2, 1, 0.0));

            var sample_agent_test = new SampleAgent()
                                .SetInitBeliefGene(init_belief_gene)
                                .SetThreshold(op_form_threshold)
                                .SetSubject(subject_test)
                                .SetInitOpinion(Matrix<double>.Build.Dense(5, 1, 0.0));

            var sensor_gene = new SensorGenerator()
                            .SetSensorSize((int)(0.1 * graph.Nodes.Count));

            int agent_gene_seed = 0;
            var agent_gene_rand = new ExtendRandom(agent_gene_seed);


            var agent_network = new AgentNetwork()
                                    .SetRand(agent_gene_rand)
                                    .GenerateNetworkFrame(graph)
                                    .ApplySampleAgent(sample_agent_1, mode: SampleAgentSetMode.RandomSetRate, random_set_rate: 0.5)
                                    .ApplySampleAgent(sample_agent_2, mode: SampleAgentSetMode.RemainSet)
                                    //.ApplySampleAgent(sample_agent_test, mode: SampleAgentSetMode.RemainSet)
                                    .GenerateSensor(sensor_gene)
                                    .SetLayout(layout);


            int update_step_seed = 0;
            var update_step_rand = new ExtendRandom(update_step_seed);

            var env_mgr = new EnvironmentManager()
                            .SetSubject(subject_tv)
                            //.SetSubject(subject_test)
                            .SetCorrectDim(0)
                            .SetSensorRate(0.55);

            OSMBase<AAT_OSM> osm = new AATG_OSM()
                    .SetRand(update_step_rand)
                    .SetAgentNetwork(agent_network)
                    .SetEnvManager(env_mgr)
                    .SetSubjectManager(subject_manager)
                    .SetInitWeightsMode(mode: CalcWeightMode.FavorMyOpinion)
                    .SetOpinionIntroInterval(10)
                    .SetOpinionIntroRate(0.1);


            this.MyOSM = osm;
            this.MyAnimationForm.RegistOSM(osm);
            //osm.InitializeToZeroStep();
            //osm.UpdateSteps(200);
            //osm.UpdateRounds(100, 200);
            //osm.UpdateRounds(1, 10000);

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
            this.radioButtonStepCheck.Checked = true;
            this.numericUpDownStepsControl.Value = 1000;
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

        void PlayOne()
        {
            var control_seed = (int)this.numericUpDownControlSeed.Value;
            var control_speed = (int)this.numericUpDownSpeedControl.Value;
            var max_steps = (int)this.numericUpDownStepsControl.Value;

            var current_rounds = int.Parse(this.labelRoundNum.Text);
            var current_steps = int.Parse(this.labelStepNum.Text);

            control_seed += int.Parse(this.labelStepNum.Text);
            control_seed += int.Parse(this.labelRoundNum.Text);
            this.MyOSM.UpdateSteps(control_speed);
            this.labelStepNum.Text = (current_steps + control_speed).ToString();
            current_steps += control_speed;

            if (this.radioButtonRoundCheck.Checked)
            {
                if (max_steps <= current_steps)
                {
                    this.MyOSM.IntegrateReceiveOpinion();
                    this.MyOSM.UpdateRoundWithoutSteps();
                    this.MyAnimationForm.UpdatePictureBox();
                    this.MyOSM.MyAgentNetwork.Agents.ForEach(agent => this.MyOSM.AgentReceiveOpinionsByRound[agent].Clear());
                    this.MyOSM.MyAgentNetwork.Agents.ForEach(agent => this.MyOSM.AgentReceiveRounds[agent].Clear());
                    this.labelStepNum.Text = 0.ToString();
                    this.labelRoundNum.Text = (current_rounds + 1).ToString();
                }
                else
                {
                    this.MyAnimationForm.UpdatePictureBox();
                }
            }
            else
            {
                this.MyAnimationForm.UpdatePictureBox();

            }

        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            this.PlayOne();
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

            int step_num = 0;
            if (this.radioButtonStepCheck.Checked)
            {
                this.labelStepNum.Text = step_num.ToString();
                this.MyOSM.InitializeToZeroStep();
            }
            else if (this.radioButtonRoundCheck.Checked)
            {
                int round_num = 0;
                this.labelRoundNum.Text = round_num.ToString();
                this.labelStepNum.Text = step_num.ToString();
                this.MyOSM.InitializeToZeroRound();
            }
            this.MyOSM.MyAgentNetwork.Agents.ForEach(agent => this.MyOSM.AgentReceiveOpinionsByStep[agent].Clear());
            this.MyOSM.MyAgentNetwork.Agents.ForEach(agent => this.MyOSM.AgentReceiveRounds[agent].Clear());
            this.MyAnimationForm.UpdatePictureBox();
        }

        private void radioButtonPlayStop_Click(object sender, EventArgs e)
        {
            this.PlayStop();
        }

        private void radioButtonPlayStep_Click(object sender, EventArgs e)
        {
            this.PlayOne();
        }
    }
}
