namespace OSM2019.GUI
{
    partial class LearningGUI
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
            this.numericUpDownLearningSeed = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDownAllSteps = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownAllRounds = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonRunLearning = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLearningSeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAllSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAllRounds)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(461, 466);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownLearningSeed);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.numericUpDownAllSteps);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.numericUpDownAllRounds);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.buttonRunLearning);
            this.groupBox2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(222, 168);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Learning Control";
            // 
            // numericUpDownLearningSeed
            // 
            this.numericUpDownLearningSeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownLearningSeed.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownLearningSeed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownLearningSeed.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownLearningSeed.Location = new System.Drawing.Point(141, 133);
            this.numericUpDownLearningSeed.Name = "numericUpDownLearningSeed";
            this.numericUpDownLearningSeed.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownLearningSeed.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 132);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 19);
            this.label11.TabIndex = 20;
            this.label11.Text = "Seed:";
            // 
            // numericUpDownAllSteps
            // 
            this.numericUpDownAllSteps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownAllSteps.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownAllSteps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownAllSteps.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownAllSteps.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownAllSteps.Location = new System.Drawing.Point(141, 102);
            this.numericUpDownAllSteps.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownAllSteps.Name = "numericUpDownAllSteps";
            this.numericUpDownAllSteps.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownAllSteps.TabIndex = 17;
            this.numericUpDownAllSteps.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 19);
            this.label9.TabIndex = 16;
            this.label9.Text = "Step:";
            // 
            // numericUpDownAllRounds
            // 
            this.numericUpDownAllRounds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownAllRounds.BackColor = System.Drawing.SystemColors.ControlDark;
            this.numericUpDownAllRounds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDownAllRounds.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.numericUpDownAllRounds.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownAllRounds.Location = new System.Drawing.Point(141, 68);
            this.numericUpDownAllRounds.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownAllRounds.Name = "numericUpDownAllRounds";
            this.numericUpDownAllRounds.Size = new System.Drawing.Size(69, 22);
            this.numericUpDownAllRounds.TabIndex = 8;
            this.numericUpDownAllRounds.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 19);
            this.label12.TabIndex = 6;
            this.label12.Text = "All Rounds:";
            // 
            // buttonRunLearning
            // 
            this.buttonRunLearning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRunLearning.Location = new System.Drawing.Point(6, 25);
            this.buttonRunLearning.Name = "buttonRunLearning";
            this.buttonRunLearning.Size = new System.Drawing.Size(91, 32);
            this.buttonRunLearning.TabIndex = 0;
            this.buttonRunLearning.Text = "Run";
            this.buttonRunLearning.UseVisualStyleBackColor = false;
            // 
            // LearningGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "LearningGUI";
            this.Size = new System.Drawing.Size(467, 472);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLearningSeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAllSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAllRounds)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownLearningSeed;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDownAllSteps;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownAllRounds;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonRunLearning;
    }
}
