namespace OSM2019.GUI
{
    partial class GraphGUI
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownGraphSeed = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNodeNum = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonGenerateGraph = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonOther = new System.Windows.Forms.RadioButton();
            this.comboBoxOther = new System.Windows.Forms.ComboBox();
            this.radioButtonSmallWorld = new System.Windows.Forms.RadioButton();
            this.radioButtonScaleFree = new System.Windows.Forms.RadioButton();
            this.radioButtonRandom = new System.Windows.Forms.RadioButton();
            this.comboBoxRandom = new System.Windows.Forms.ComboBox();
            this.comboBoxScaleFree = new System.Windows.Forms.ComboBox();
            this.comboBoxSmallWorld = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBoxWSproperty = new System.Windows.Forms.GroupBox();
            this.numericUpDownWSdegree = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWSrewirep = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBoxBAproperty = new System.Windows.Forms.GroupBox();
            this.numericUpDownBAattachEdge = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBoxPCproperty = new System.Windows.Forms.GroupBox();
            this.numericUpDownPCaddTriP = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.numericUpDownPCrndEdge = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownEdgeCreationP = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numericUpDownGridN = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownGridM = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGraphSeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNodeNum)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBoxWSproperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWSdegree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWSrewirep)).BeginInit();
            this.groupBoxBAproperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBAattachEdge)).BeginInit();
            this.groupBoxPCproperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPCaddTriP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPCrndEdge)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeCreationP)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridM)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxWSproperty);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxBAproperty);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxPCproperty);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(682, 668);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownGraphSeed);
            this.groupBox2.Controls.Add(this.numericUpDownNodeNum);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.buttonGenerateGraph);
            this.groupBox2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 119);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Graph Generation";
            // 
            // numericUpDownGraphSeed
            // 
            this.numericUpDownGraphSeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownGraphSeed.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownGraphSeed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownGraphSeed.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownGraphSeed.Location = new System.Drawing.Point(178, 90);
            this.numericUpDownGraphSeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownGraphSeed.Name = "numericUpDownGraphSeed";
            this.numericUpDownGraphSeed.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownGraphSeed.TabIndex = 8;
            this.numericUpDownGraphSeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownNodeNum
            // 
            this.numericUpDownNodeNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownNodeNum.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownNodeNum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownNodeNum.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownNodeNum.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownNodeNum.Location = new System.Drawing.Point(178, 64);
            this.numericUpDownNodeNum.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownNodeNum.Name = "numericUpDownNodeNum";
            this.numericUpDownNodeNum.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownNodeNum.TabIndex = 7;
            this.numericUpDownNodeNum.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 89);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 19);
            this.label10.TabIndex = 6;
            this.label10.Text = "Seed:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 19);
            this.label9.TabIndex = 5;
            this.label9.Text = "Node Num:";
            // 
            // buttonGenerateGraph
            // 
            this.buttonGenerateGraph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGenerateGraph.Location = new System.Drawing.Point(6, 25);
            this.buttonGenerateGraph.Name = "buttonGenerateGraph";
            this.buttonGenerateGraph.Size = new System.Drawing.Size(93, 32);
            this.buttonGenerateGraph.TabIndex = 0;
            this.buttonGenerateGraph.Text = "Generate";
            this.buttonGenerateGraph.UseVisualStyleBackColor = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonOther);
            this.groupBox3.Controls.Add(this.comboBoxOther);
            this.groupBox3.Controls.Add(this.radioButtonSmallWorld);
            this.groupBox3.Controls.Add(this.radioButtonScaleFree);
            this.groupBox3.Controls.Add(this.radioButtonRandom);
            this.groupBox3.Controls.Add(this.comboBoxRandom);
            this.groupBox3.Controls.Add(this.comboBoxScaleFree);
            this.groupBox3.Controls.Add(this.comboBoxSmallWorld);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox3.Location = new System.Drawing.Point(268, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(365, 163);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Graph List";
            // 
            // radioButtonOther
            // 
            this.radioButtonOther.AutoSize = true;
            this.radioButtonOther.Location = new System.Drawing.Point(194, 127);
            this.radioButtonOther.Name = "radioButtonOther";
            this.radioButtonOther.Size = new System.Drawing.Size(14, 13);
            this.radioButtonOther.TabIndex = 31;
            this.radioButtonOther.TabStop = true;
            this.radioButtonOther.UseVisualStyleBackColor = true;
            // 
            // comboBoxOther
            // 
            this.comboBoxOther.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxOther.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxOther.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxOther.FormattingEnabled = true;
            this.comboBoxOther.Location = new System.Drawing.Point(212, 121);
            this.comboBoxOther.Name = "comboBoxOther";
            this.comboBoxOther.Size = new System.Drawing.Size(147, 27);
            this.comboBoxOther.TabIndex = 30;
            // 
            // radioButtonSmallWorld
            // 
            this.radioButtonSmallWorld.AutoSize = true;
            this.radioButtonSmallWorld.Checked = true;
            this.radioButtonSmallWorld.Location = new System.Drawing.Point(10, 60);
            this.radioButtonSmallWorld.Name = "radioButtonSmallWorld";
            this.radioButtonSmallWorld.Size = new System.Drawing.Size(14, 13);
            this.radioButtonSmallWorld.TabIndex = 29;
            this.radioButtonSmallWorld.TabStop = true;
            this.radioButtonSmallWorld.UseVisualStyleBackColor = true;
            // 
            // radioButtonScaleFree
            // 
            this.radioButtonScaleFree.AutoSize = true;
            this.radioButtonScaleFree.Location = new System.Drawing.Point(10, 127);
            this.radioButtonScaleFree.Name = "radioButtonScaleFree";
            this.radioButtonScaleFree.Size = new System.Drawing.Size(14, 13);
            this.radioButtonScaleFree.TabIndex = 28;
            this.radioButtonScaleFree.TabStop = true;
            this.radioButtonScaleFree.UseVisualStyleBackColor = true;
            // 
            // radioButtonRandom
            // 
            this.radioButtonRandom.AutoSize = true;
            this.radioButtonRandom.Location = new System.Drawing.Point(193, 60);
            this.radioButtonRandom.Name = "radioButtonRandom";
            this.radioButtonRandom.Size = new System.Drawing.Size(14, 13);
            this.radioButtonRandom.TabIndex = 27;
            this.radioButtonRandom.TabStop = true;
            this.radioButtonRandom.UseVisualStyleBackColor = true;
            // 
            // comboBoxRandom
            // 
            this.comboBoxRandom.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxRandom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxRandom.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxRandom.FormattingEnabled = true;
            this.comboBoxRandom.Location = new System.Drawing.Point(212, 54);
            this.comboBoxRandom.Name = "comboBoxRandom";
            this.comboBoxRandom.Size = new System.Drawing.Size(147, 27);
            this.comboBoxRandom.TabIndex = 25;
            // 
            // comboBoxScaleFree
            // 
            this.comboBoxScaleFree.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxScaleFree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxScaleFree.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxScaleFree.FormattingEnabled = true;
            this.comboBoxScaleFree.Location = new System.Drawing.Point(28, 121);
            this.comboBoxScaleFree.Name = "comboBoxScaleFree";
            this.comboBoxScaleFree.Size = new System.Drawing.Size(147, 27);
            this.comboBoxScaleFree.TabIndex = 24;
            // 
            // comboBoxSmallWorld
            // 
            this.comboBoxSmallWorld.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxSmallWorld.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxSmallWorld.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxSmallWorld.FormattingEnabled = true;
            this.comboBoxSmallWorld.Location = new System.Drawing.Point(28, 54);
            this.comboBoxSmallWorld.Name = "comboBoxSmallWorld";
            this.comboBoxSmallWorld.Size = new System.Drawing.Size(147, 27);
            this.comboBoxSmallWorld.TabIndex = 23;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(208, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 19);
            this.label14.TabIndex = 13;
            this.label14.Text = "Random";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(208, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 19);
            this.label13.TabIndex = 9;
            this.label13.Text = "Other";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 19);
            this.label12.TabIndex = 6;
            this.label12.Text = "Scale Free";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 32);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 19);
            this.label11.TabIndex = 3;
            this.label11.Text = "Small World";
            // 
            // groupBoxWSproperty
            // 
            this.groupBoxWSproperty.Controls.Add(this.numericUpDownWSdegree);
            this.groupBoxWSproperty.Controls.Add(this.numericUpDownWSrewirep);
            this.groupBoxWSproperty.Controls.Add(this.label17);
            this.groupBoxWSproperty.Controls.Add(this.label18);
            this.groupBoxWSproperty.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxWSproperty.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxWSproperty.Location = new System.Drawing.Point(3, 172);
            this.groupBoxWSproperty.Name = "groupBoxWSproperty";
            this.groupBoxWSproperty.Size = new System.Drawing.Size(298, 92);
            this.groupBoxWSproperty.TabIndex = 18;
            this.groupBoxWSproperty.TabStop = false;
            this.groupBoxWSproperty.Text = "WS Property";
            // 
            // numericUpDownWSdegree
            // 
            this.numericUpDownWSdegree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownWSdegree.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownWSdegree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownWSdegree.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownWSdegree.Location = new System.Drawing.Point(217, 59);
            this.numericUpDownWSdegree.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWSdegree.Name = "numericUpDownWSdegree";
            this.numericUpDownWSdegree.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownWSdegree.TabIndex = 8;
            this.numericUpDownWSdegree.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // numericUpDownWSrewirep
            // 
            this.numericUpDownWSrewirep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownWSrewirep.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownWSrewirep.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownWSrewirep.DecimalPlaces = 3;
            this.numericUpDownWSrewirep.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownWSrewirep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownWSrewirep.Location = new System.Drawing.Point(217, 33);
            this.numericUpDownWSrewirep.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWSrewirep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownWSrewirep.Name = "numericUpDownWSrewirep";
            this.numericUpDownWSrewirep.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownWSrewirep.TabIndex = 7;
            this.numericUpDownWSrewirep.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 58);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(99, 19);
            this.label17.TabIndex = 6;
            this.label17.Text = "Degree<k>:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 32);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(99, 19);
            this.label18.TabIndex = 5;
            this.label18.Text = "Rewrite P:";
            // 
            // groupBoxBAproperty
            // 
            this.groupBoxBAproperty.Controls.Add(this.numericUpDownBAattachEdge);
            this.groupBoxBAproperty.Controls.Add(this.label16);
            this.groupBoxBAproperty.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxBAproperty.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxBAproperty.Location = new System.Drawing.Point(307, 172);
            this.groupBoxBAproperty.Name = "groupBoxBAproperty";
            this.groupBoxBAproperty.Size = new System.Drawing.Size(298, 64);
            this.groupBoxBAproperty.TabIndex = 17;
            this.groupBoxBAproperty.TabStop = false;
            this.groupBoxBAproperty.Text = "BA Property";
            // 
            // numericUpDownBAattachEdge
            // 
            this.numericUpDownBAattachEdge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownBAattachEdge.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownBAattachEdge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownBAattachEdge.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownBAattachEdge.Location = new System.Drawing.Point(223, 33);
            this.numericUpDownBAattachEdge.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBAattachEdge.Name = "numericUpDownBAattachEdge";
            this.numericUpDownBAattachEdge.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownBAattachEdge.TabIndex = 7;
            this.numericUpDownBAattachEdge.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 32);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(153, 19);
            this.label16.TabIndex = 5;
            this.label16.Text = "Attach Edge Num:";
            // 
            // groupBoxPCproperty
            // 
            this.groupBoxPCproperty.Controls.Add(this.numericUpDownPCaddTriP);
            this.groupBoxPCproperty.Controls.Add(this.label19);
            this.groupBoxPCproperty.Controls.Add(this.numericUpDownPCrndEdge);
            this.groupBoxPCproperty.Controls.Add(this.label15);
            this.groupBoxPCproperty.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPCproperty.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxPCproperty.Location = new System.Drawing.Point(3, 270);
            this.groupBoxPCproperty.Name = "groupBoxPCproperty";
            this.groupBoxPCproperty.Size = new System.Drawing.Size(298, 98);
            this.groupBoxPCproperty.TabIndex = 19;
            this.groupBoxPCproperty.TabStop = false;
            this.groupBoxPCproperty.Text = "PC Property";
            // 
            // numericUpDownPCaddTriP
            // 
            this.numericUpDownPCaddTriP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownPCaddTriP.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownPCaddTriP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownPCaddTriP.DecimalPlaces = 3;
            this.numericUpDownPCaddTriP.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownPCaddTriP.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownPCaddTriP.Location = new System.Drawing.Point(223, 62);
            this.numericUpDownPCaddTriP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownPCaddTriP.Name = "numericUpDownPCaddTriP";
            this.numericUpDownPCaddTriP.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownPCaddTriP.TabIndex = 9;
            this.numericUpDownPCaddTriP.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 61);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(144, 19);
            this.label19.TabIndex = 8;
            this.label19.Text = "Add Triangle P:";
            // 
            // numericUpDownPCrndEdge
            // 
            this.numericUpDownPCrndEdge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownPCrndEdge.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownPCrndEdge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownPCrndEdge.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownPCrndEdge.Location = new System.Drawing.Point(223, 33);
            this.numericUpDownPCrndEdge.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPCrndEdge.Name = "numericUpDownPCrndEdge";
            this.numericUpDownPCrndEdge.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownPCrndEdge.TabIndex = 7;
            this.numericUpDownPCrndEdge.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 32);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(126, 19);
            this.label15.TabIndex = 5;
            this.label15.Text = "Rnd Edge Num:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownEdgeCreationP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(307, 270);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 64);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ER Property";
            // 
            // numericUpDownEdgeCreationP
            // 
            this.numericUpDownEdgeCreationP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownEdgeCreationP.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownEdgeCreationP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownEdgeCreationP.DecimalPlaces = 3;
            this.numericUpDownEdgeCreationP.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownEdgeCreationP.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownEdgeCreationP.Location = new System.Drawing.Point(217, 33);
            this.numericUpDownEdgeCreationP.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEdgeCreationP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownEdgeCreationP.Name = "numericUpDownEdgeCreationP";
            this.numericUpDownEdgeCreationP.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownEdgeCreationP.TabIndex = 7;
            this.numericUpDownEdgeCreationP.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Edge Creation P";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numericUpDownGridN);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.numericUpDownGridM);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox4.Location = new System.Drawing.Point(3, 374);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(298, 79);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Lattice Property";
            // 
            // numericUpDownGridN
            // 
            this.numericUpDownGridN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownGridN.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownGridN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownGridN.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownGridN.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownGridN.Location = new System.Drawing.Point(223, 49);
            this.numericUpDownGridN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownGridN.Name = "numericUpDownGridN";
            this.numericUpDownGridN.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownGridN.TabIndex = 9;
            this.numericUpDownGridN.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "N Node Num";
            // 
            // numericUpDownGridM
            // 
            this.numericUpDownGridM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownGridM.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownGridM.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownGridM.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownGridM.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownGridM.Location = new System.Drawing.Point(223, 21);
            this.numericUpDownGridM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownGridM.Name = "numericUpDownGridM";
            this.numericUpDownGridM.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownGridM.TabIndex = 7;
            this.numericUpDownGridM.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "M Node Num";
            // 
            // GraphGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "GraphGUI";
            this.Size = new System.Drawing.Size(691, 679);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGraphSeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNodeNum)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxWSproperty.ResumeLayout(false);
            this.groupBoxWSproperty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWSdegree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWSrewirep)).EndInit();
            this.groupBoxBAproperty.ResumeLayout(false);
            this.groupBoxBAproperty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBAattachEdge)).EndInit();
            this.groupBoxPCproperty.ResumeLayout(false);
            this.groupBoxPCproperty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPCaddTriP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPCrndEdge)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeCreationP)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownGraphSeed;
        private System.Windows.Forms.NumericUpDown numericUpDownNodeNum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonGenerateGraph;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonOther;
        private System.Windows.Forms.ComboBox comboBoxOther;
        private System.Windows.Forms.RadioButton radioButtonSmallWorld;
        private System.Windows.Forms.RadioButton radioButtonScaleFree;
        private System.Windows.Forms.RadioButton radioButtonRandom;
        private System.Windows.Forms.ComboBox comboBoxRandom;
        private System.Windows.Forms.ComboBox comboBoxScaleFree;
        private System.Windows.Forms.ComboBox comboBoxSmallWorld;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBoxWSproperty;
        private System.Windows.Forms.NumericUpDown numericUpDownWSdegree;
        private System.Windows.Forms.NumericUpDown numericUpDownWSrewirep;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBoxBAproperty;
        private System.Windows.Forms.NumericUpDown numericUpDownBAattachEdge;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBoxPCproperty;
        private System.Windows.Forms.NumericUpDown numericUpDownPCaddTriP;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown numericUpDownPCrndEdge;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownEdgeCreationP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numericUpDownGridN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownGridM;
        private System.Windows.Forms.Label label1;
    }
}
