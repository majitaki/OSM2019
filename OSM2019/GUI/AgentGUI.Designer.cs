namespace OSM2019.GUI
{
    partial class AgentGUI
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxInitOpinion = new System.Windows.Forms.ComboBox();
            this.numericUpDownAgentSeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonGenerateAgentNetwork = new System.Windows.Forms.Button();
            this.groupBoxAgent = new System.Windows.Forms.GroupBox();
            this.numericUpDownTargetH = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownOpinionIntroDuration = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownRedSigma = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxSensorRateEnabled = new System.Windows.Forms.CheckBox();
            this.numericUpDownSensorRate = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSensorAccuracy = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDownOpinionIntroRate = new System.Windows.Forms.NumericUpDown();
            this.labelOpinionIntroRate = new System.Windows.Forms.Label();
            this.numericUpDownGreenSigma = new System.Windows.Forms.NumericUpDown();
            this.labelAATSigma = new System.Windows.Forms.Label();
            this.labelAATSensorNum = new System.Windows.Forms.Label();
            this.numericUpDownSensorNum = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonGA = new System.Windows.Forms.RadioButton();
            this.radioButtonOther = new System.Windows.Forms.RadioButton();
            this.radioButtonAAT = new System.Windows.Forms.RadioButton();
            this.comboBoxGA = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxOtherAlgo = new System.Windows.Forms.ComboBox();
            this.comboBoxAAT = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAgentSeed)).BeginInit();
            this.groupBoxAgent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpinionIntroDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRedSigma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSensorRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSensorAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpinionIntroRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGreenSigma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSensorNum)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxAgent);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(531, 666);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxInitOpinion);
            this.groupBox1.Controls.Add(this.numericUpDownAgentSeed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonGenerateAgentNetwork);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 116);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agent Generation";
            // 
            // comboBoxInitOpinion
            // 
            this.comboBoxInitOpinion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxInitOpinion.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxInitOpinion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInitOpinion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxInitOpinion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxInitOpinion.FormattingEnabled = true;
            this.comboBoxInitOpinion.Location = new System.Drawing.Point(149, 57);
            this.comboBoxInitOpinion.Name = "comboBoxInitOpinion";
            this.comboBoxInitOpinion.Size = new System.Drawing.Size(107, 27);
            this.comboBoxInitOpinion.TabIndex = 15;
            // 
            // numericUpDownAgentSeed
            // 
            this.numericUpDownAgentSeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownAgentSeed.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownAgentSeed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownAgentSeed.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownAgentSeed.Location = new System.Drawing.Point(187, 87);
            this.numericUpDownAgentSeed.Name = "numericUpDownAgentSeed";
            this.numericUpDownAgentSeed.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownAgentSeed.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Seed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Init Opinion:";
            // 
            // buttonGenerateAgentNetwork
            // 
            this.buttonGenerateAgentNetwork.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGenerateAgentNetwork.Location = new System.Drawing.Point(6, 25);
            this.buttonGenerateAgentNetwork.Name = "buttonGenerateAgentNetwork";
            this.buttonGenerateAgentNetwork.Size = new System.Drawing.Size(99, 32);
            this.buttonGenerateAgentNetwork.TabIndex = 0;
            this.buttonGenerateAgentNetwork.Text = "Generate";
            this.buttonGenerateAgentNetwork.UseVisualStyleBackColor = false;
            // 
            // groupBoxAgent
            // 
            this.groupBoxAgent.AutoSize = true;
            this.groupBoxAgent.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxAgent.Controls.Add(this.numericUpDownTargetH);
            this.groupBoxAgent.Controls.Add(this.label8);
            this.groupBoxAgent.Controls.Add(this.numericUpDownOpinionIntroDuration);
            this.groupBoxAgent.Controls.Add(this.label6);
            this.groupBoxAgent.Controls.Add(this.numericUpDownRedSigma);
            this.groupBoxAgent.Controls.Add(this.label5);
            this.groupBoxAgent.Controls.Add(this.checkBoxSensorRateEnabled);
            this.groupBoxAgent.Controls.Add(this.numericUpDownSensorRate);
            this.groupBoxAgent.Controls.Add(this.numericUpDownSensorAccuracy);
            this.groupBoxAgent.Controls.Add(this.label15);
            this.groupBoxAgent.Controls.Add(this.numericUpDownOpinionIntroRate);
            this.groupBoxAgent.Controls.Add(this.labelOpinionIntroRate);
            this.groupBoxAgent.Controls.Add(this.numericUpDownGreenSigma);
            this.groupBoxAgent.Controls.Add(this.labelAATSigma);
            this.groupBoxAgent.Controls.Add(this.labelAATSensorNum);
            this.groupBoxAgent.Controls.Add(this.numericUpDownSensorNum);
            this.groupBoxAgent.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAgent.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxAgent.Location = new System.Drawing.Point(3, 125);
            this.groupBoxAgent.Name = "groupBoxAgent";
            this.groupBoxAgent.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.groupBoxAgent.Size = new System.Drawing.Size(455, 282);
            this.groupBoxAgent.TabIndex = 18;
            this.groupBoxAgent.TabStop = false;
            this.groupBoxAgent.Text = "Agent Property";
            // 
            // numericUpDownTargetH
            // 
            this.numericUpDownTargetH.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.numericUpDownTargetH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownTargetH.DecimalPlaces = 2;
            this.numericUpDownTargetH.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownTargetH.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownTargetH.Location = new System.Drawing.Point(198, 234);
            this.numericUpDownTargetH.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTargetH.Name = "numericUpDownTargetH";
            this.numericUpDownTargetH.Size = new System.Drawing.Size(107, 26);
            this.numericUpDownTargetH.TabIndex = 24;
            this.numericUpDownTargetH.Value = new decimal(new int[] {
            90,
            0,
            0,
            131072});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 236);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 19);
            this.label8.TabIndex = 23;
            this.label8.Text = "Target h";
            // 
            // numericUpDownOpinionIntroDuration
            // 
            this.numericUpDownOpinionIntroDuration.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.numericUpDownOpinionIntroDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownOpinionIntroDuration.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownOpinionIntroDuration.Location = new System.Drawing.Point(198, 202);
            this.numericUpDownOpinionIntroDuration.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownOpinionIntroDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownOpinionIntroDuration.Name = "numericUpDownOpinionIntroDuration";
            this.numericUpDownOpinionIntroDuration.Size = new System.Drawing.Size(107, 26);
            this.numericUpDownOpinionIntroDuration.TabIndex = 22;
            this.numericUpDownOpinionIntroDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(171, 19);
            this.label6.TabIndex = 21;
            this.label6.Text = "Opinion Intro Dura";
            // 
            // numericUpDownRedSigma
            // 
            this.numericUpDownRedSigma.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.numericUpDownRedSigma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownRedSigma.DecimalPlaces = 2;
            this.numericUpDownRedSigma.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownRedSigma.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownRedSigma.Location = new System.Drawing.Point(198, 131);
            this.numericUpDownRedSigma.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownRedSigma.Name = "numericUpDownRedSigma";
            this.numericUpDownRedSigma.Size = new System.Drawing.Size(107, 26);
            this.numericUpDownRedSigma.TabIndex = 20;
            this.numericUpDownRedSigma.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 19);
            this.label5.TabIndex = 19;
            this.label5.Text = "Red Sigma";
            // 
            // checkBoxSensorRateEnabled
            // 
            this.checkBoxSensorRateEnabled.AutoSize = true;
            this.checkBoxSensorRateEnabled.Location = new System.Drawing.Point(319, 32);
            this.checkBoxSensorRateEnabled.Name = "checkBoxSensorRateEnabled";
            this.checkBoxSensorRateEnabled.Size = new System.Drawing.Size(15, 14);
            this.checkBoxSensorRateEnabled.TabIndex = 18;
            this.checkBoxSensorRateEnabled.UseVisualStyleBackColor = true;
            // 
            // numericUpDownSensorRate
            // 
            this.numericUpDownSensorRate.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.numericUpDownSensorRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownSensorRate.DecimalPlaces = 2;
            this.numericUpDownSensorRate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownSensorRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownSensorRate.Location = new System.Drawing.Point(340, 24);
            this.numericUpDownSensorRate.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSensorRate.Name = "numericUpDownSensorRate";
            this.numericUpDownSensorRate.Size = new System.Drawing.Size(107, 26);
            this.numericUpDownSensorRate.TabIndex = 17;
            // 
            // numericUpDownSensorAccuracy
            // 
            this.numericUpDownSensorAccuracy.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.numericUpDownSensorAccuracy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownSensorAccuracy.DecimalPlaces = 2;
            this.numericUpDownSensorAccuracy.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownSensorAccuracy.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownSensorAccuracy.Location = new System.Drawing.Point(198, 62);
            this.numericUpDownSensorAccuracy.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSensorAccuracy.Name = "numericUpDownSensorAccuracy";
            this.numericUpDownSensorAccuracy.Size = new System.Drawing.Size(107, 26);
            this.numericUpDownSensorAccuracy.TabIndex = 13;
            this.numericUpDownSensorAccuracy.Value = new decimal(new int[] {
            55,
            0,
            0,
            131072});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 64);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(144, 19);
            this.label15.TabIndex = 12;
            this.label15.Text = "Sensor Accuracy";
            // 
            // numericUpDownOpinionIntroRate
            // 
            this.numericUpDownOpinionIntroRate.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.numericUpDownOpinionIntroRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownOpinionIntroRate.DecimalPlaces = 2;
            this.numericUpDownOpinionIntroRate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownOpinionIntroRate.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownOpinionIntroRate.Location = new System.Drawing.Point(198, 166);
            this.numericUpDownOpinionIntroRate.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownOpinionIntroRate.Name = "numericUpDownOpinionIntroRate";
            this.numericUpDownOpinionIntroRate.Size = new System.Drawing.Size(107, 26);
            this.numericUpDownOpinionIntroRate.TabIndex = 11;
            this.numericUpDownOpinionIntroRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // labelOpinionIntroRate
            // 
            this.labelOpinionIntroRate.AutoSize = true;
            this.labelOpinionIntroRate.Location = new System.Drawing.Point(9, 168);
            this.labelOpinionIntroRate.Name = "labelOpinionIntroRate";
            this.labelOpinionIntroRate.Size = new System.Drawing.Size(171, 19);
            this.labelOpinionIntroRate.TabIndex = 10;
            this.labelOpinionIntroRate.Text = "Opinion Intro Rate";
            // 
            // numericUpDownGreenSigma
            // 
            this.numericUpDownGreenSigma.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.numericUpDownGreenSigma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownGreenSigma.DecimalPlaces = 2;
            this.numericUpDownGreenSigma.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownGreenSigma.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownGreenSigma.Location = new System.Drawing.Point(198, 95);
            this.numericUpDownGreenSigma.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownGreenSigma.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownGreenSigma.Name = "numericUpDownGreenSigma";
            this.numericUpDownGreenSigma.Size = new System.Drawing.Size(107, 26);
            this.numericUpDownGreenSigma.TabIndex = 3;
            this.numericUpDownGreenSigma.Value = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            // 
            // labelAATSigma
            // 
            this.labelAATSigma.AutoSize = true;
            this.labelAATSigma.Location = new System.Drawing.Point(9, 97);
            this.labelAATSigma.Name = "labelAATSigma";
            this.labelAATSigma.Size = new System.Drawing.Size(108, 19);
            this.labelAATSigma.TabIndex = 2;
            this.labelAATSigma.Text = "Green Sigma";
            // 
            // labelAATSensorNum
            // 
            this.labelAATSensorNum.AutoSize = true;
            this.labelAATSensorNum.Location = new System.Drawing.Point(9, 26);
            this.labelAATSensorNum.Name = "labelAATSensorNum";
            this.labelAATSensorNum.Size = new System.Drawing.Size(99, 19);
            this.labelAATSensorNum.TabIndex = 7;
            this.labelAATSensorNum.Text = "Sensor Num";
            // 
            // numericUpDownSensorNum
            // 
            this.numericUpDownSensorNum.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.numericUpDownSensorNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownSensorNum.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownSensorNum.Location = new System.Drawing.Point(198, 24);
            this.numericUpDownSensorNum.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownSensorNum.Name = "numericUpDownSensorNum";
            this.numericUpDownSensorNum.Size = new System.Drawing.Size(107, 26);
            this.numericUpDownSensorNum.TabIndex = 6;
            this.numericUpDownSensorNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonGA);
            this.groupBox3.Controls.Add(this.radioButtonOther);
            this.groupBox3.Controls.Add(this.radioButtonAAT);
            this.groupBox3.Controls.Add(this.comboBoxGA);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.comboBoxOtherAlgo);
            this.groupBox3.Controls.Add(this.comboBoxAAT);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox3.Location = new System.Drawing.Point(3, 413);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(375, 149);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Algo List";
            // 
            // radioButtonGA
            // 
            this.radioButtonGA.AutoSize = true;
            this.radioButtonGA.Location = new System.Drawing.Point(13, 108);
            this.radioButtonGA.Name = "radioButtonGA";
            this.radioButtonGA.Size = new System.Drawing.Size(14, 13);
            this.radioButtonGA.TabIndex = 44;
            this.radioButtonGA.UseVisualStyleBackColor = true;
            // 
            // radioButtonOther
            // 
            this.radioButtonOther.AutoSize = true;
            this.radioButtonOther.Location = new System.Drawing.Point(198, 50);
            this.radioButtonOther.Name = "radioButtonOther";
            this.radioButtonOther.Size = new System.Drawing.Size(14, 13);
            this.radioButtonOther.TabIndex = 43;
            this.radioButtonOther.UseVisualStyleBackColor = true;
            // 
            // radioButtonAAT
            // 
            this.radioButtonAAT.AutoSize = true;
            this.radioButtonAAT.Checked = true;
            this.radioButtonAAT.Location = new System.Drawing.Point(13, 50);
            this.radioButtonAAT.Name = "radioButtonAAT";
            this.radioButtonAAT.Size = new System.Drawing.Size(14, 13);
            this.radioButtonAAT.TabIndex = 42;
            this.radioButtonAAT.TabStop = true;
            this.radioButtonAAT.UseVisualStyleBackColor = true;
            // 
            // comboBoxGA
            // 
            this.comboBoxGA.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxGA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxGA.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxGA.FormattingEnabled = true;
            this.comboBoxGA.Items.AddRange(new object[] {
            "GA"});
            this.comboBoxGA.Location = new System.Drawing.Point(31, 102);
            this.comboBoxGA.Name = "comboBoxGA";
            this.comboBoxGA.Size = new System.Drawing.Size(147, 27);
            this.comboBoxGA.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 19);
            this.label4.TabIndex = 40;
            this.label4.Text = "GA";
            // 
            // comboBoxOtherAlgo
            // 
            this.comboBoxOtherAlgo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxOtherAlgo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxOtherAlgo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxOtherAlgo.FormattingEnabled = true;
            this.comboBoxOtherAlgo.Items.AddRange(new object[] {
            "Fool"});
            this.comboBoxOtherAlgo.Location = new System.Drawing.Point(218, 44);
            this.comboBoxOtherAlgo.Name = "comboBoxOtherAlgo";
            this.comboBoxOtherAlgo.Size = new System.Drawing.Size(147, 27);
            this.comboBoxOtherAlgo.TabIndex = 39;
            // 
            // comboBoxAAT
            // 
            this.comboBoxAAT.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxAAT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxAAT.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxAAT.FormattingEnabled = true;
            this.comboBoxAAT.Location = new System.Drawing.Point(31, 44);
            this.comboBoxAAT.Name = "comboBoxAAT";
            this.comboBoxAAT.Size = new System.Drawing.Size(147, 27);
            this.comboBoxAAT.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 36;
            this.label3.Text = "Other";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 19);
            this.label11.TabIndex = 3;
            this.label11.Text = "AAT";
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.numericUpDown4);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(3, 568);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.groupBox2.Size = new System.Drawing.Size(285, 75);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AAT Property";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.numericUpDown4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown4.DecimalPlaces = 2;
            this.numericUpDown4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDown4.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDown4.Location = new System.Drawing.Point(170, 27);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(107, 26);
            this.numericUpDown4.TabIndex = 9;
            this.numericUpDown4.Value = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 19);
            this.label7.TabIndex = 8;
            this.label7.Text = "h_target";
            // 
            // AgentGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "AgentGUI";
            this.Size = new System.Drawing.Size(540, 678);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAgentSeed)).EndInit();
            this.groupBoxAgent.ResumeLayout(false);
            this.groupBoxAgent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTargetH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpinionIntroDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRedSigma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSensorRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSensorAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOpinionIntroRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGreenSigma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSensorNum)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxInitOpinion;
        private System.Windows.Forms.NumericUpDown numericUpDownAgentSeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonGenerateAgentNetwork;
        private System.Windows.Forms.GroupBox groupBoxAgent;
        private System.Windows.Forms.NumericUpDown numericUpDownTargetH;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownOpinionIntroDuration;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownRedSigma;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxSensorRateEnabled;
        private System.Windows.Forms.NumericUpDown numericUpDownSensorRate;
        private System.Windows.Forms.NumericUpDown numericUpDownSensorAccuracy;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericUpDownOpinionIntroRate;
        private System.Windows.Forms.Label labelOpinionIntroRate;
        private System.Windows.Forms.NumericUpDown numericUpDownGreenSigma;
        private System.Windows.Forms.Label labelAATSigma;
        private System.Windows.Forms.Label labelAATSensorNum;
        private System.Windows.Forms.NumericUpDown numericUpDownSensorNum;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonGA;
        private System.Windows.Forms.RadioButton radioButtonOther;
        private System.Windows.Forms.RadioButton radioButtonAAT;
        private System.Windows.Forms.ComboBox comboBoxGA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxOtherAlgo;
        private System.Windows.Forms.ComboBox comboBoxAAT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label7;
    }
}
