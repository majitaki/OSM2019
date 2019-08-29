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
      //NormalExp();
      TargethExp();
      //TestExp();
      this.MyAnimationForm.Show();
      this.MyAnimationForm.Left = this.Right;
    }

    void NormalExp()
    {
      var dt = DateTime.Now;
      var dt_name = dt.ToString("yyyy-MMdd-HHmm");
      int seeds = 2;
      int dim = 5;
      double sensor_rate = 0.8;
      int network_size = 200;
      double dist_weight = 0.5;
      int rounds = 1000;
      int steps = 3000;
      bool is_dynamic = false;


      dt = DateTime.Now;
      dt_name = dt.ToString("yyyy-MMdd-HHmm");
      Parallel.For(0, seeds, seed =>
      {
        new Normal_Experiment()
              .SetGraphs(new List<GraphEnum>() { GraphEnum.WS, GraphEnum.Hexagonal })
              .SetAlgos(new List<AlgoEnum>() { AlgoEnum.AAT, AlgoEnum.AATfunction, AlgoEnum.AATparticle, AlgoEnum.AATfunctionparticle, AlgoEnum.IWTori, AlgoEnum.AATfunctioniwt })
              .SetNetworkSize(network_size, network_size, 500)
              .SetDimSize(dim).SetSensorRate(sensor_rate)
              .SetBeliefUpdater(new BeliefUpdater().SetSensorWeightMode(SensorWeightEnum.DependSensorRate))
              .SetSubjectName("test")
              .SetEnvDistWeight(dist_weight)
              //.SetCommonWeight(0.8)
              .SetCommonCuriocity(0.5)
              .SetTargetHs(0.9)
              //.SetSensorSizeRate(0.1)
              .SetSensorFixSize(10)
              .SetLogFolder(dt_name, "normal_static")
              .SetRounds(rounds)
              .SetSteps(steps)
              .SetOpinionThreshold(0.9)
              .SetDynamic(is_dynamic)
              .Run(seed);
      });

      dt = DateTime.Now;
      dt_name = dt.ToString("yyyy-MMdd-HHmm");
      is_dynamic = true;
      Parallel.For(0, seeds, seed =>
      {
        new Normal_Experiment()
              .SetGraphs(new List<GraphEnum>() { GraphEnum.WS, GraphEnum.Hexagonal })
              .SetAlgos(new List<AlgoEnum>() { AlgoEnum.AAT, AlgoEnum.AATfunction, AlgoEnum.AATparticle, AlgoEnum.AATfunctionparticle, AlgoEnum.IWTori, AlgoEnum.AATfunctioniwt })
              .SetNetworkSize(network_size, network_size, 500)
              .SetDimSize(dim).SetSensorRate(sensor_rate)
              .SetBeliefUpdater(new BeliefUpdater().SetSensorWeightMode(SensorWeightEnum.DependSensorRate))
              .SetSubjectName("test")
              .SetEnvDistWeight(dist_weight)
              //.SetCommonWeight(0.8)
              .SetCommonCuriocity(0.5)
              .SetTargetHs(0.9)
              //.SetSensorSizeRate(0.1)
              .SetSensorFixSize(10)
              .SetLogFolder(dt_name, "normal_dynamic")
              .SetRounds(rounds)
              .SetSteps(steps)
              .SetOpinionThreshold(0.9)
              .SetDynamic(is_dynamic)
              .Run(seed);
      });


      Environment.Exit(0);
    }

    void TargethExp()
    {
      var dt = DateTime.Now;
      var dt_name = dt.ToString("yyyyMMddHHmm");
      int seeds = 3;
      double sensor_rate = 0.8;
      int rounds = 200;
      int steps = 2000;
      var th_duration = 0.05;

      dt = DateTime.Now;
      dt_name = dt.ToString("yyyyMMddHHmm");
      Parallel.For(0, seeds, seed =>
      {
        new TargetH_Experiment()
              .SetGraphs(new List<GraphEnum>() { GraphEnum.WS, GraphEnum.BA, GraphEnum.Grid2D })
              .SetAlgos(new List<AlgoEnum>() { AlgoEnum.AAT, AlgoEnum.AATinfo })
              .SetNetworkSize(new List<int>() { 100, 500, 1000 })
              .SetDims(new List<int>() { 2, 5, 10 }).SetSensorRate(sensor_rate)
              //.SetSensorCommonWeight(0.70)
              .SetSensorSizeRate(0.05)
              //.SetSensorFixSize(10)
              .SetBeliefUpdater(new BeliefUpdater().SetSensorWeightMode(SensorWeightEnum.DependSensorRate))
              .SetSubjectName("test")
              .SetEnvDistWeights(new List<double>() { 0.5 })
              .SetCommonCuriocity(0.1)
              //.SetSensorFixSize(10)
              .SetTargetHs(Enumerable.Range(10, 20 - 9).Select(i => i * th_duration).ToList())
              .SetLogFolder(dt_name, "")
              .SetRounds(rounds)
              .SetSteps(steps)
              .SetOpinionThreshold(0.9)
              .SetDynamics(new List<bool>() { false })
              .SetEnvDistModes(new List<EnvDistributionEnum> { EnvDistributionEnum.Exponential })
              .SetInfoWeightRates(new List<double>() { 1.0 })
              .Run(seed);
      });

      Environment.Exit(0);
    }

    void TestExp()
    {
      //dim, rate
      //2, 0.62
      //3, 0.5
      //4, 0.45
      //5, 0.42
      //10. 0.35
      var dt = DateTime.Now;
      var dt_name = dt.ToString("yyyy-MMdd-HHmm");
      //Parallel.For(0, 5, seed =>
      //{
      //    new CommonCuriocity_Experiment()
      //    .SetGraphs(new List<GraphEnum>() { GraphEnum.WS })
      //    .SetAlgos(new List<AlgoEnum>() { AlgoEnum.IWTorionly })
      //    .SetNetworkSize(300, 300, 100)
      //    .SetDimSize(10).SetSensorRate(0.3)
      //    .SetSensorCommonWeight(0.70)
      //    .SetSensorSizeRate(0.1)
      //    //.SetSensorFixSize(10)
      //    //.SetTargetH(0.90)
      //    .SetCommonWeight(0.4)
      //    .SetCommonCuriocities(Enumerable.Range(0, 11).Select(i => i / 10.0).ToList())
      //    .SetRounds(300)
      //    .SetSteps(1500)
      //    .SetOpinionThreshold(0.9)
      //    .SetLogFolder(dt_name, "")
      //    .Run(seed);
      //});
      //dt = DateTime.Now;
      //dt_name = dt.ToString("yyyy-MMdd-HHmm");
      //int dim = 2;
      //int correct_dim = 0;
      //double sensor_rate = 0.6;
      //int network_size = 100;
      //Parallel.For(0, 5, seed =>
      //{
      //    new CommonWeight_Experiment()
      //    .SetGraphs(new List<GraphEnum>() { GraphEnum.WS })
      //    .SetAlgos(new List<AlgoEnum>() { AlgoEnum.OSMonly })
      //    .SetNetworkSize(network_size, network_size, 100)
      //    .SetDimSize(dim).SetSensorRate(sensor_rate)
      //    .SetSensorSizeRate(0.1)
      //    .SetCustomDistribution(new Turara_DistGenerator(dim, 0.3, correct_dim).Generate())
      //    .SetBeliefUpdater(new BeliefUpdater().SetSensorWeightMode(SensorWeightEnum.Custom, sensor_rate))
      //    //.SetSensorFixSize(10)
      //    .SetCommonWeights(Enumerable.Range(0, 21).Select(i => i / 20.0).Where(j => j >= 1.0 / 10).ToList())
      //    //.SetCommonCuriocity(0.8)
      //    //.SetRandomCommonCuriocity()
      //    .SetLogFolder(dt_name, "")
      //    .SetRounds(300)
      //    .SetSteps(3000)
      //    .SetOpinionThreshold(0.9)
      //    .Run(seed);
      //});

      Environment.Exit(0);
    }

    void Test()
    {
      int agent_size = 200;
      int dim = 2;
      int correct_dim = 0;
      AlgoEnum algo = AlgoEnum.AATinfostep;
      double targeth = 0.9;
      double common_weight = 0.5;
      double common_curiocity = 0.5;
      double sensor_rate = 0.8;
      double dist_weight = 0.5;
      var op_form_threshold = 0.9;
      int sample_size = 10;
      int change_round = 25;

      var belief_updater = new BeliefUpdater().SetSensorWeightMode(SensorWeightEnum.DependSensorRate);

      GraphGeneratorBase graph_generator;
      //graph_generator = new PC_GraphGenerator().SetNodeSize(500).SetRandomEdges(3).SetAddTriangleP(0.1);
      //graph_generator = new WS_GraphGenerator().SetNodeSize(agent_size).SetNearestNeighbors(6).SetRewireP(0.01);
      graph_generator = new BA_GraphGenerator().SetNodeSize(agent_size).SetAttachEdges(2);
      //graph_generator = new Grid2D_GraphGenerator().SetNodeSize(agent_size);

      var pb = new ExtendProgressBar(100);
      var graph = graph_generator.Generate(0, pb);
      var layout = new KamadaKawai_LayoutGenerator(graph).Generate(pb);
      //var layout = new Circular_LayoutGenerator(graph).Generate();

      var init_belief_gene = new InitBeliefGenerator()
                              .SetInitBeliefMode(mode: InitBeliefMode.NormalNarrow);


      var subject_test = new OpinionSubject("test", dim);

      var sample_agent_test = new SampleAgent()
                          .SetInitBeliefGene(init_belief_gene)
                          .SetThreshold(op_form_threshold)
                          .SetSubject(subject_test)
                          .SetInitOpinion(Vector<double>.Build.Dense(dim, 0.0));

      var sensor_gene = new SensorGenerator()
                      .SetSensorSize((int)(0.05 * graph.Nodes.Count));

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
          osm_function.SetAwaRateWindowSize(20);
          osm = osm_function;
          break;
        case AlgoEnum.AATfunctioniwt:
          var osm_function_iwt = new AATfunction_iwt_OSM();
          osm_function_iwt.SetCommonCuriocity(common_curiocity);
          osm_function_iwt.SetTargetH(targeth);
          osm_function_iwt.SetAwaRateWindowSize(20);
          osm = osm_function_iwt;
          break;
        case AlgoEnum.AATinfo:
          var osm_aat_info = new AATinfo_OSM();
          osm_aat_info.SetTargetH(targeth);
          osm_aat_info.SetAwaRateWindowSize(20);
          osm_aat_info.SetLinkInfoValueWindowSize(20);
          osm_aat_info.SetInfoWeightRate(0.5);
          osm = osm_aat_info;
          break;
        case AlgoEnum.AATinfostep:
          var osm_aat_info_step = new AATinfo_step_OSM();
          osm_aat_info_step.SetTargetH(targeth);
          osm_aat_info_step.SetAwaRateWindowSize(100);
          osm_aat_info_step.SetLinkInfoValueWindowSize(100);
          osm_aat_info_step.SetInfoWeightRate(1.0);
          osm_aat_info_step.SetInfoLearningRate(0.2);
          osm = osm_aat_info_step;
          break;
        default:
          break;
      }
      osm.SetRand(update_step_rand);
      osm.SetAgentNetwork(agent_network);
      var subject_mgr_dic = new Dictionary<int, SubjectManager>();
      for (int i = 0; i < 1; i++)
      {
        subject_mgr_dic.Add(i * change_round, new SubjectManagerGenerator().Generate(subject_test, dist_weight, i % dim, sensor_rate, EnvDistributionEnum.Exponential));
      }
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
