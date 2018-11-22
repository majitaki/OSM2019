namespace OSM2019
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxMenu = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonPlayStop = new System.Windows.Forms.RadioButton();
            this.radioButtonSeedPlus = new System.Windows.Forms.RadioButton();
            this.radioButtonPlay = new System.Windows.Forms.RadioButton();
            this.radioButtonPlayStep = new System.Windows.Forms.RadioButton();
            this.radioButtonStepCheck = new System.Windows.Forms.RadioButton();
            this.radioButtonRoundCheck = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownSpeedControl = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownControlSeed = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStepsControl = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelRoundNum = new System.Windows.Forms.Label();
            this.labelStepNum = new System.Windows.Forms.Label();
            this.buttonGraphShow = new System.Windows.Forms.Button();
            this.labelStep = new System.Windows.Forms.Label();
            this.labelRound = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonGraphGUI = new System.Windows.Forms.RadioButton();
            this.radioButtonAgentGUI = new System.Windows.Forms.RadioButton();
            this.radioButtonLearningGUI = new System.Windows.Forms.RadioButton();
            this.radioButtonExperimentGUI = new System.Windows.Forms.RadioButton();
            this.timerAnimation = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeedControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownControlSeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStepsControl)).BeginInit();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.flowLayoutPanel2.Controls.Add(this.checkBoxMenu);
            this.flowLayoutPanel2.Controls.Add(this.panel3);
            this.flowLayoutPanel2.Controls.Add(this.panel4);
            this.flowLayoutPanel2.Controls.Add(this.panel2);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(784, 116);
            this.flowLayoutPanel2.TabIndex = 10;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // checkBoxMenu
            // 
            this.checkBoxMenu.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxMenu.FlatAppearance.BorderSize = 0;
            this.checkBoxMenu.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxMenu.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxMenu.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.checkBoxMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxMenu.Image = global::OSM2019.Properties.Resources.icon_menu;
            this.checkBoxMenu.Location = new System.Drawing.Point(3, 3);
            this.checkBoxMenu.Name = "checkBoxMenu";
            this.checkBoxMenu.Size = new System.Drawing.Size(42, 110);
            this.checkBoxMenu.TabIndex = 8;
            this.checkBoxMenu.UseVisualStyleBackColor = true;
            this.checkBoxMenu.CheckedChanged += new System.EventHandler(this.checkBoxMenu_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.flowLayoutPanel3);
            this.panel3.Controls.Add(this.radioButtonStepCheck);
            this.panel3.Controls.Add(this.radioButtonRoundCheck);
            this.panel3.Location = new System.Drawing.Point(51, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(272, 112);
            this.panel3.TabIndex = 7;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.radioButtonPlayStop);
            this.flowLayoutPanel3.Controls.Add(this.radioButtonSeedPlus);
            this.flowLayoutPanel3.Controls.Add(this.radioButtonPlay);
            this.flowLayoutPanel3.Controls.Add(this.radioButtonPlayStep);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(6, 7);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(263, 69);
            this.flowLayoutPanel3.TabIndex = 31;
            // 
            // radioButtonPlayStop
            // 
            this.radioButtonPlayStop.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonPlayStop.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonPlayStop.FlatAppearance.BorderSize = 0;
            this.radioButtonPlayStop.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonPlayStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.radioButtonPlayStop.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonPlayStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonPlayStop.Image = global::OSM2019.Properties.Resources.icon_stop;
            this.radioButtonPlayStop.Location = new System.Drawing.Point(3, 3);
            this.radioButtonPlayStop.Name = "radioButtonPlayStop";
            this.radioButtonPlayStop.Size = new System.Drawing.Size(58, 58);
            this.radioButtonPlayStop.TabIndex = 3;
            this.radioButtonPlayStop.TabStop = true;
            this.radioButtonPlayStop.UseVisualStyleBackColor = false;
            this.radioButtonPlayStop.Click += new System.EventHandler(this.radioButtonPlayStop_Click);
            // 
            // radioButtonSeedPlus
            // 
            this.radioButtonSeedPlus.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonSeedPlus.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonSeedPlus.FlatAppearance.BorderSize = 0;
            this.radioButtonSeedPlus.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonSeedPlus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.radioButtonSeedPlus.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonSeedPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonSeedPlus.Image = global::OSM2019.Properties.Resources.icon_seedplus;
            this.radioButtonSeedPlus.Location = new System.Drawing.Point(67, 3);
            this.radioButtonSeedPlus.Name = "radioButtonSeedPlus";
            this.radioButtonSeedPlus.Size = new System.Drawing.Size(58, 58);
            this.radioButtonSeedPlus.TabIndex = 4;
            this.radioButtonSeedPlus.TabStop = true;
            this.radioButtonSeedPlus.UseVisualStyleBackColor = false;
            // 
            // radioButtonPlay
            // 
            this.radioButtonPlay.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonPlay.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonPlay.FlatAppearance.BorderSize = 0;
            this.radioButtonPlay.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.radioButtonPlay.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonPlay.Image = global::OSM2019.Properties.Resources.icon_play;
            this.radioButtonPlay.Location = new System.Drawing.Point(131, 3);
            this.radioButtonPlay.Name = "radioButtonPlay";
            this.radioButtonPlay.Size = new System.Drawing.Size(58, 58);
            this.radioButtonPlay.TabIndex = 5;
            this.radioButtonPlay.TabStop = true;
            this.radioButtonPlay.UseVisualStyleBackColor = false;
            this.radioButtonPlay.Click += new System.EventHandler(this.radioButtonPlay_Click);
            // 
            // radioButtonPlayStep
            // 
            this.radioButtonPlayStep.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonPlayStep.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonPlayStep.FlatAppearance.BorderSize = 0;
            this.radioButtonPlayStep.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonPlayStep.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.radioButtonPlayStep.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonPlayStep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonPlayStep.Image = global::OSM2019.Properties.Resources.icon_playstep;
            this.radioButtonPlayStep.Location = new System.Drawing.Point(195, 3);
            this.radioButtonPlayStep.Name = "radioButtonPlayStep";
            this.radioButtonPlayStep.Size = new System.Drawing.Size(58, 58);
            this.radioButtonPlayStep.TabIndex = 6;
            this.radioButtonPlayStep.TabStop = true;
            this.radioButtonPlayStep.UseVisualStyleBackColor = false;
            this.radioButtonPlayStep.Click += new System.EventHandler(this.radioButtonPlayStep_Click);
            // 
            // radioButtonStepCheck
            // 
            this.radioButtonStepCheck.AutoSize = true;
            this.radioButtonStepCheck.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonStepCheck.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.radioButtonStepCheck.Location = new System.Drawing.Point(79, 75);
            this.radioButtonStepCheck.Name = "radioButtonStepCheck";
            this.radioButtonStepCheck.Size = new System.Drawing.Size(54, 22);
            this.radioButtonStepCheck.TabIndex = 30;
            this.radioButtonStepCheck.TabStop = true;
            this.radioButtonStepCheck.Text = "Step";
            this.radioButtonStepCheck.UseVisualStyleBackColor = true;
            // 
            // radioButtonRoundCheck
            // 
            this.radioButtonRoundCheck.AutoSize = true;
            this.radioButtonRoundCheck.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonRoundCheck.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.radioButtonRoundCheck.Location = new System.Drawing.Point(6, 75);
            this.radioButtonRoundCheck.Name = "radioButtonRoundCheck";
            this.radioButtonRoundCheck.Size = new System.Drawing.Size(67, 22);
            this.radioButtonRoundCheck.TabIndex = 28;
            this.radioButtonRoundCheck.TabStop = true;
            this.radioButtonRoundCheck.Text = "Round";
            this.radioButtonRoundCheck.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.numericUpDownSpeedControl);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.numericUpDownControlSeed);
            this.panel4.Controls.Add(this.numericUpDownStepsControl);
            this.panel4.Location = new System.Drawing.Point(329, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(232, 112);
            this.panel4.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(4, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 24);
            this.label1.TabIndex = 32;
            this.label1.Text = "Speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(9, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 24);
            this.label2.TabIndex = 24;
            this.label2.Text = "Steps";
            // 
            // numericUpDownSpeedControl
            // 
            this.numericUpDownSpeedControl.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.numericUpDownSpeedControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownSpeedControl.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDownSpeedControl.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownSpeedControl.Location = new System.Drawing.Point(78, 9);
            this.numericUpDownSpeedControl.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownSpeedControl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSpeedControl.Name = "numericUpDownSpeedControl";
            this.numericUpDownSpeedControl.Size = new System.Drawing.Size(58, 25);
            this.numericUpDownSpeedControl.TabIndex = 31;
            this.numericUpDownSpeedControl.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label17.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label17.Location = new System.Drawing.Point(15, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 24);
            this.label17.TabIndex = 21;
            this.label17.Text = "Seed";
            // 
            // numericUpDownControlSeed
            // 
            this.numericUpDownControlSeed.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.numericUpDownControlSeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownControlSeed.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDownControlSeed.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownControlSeed.Location = new System.Drawing.Point(78, 79);
            this.numericUpDownControlSeed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownControlSeed.Name = "numericUpDownControlSeed";
            this.numericUpDownControlSeed.Size = new System.Drawing.Size(58, 25);
            this.numericUpDownControlSeed.TabIndex = 20;
            // 
            // numericUpDownStepsControl
            // 
            this.numericUpDownStepsControl.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.numericUpDownStepsControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownStepsControl.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericUpDownStepsControl.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownStepsControl.Location = new System.Drawing.Point(78, 44);
            this.numericUpDownStepsControl.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownStepsControl.Name = "numericUpDownStepsControl";
            this.numericUpDownStepsControl.Size = new System.Drawing.Size(58, 25);
            this.numericUpDownStepsControl.TabIndex = 23;
            this.numericUpDownStepsControl.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelRoundNum);
            this.panel2.Controls.Add(this.labelStepNum);
            this.panel2.Controls.Add(this.buttonGraphShow);
            this.panel2.Controls.Add(this.labelStep);
            this.panel2.Controls.Add(this.labelRound);
            this.panel2.Location = new System.Drawing.Point(567, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(232, 112);
            this.panel2.TabIndex = 0;
            // 
            // labelRoundNum
            // 
            this.labelRoundNum.AutoSize = true;
            this.labelRoundNum.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelRoundNum.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelRoundNum.Location = new System.Drawing.Point(129, 35);
            this.labelRoundNum.Name = "labelRoundNum";
            this.labelRoundNum.Size = new System.Drawing.Size(36, 41);
            this.labelRoundNum.TabIndex = 3;
            this.labelRoundNum.Text = "0";
            // 
            // labelStepNum
            // 
            this.labelStepNum.AutoSize = true;
            this.labelStepNum.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelStepNum.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelStepNum.Location = new System.Drawing.Point(129, 68);
            this.labelStepNum.Name = "labelStepNum";
            this.labelStepNum.Size = new System.Drawing.Size(36, 41);
            this.labelStepNum.TabIndex = 2;
            this.labelStepNum.Text = "0";
            // 
            // buttonGraphShow
            // 
            this.buttonGraphShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGraphShow.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonGraphShow.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonGraphShow.Location = new System.Drawing.Point(10, 7);
            this.buttonGraphShow.Name = "buttonGraphShow";
            this.buttonGraphShow.Size = new System.Drawing.Size(101, 27);
            this.buttonGraphShow.TabIndex = 22;
            this.buttonGraphShow.Text = "Graph Show";
            this.buttonGraphShow.UseVisualStyleBackColor = false;
            // 
            // labelStep
            // 
            this.labelStep.AutoSize = true;
            this.labelStep.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelStep.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelStep.Location = new System.Drawing.Point(3, 68);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(93, 41);
            this.labelStep.TabIndex = 1;
            this.labelStep.Text = "Step:";
            // 
            // labelRound
            // 
            this.labelRound.AutoSize = true;
            this.labelRound.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelRound.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelRound.Location = new System.Drawing.Point(3, 33);
            this.labelRound.Name = "labelRound";
            this.labelRound.Size = new System.Drawing.Size(120, 41);
            this.labelRound.TabIndex = 0;
            this.labelRound.Text = "Round:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.flowLayoutPanel1.Controls.Add(this.radioButtonGraphGUI);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonAgentGUI);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonLearningGUI);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonExperimentGUI);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 116);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(61, 545);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // radioButtonGraphGUI
            // 
            this.radioButtonGraphGUI.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonGraphGUI.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonGraphGUI.FlatAppearance.BorderSize = 0;
            this.radioButtonGraphGUI.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonGraphGUI.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.radioButtonGraphGUI.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonGraphGUI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonGraphGUI.Image = global::OSM2019.Properties.Resources.icon_graph;
            this.radioButtonGraphGUI.Location = new System.Drawing.Point(3, 3);
            this.radioButtonGraphGUI.Name = "radioButtonGraphGUI";
            this.radioButtonGraphGUI.Size = new System.Drawing.Size(48, 48);
            this.radioButtonGraphGUI.TabIndex = 2;
            this.radioButtonGraphGUI.TabStop = true;
            this.radioButtonGraphGUI.UseVisualStyleBackColor = false;
            this.radioButtonGraphGUI.CheckedChanged += new System.EventHandler(this.radioButtonSetting_CheckedChanged);
            // 
            // radioButtonAgentGUI
            // 
            this.radioButtonAgentGUI.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonAgentGUI.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonAgentGUI.FlatAppearance.BorderSize = 0;
            this.radioButtonAgentGUI.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonAgentGUI.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.radioButtonAgentGUI.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonAgentGUI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonAgentGUI.Image = global::OSM2019.Properties.Resources.icon_agent;
            this.radioButtonAgentGUI.Location = new System.Drawing.Point(3, 57);
            this.radioButtonAgentGUI.Name = "radioButtonAgentGUI";
            this.radioButtonAgentGUI.Size = new System.Drawing.Size(48, 48);
            this.radioButtonAgentGUI.TabIndex = 3;
            this.radioButtonAgentGUI.TabStop = true;
            this.radioButtonAgentGUI.UseVisualStyleBackColor = false;
            this.radioButtonAgentGUI.CheckedChanged += new System.EventHandler(this.radioButtonSetting_CheckedChanged);
            // 
            // radioButtonLearningGUI
            // 
            this.radioButtonLearningGUI.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonLearningGUI.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonLearningGUI.FlatAppearance.BorderSize = 0;
            this.radioButtonLearningGUI.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonLearningGUI.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.radioButtonLearningGUI.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonLearningGUI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonLearningGUI.Image = global::OSM2019.Properties.Resources.icon_learning;
            this.radioButtonLearningGUI.Location = new System.Drawing.Point(3, 111);
            this.radioButtonLearningGUI.Name = "radioButtonLearningGUI";
            this.radioButtonLearningGUI.Size = new System.Drawing.Size(48, 48);
            this.radioButtonLearningGUI.TabIndex = 4;
            this.radioButtonLearningGUI.TabStop = true;
            this.radioButtonLearningGUI.UseVisualStyleBackColor = false;
            this.radioButtonLearningGUI.CheckedChanged += new System.EventHandler(this.radioButtonSetting_CheckedChanged);
            // 
            // radioButtonExperimentGUI
            // 
            this.radioButtonExperimentGUI.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonExperimentGUI.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonExperimentGUI.FlatAppearance.BorderSize = 0;
            this.radioButtonExperimentGUI.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonExperimentGUI.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.radioButtonExperimentGUI.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.radioButtonExperimentGUI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonExperimentGUI.Image = global::OSM2019.Properties.Resources.icon_experiment;
            this.radioButtonExperimentGUI.Location = new System.Drawing.Point(3, 165);
            this.radioButtonExperimentGUI.Name = "radioButtonExperimentGUI";
            this.radioButtonExperimentGUI.Size = new System.Drawing.Size(48, 48);
            this.radioButtonExperimentGUI.TabIndex = 7;
            this.radioButtonExperimentGUI.TabStop = true;
            this.radioButtonExperimentGUI.UseVisualStyleBackColor = false;
            this.radioButtonExperimentGUI.CheckedChanged += new System.EventHandler(this.radioButtonSetting_CheckedChanged);
            // 
            // timerAnimation
            // 
            this.timerAnimation.Interval = 1;
            this.timerAnimation.Tick += new System.EventHandler(this.timerAnimation_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "OSM2019";
            this.flowLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeedControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownControlSeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStepsControl)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBoxMenu;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButtonStepCheck;
        private System.Windows.Forms.RadioButton radioButtonRoundCheck;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownSpeedControl;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numericUpDownControlSeed;
        private System.Windows.Forms.NumericUpDown numericUpDownStepsControl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelRoundNum;
        private System.Windows.Forms.Label labelStepNum;
        private System.Windows.Forms.Button buttonGraphShow;
        private System.Windows.Forms.Label labelStep;
        private System.Windows.Forms.Label labelRound;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton radioButtonGraphGUI;
        private System.Windows.Forms.RadioButton radioButtonAgentGUI;
        private System.Windows.Forms.RadioButton radioButtonLearningGUI;
        private System.Windows.Forms.RadioButton radioButtonExperimentGUI;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.RadioButton radioButtonPlayStop;
        private System.Windows.Forms.RadioButton radioButtonSeedPlus;
        private System.Windows.Forms.RadioButton radioButtonPlay;
        private System.Windows.Forms.RadioButton radioButtonPlayStep;
        private System.Windows.Forms.Timer timerAnimation;
    }
}

