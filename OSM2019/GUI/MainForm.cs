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
      TargethExp();
      this.MyAnimationForm.Show();
      this.MyAnimationForm.Left = this.Right;
    }
    void TargethExp()
    {
      var dt = DateTime.Now;
      var dt_name = dt.ToString("yyyyMMddHHmm");
      //int seeds = 10;
      var seeds = new List<int>() { 0, 1, 2 };
      double sensor_weight = 0.8;
      //int rounds = 200;
      int steps = 2000;
      var th_duration = 0.05;

      dt = DateTime.Now;
      dt_name = dt.ToString("yyyyMMddHHmm");
      //Parallel.For(0, seeds, seed =>
      Parallel.ForEach(seeds, seed =>
      {
        new TargetH_Experiment()
              .SetGraphs(new List<GraphEnum>() { GraphEnum.WS })
              //.SetAlgos(new List<AlgoEnum>() { AlgoEnum.GDWTsigW })
              .SetAlgos(new List<AlgoEnum>() { AlgoEnum.OSMonly, AlgoEnum.AAT, AlgoEnum.SWT, AlgoEnum.AATfunction })
              .SetNetworkSize(new List<int>() { 500 })
              .SetDims(new List<int>() { 2, 4, 6, 8, 10 }).SetSensorWeight(sensor_weight)
              //.SetSensorCommonWeight(0.70)
              .SetSensorSizeRate(new List<double>() { 0.05 })
              .SetMaliciousSensorSizeRate(new List<double>() { 0.00 })
              //.SetSensorFixSize(10)
              .SetBeliefUpdater(new BeliefUpdater().SetSensorWeightMode(SensorWeightEnum.DependSensorRate))
              .SetSubjectName("test")
              .SetEnvDistWeights(new List<double>() { 0.5 })
              //.SetEnvDistWeights(new List<double>() { 0.65 })
              .SetMaliciousEnvDistWeights(new List<double>() { 0.0 })
              .SetCommonCuriocity(0.1)
              //.SetTargetHs(new List<double>() { 0.7, 0.75, 0.8, 0.85, 0.9, 0.91, 0.92, 0.93, 0.94, 0.95, 0.96, 0.97, 0.98, 0.99, 1.0 })
              .SetTargetHs(new List<double>() { 0.90 })
              .SetLogFolder(dt_name, "dw0.5")
              .SetRounds(new List<int>() { 200 })
              .SetSteps(steps)
              .SetOpinionThreshold(0.8)
              .SetDynamics(new List<bool>() { false })
              .SetEnvDistModes(new List<EnvDistributionEnum> { EnvDistributionEnum.Exponential })
              .SetInfoWeightRates(new List<double>() { 1.0 })
              .SetCommonWeights(new List<double>() { 1.0 })
              //.SetCommonWeights(Enumerable.Range(25, 25).Select(x => x * 0.02).ToList())
              .Run(seed);
      });

      Environment.Exit(0);
    }
    void Test()
    {
      int agent_size = 200;
      int dim = 5;
      int correct_dim = 0;
      int malicious_dim = 1;
      AlgoEnum algo = AlgoEnum.AATfunction;
      double targeth = 0.90;
      double common_weight = 0.5;
      double common_curiocity = 0.5;
      double sensor_rate = 0.8;
      double dist_weight = 0.5;
      double malicious_dist_weight = 0.8;
      int sensor_size = (int)(0.05 * agent_size);
      //int malicious_sensor_size = (int)(0.04 * agent_size);
      int malicious_sensor_size = 0;
      var op_form_threshold = 0.9;
      int sample_size = 10;
      int change_round = 0;

      var belief_updater = new BeliefUpdater().SetSensorWeightMode(SensorWeightEnum.DependSensorRate);

      GraphGeneratorBase graph_generator;
      //graph_generator = new PC_GraphGenerator().SetNodeSize(500).SetRandomEdges(3).SetAddTriangleP(0.1);
      graph_generator = new WS_GraphGenerator().SetNodeSize(agent_size).SetNearestNeighbors(6).SetRewireP(0.01);
      //graph_generator = new BA_GraphGenerator().SetNodeSize(agent_size).SetAttachEdges(2);
      //graph_generator = new Grid2D_GraphGenerator().SetNodeSize(agent_size);

      var pb = new ExtendProgressBar(100);
      var graph = graph_generator.Generate(0, pb);
      var layout = new KamadaKawai_LayoutGenerator(graph).Generate(pb);
      //var layout = new Circular_LayoutGenerator(graph).Generate();

      var init_belief_gene = new InitBeliefGenerator()
                              .SetInitBeliefMode(mode: InitBeliefMode.NormalNarrow);
      //.SetInitBeliefMode(mode: InitBeliefMode.NormalWide);

      var subject_test = new OpinionSubject("test", dim);

      var sample_agent_test = new SampleAgent()
                          .SetInitBeliefGene(init_belief_gene)
                          .SetThreshold(op_form_threshold)
                          .SetSubject(subject_test)
                          .SetInitOpinion(Vector<double>.Build.Dense(dim, 0.0));

      var sensor_gene = new SensorGenerator()
      //                .SetSensorSize((int)5);
      .SetSensorSize(sensor_size, malicious_sensor_size);

      int agent_gene_seed = 4;
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

      OSMBase osm = new OSM_Only();
      switch (algo)
      {
        case AlgoEnum.AAT:
          var osm_aat = new AAT_OSM();
          osm_aat.SetTargetH(targeth);
          osm = osm_aat;
          break;
        case AlgoEnum.AATG:
          var osm_aatg = new AATG_OSM();
          osm_aatg.SetTargetH(targeth);
          osm = osm_aatg;
          break;
        case AlgoEnum.AATfix:
          var osm_aatfix = new AATfix_OSM();
          osm_aatfix.SetTargetH(targeth);
          osm = osm_aatfix;
          break;
        case AlgoEnum.OSMonly:
          var osm_only = new OSM_Only();
          osm_only.SetCommonWeight(common_weight);
          osm = osm_only;
          break;
        case AlgoEnum.IWTori:
          var osm_iwtori = new IWTori_OSM();
          osm_iwtori.SetCommonCuriocity(common_curiocity);
          osm_iwtori.SetTargetH(targeth);
          osm = osm_iwtori;
          break;
        case AlgoEnum.AATparticle:
          var osm_aatpar = new AATparticle_OSM();
          osm_aatpar.SetSampleSize(sample_size);
          osm_aatpar.SetTargetH(targeth);
          osm = osm_aatpar;
          break;
        case AlgoEnum.AATwindow:
          var osm_window = new AATwindow_OSM();
          osm_window.SetTargetH(targeth);
          osm_window.SetAwaRateWindowSize(20);
          osm = osm_window;
          break;
        case AlgoEnum.AATwindowparticle:
          var osm_window_particle = new AATwindow_particle_OSM();
          osm_window_particle.SetTargetH(targeth);
          osm_window_particle.SetAwaRateWindowSize(20);
          osm_window_particle.SetSampleSize(10);
          osm = osm_window_particle;
          break;
        case AlgoEnum.AATfunction:
          var osm_function = new AATfunction_OSM();
          osm_function.SetTargetH(targeth);
          osm_function.SetAwaRateWindowSize(100);
          osm = osm_function;
          break;
        case AlgoEnum.AATfunctioniwt:
          var osm_function_iwt = new AATfunction_iwt_OSM();
          osm_function_iwt.SetCommonCuriocity(common_curiocity);
          osm_function_iwt.SetTargetH(targeth);
          osm_function_iwt.SetAwaRateWindowSize(20);
          osm = osm_function_iwt;
          break;
        case AlgoEnum.SWT:
          var osm_aat_info = new SWT_OSM();
          osm_aat_info.SetTargetH(targeth);
          osm_aat_info.SetAwaRateWindowSize(100);
          osm_aat_info.SetLinkInfoValueWindowSize(100);
          osm_aat_info.SetInfoWeightRate(1.0);
          osm = osm_aat_info;
          break;
        case AlgoEnum.SWTstep:
          var osm_aat_info_step = new SWT_step_OSM();
          osm_aat_info_step.SetTargetH(targeth);
          osm_aat_info_step.SetAwaRateWindowSize(100);
          osm_aat_info_step.SetLinkInfoValueWindowSize(100);
          osm_aat_info_step.SetInfoWeightRate(1.0);
          osm_aat_info_step.SetInfoLearningRate(0.2);
          osm = osm_aat_info_step;
          break;
        case AlgoEnum.GDWTsigW:
          var osm_gdwt_sigw = new GDWT_OSM();
          osm_gdwt_sigw.SetTargetH(targeth);
          osm_gdwt_sigw.SetAwaRateWindowSize(100);
          osm_gdwt_sigw.SetEstimateFunction(new Sigmoid_weight_EstFunc(1.0, 0, 3));
          osm = osm_gdwt_sigw;
          break;
        case AlgoEnum.GDWTsigH:
          var osm_gdwt_sigh = new GDWT_OSM();
          osm_gdwt_sigh.SetTargetH(targeth);
          osm_gdwt_sigh.SetAwaRateWindowSize(100);
          osm_gdwt_sigh.SetEstimateFunction(new Sigmoid_awa_EstFunc(1.0, 0, 3));
          osm = osm_gdwt_sigh;
          break;
        case AlgoEnum.GDWTpowerH:
          var osm_gdwt_powh = new GDWT_OSM();
          osm_gdwt_powh.SetTargetH(targeth);
          osm_gdwt_powh.SetAwaRateWindowSize(100);
          osm_gdwt_powh.SetEstimateFunction(new PowerH_awa_EstFunc(3.0, 0));
          osm = osm_gdwt_powh;
          break;
        case AlgoEnum.GDWTpowerW:
          var osm_gdwt_poww = new GDWT_OSM();
          osm_gdwt_poww.SetTargetH(targeth);
          osm_gdwt_poww.SetAwaRateWindowSize(100);
          osm_gdwt_poww.SetEstimateFunction(new PowerH_weight_EstFunc(3.0, 0));
          osm = osm_gdwt_poww;
          break;
        default:
          break;
      }
      osm.SetRand(update_step_rand);
      osm.SetAgentNetwork(agent_network);
      var subject_mgr_dic = new Dictionary<int, SubjectManager>();
      subject_mgr_dic.Add(0, new SubjectManagerGenerator()
          .Generate(subject_test, dist_weight, correct_dim, sensor_rate, EnvDistributionEnum.Turara, malicious_dim, malicious_dist_weight));
      //for (int i = 0; i < 1; i++)
      //{
      //  subject_mgr_dic.Add(i * change_round, new SubjectManagerGenerator().Generate(subject_test, dist_weight, i % dim, sensor_rate, EnvDistributionEnum.Turara));
      //}
      osm.SetSubjectManagerDic(subject_mgr_dic);
      osm.SetInitWeightsMode(mode: CalcWeightMode.FavorMyOpinion);
      osm.SetOpinionIntroInterval(10);
      osm.SetOpinionIntroRate(0.1);
      //osm.SetSensorCommonWeight(0.70);
      osm.SetBeliefUpdater(belief_updater);

      this.MyOSM = osm;
      this.MyAnimationForm.RegistOSM(osm);

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
      this.checkBoxMenu.Checked = true;

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
