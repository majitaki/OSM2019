namespace OSM2019.GUI
{
    partial class ExperimentGUI
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxExp = new System.Windows.Forms.ComboBox();
            this.buttonRunExperiment = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(525, 417);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.comboBoxExp);
            this.groupBox2.Controls.Add(this.buttonRunExperiment);
            this.groupBox2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(476, 115);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Experiment Control";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 19);
            this.label2.TabIndex = 40;
            this.label2.Text = "Experiment";
            // 
            // comboBoxExp
            // 
            this.comboBoxExp.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxExp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxExp.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxExp.FormattingEnabled = true;
            this.comboBoxExp.Location = new System.Drawing.Point(107, 72);
            this.comboBoxExp.Name = "comboBoxExp";
            this.comboBoxExp.Size = new System.Drawing.Size(352, 27);
            this.comboBoxExp.TabIndex = 39;
            // 
            // buttonRunExperiment
            // 
            this.buttonRunExperiment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRunExperiment.Location = new System.Drawing.Point(6, 25);
            this.buttonRunExperiment.Name = "buttonRunExperiment";
            this.buttonRunExperiment.Size = new System.Drawing.Size(91, 32);
            this.buttonRunExperiment.TabIndex = 0;
            this.buttonRunExperiment.Text = "Run";
            this.buttonRunExperiment.UseVisualStyleBackColor = false;
            // 
            // ExperimentGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ExperimentGUI";
            this.Size = new System.Drawing.Size(536, 586);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxExp;
        private System.Windows.Forms.Button buttonRunExperiment;
    }
}
