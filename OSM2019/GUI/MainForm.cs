using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;
using OSM2019.GUI;
using OSM2019.Interfaces;
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

        public MainForm()
        {
            this.MyGUIEnum = GUIEnum.MainFormGUI;
            this.InitializeGUIs();
            InitializeComponent();
            this.UserInitialize();

            GraphGeneratorBase graph_generator;
            graph_generator = new PC_GraphGenerator().SetNodeSize(100).SetRandomEdges(3).SetAddTriangleP(0.1);

            var graph = graph_generator.Generate(0);
            var layout = new Circular_LayoutGenerator(graph).Generate();

            var init_belief_gene = new InitBeliefGenerator()
                                    .SetInitBeliefMode(mode: InitBeliefMode.NoRandom);

            var subject_tv = "good_tv";
            var subject_company = "good_company";
            double[] conv_array = { 2.0, 0.0, 0.0, 1.0 };
            var conv_matrix = Matrix<double>.Build.DenseOfColumnMajor(2, 2, conv_array);

            var subject_manager = new SubjectManager()
                                .AddSubject(subject_tv)
                                .AddSubject(subject_company)
                                .AddConvMatrix(subject_tv, subject_company, conv_matrix);



            var op_form_threshold = 0.9;
            var sample_agent_1 = new SampleAgent()
                                .SetInitBeliefGene(init_belief_gene)
                                .SetThreshold(op_form_threshold)
                                .SetSubject(subject_tv)
                                .SetInitOpinion(Matrix<double>.Build.Dense(2, 1, 0.0))
                                .SetInitWeightsMode(mode: InitWeightMode.Equality);


            var sample_agent_2 = new SampleAgent()
                                .SetInitBeliefGene(init_belief_gene)
                                .SetThreshold(op_form_threshold)
                                .SetSubject(subject_company)
                                .SetInitOpinion(Matrix<double>.Build.Dense(3, 1, 0.0))
                                .SetInitWeightsMode(mode: InitWeightMode.Equality);

            var sensor_gene = new SensorGenerator()
                            .SetSensorSize(10);

            int agent_gene_seed = 0;
            var agent_gene_rand = new ExtendRandom(agent_gene_seed);


            var agent_network = new AgentNetwork()
                                    .SetRand(agent_gene_rand)
                                    .GenerateNetworkFrame(graph)
                                    .ApplySampleAgent(sample_agent_1, mode: SampleAgentSetMode.RandomSetRate, random_set_rate: 0.5)
                                    .ApplySampleAgent(sample_agent_2, mode: SampleAgentSetMode.RemainSet)
                                    .GenerateSensor(sensor_gene)
                                    .SetLayout(layout);

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
            this.numericUpDownStepsControl.Value = 3000;
            this.numericUpDownSpeedControl.Value = 1;
            this.labelRoundNum.Text = 1.ToString();
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
    }

}
