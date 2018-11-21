namespace OSM2019.GUI
{
    partial class AnimationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonUpdateLayout = new System.Windows.Forms.Button();
            this.trackBarRadius = new System.Windows.Forms.TrackBar();
            this.comboBoxLayout = new System.Windows.Forms.ComboBox();
            this.pictureBoxAnimation = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnimation)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonUpdateLayout
            // 
            this.buttonUpdateLayout.AutoSize = true;
            this.buttonUpdateLayout.BackColor = System.Drawing.Color.Transparent;
            this.buttonUpdateLayout.FlatAppearance.BorderSize = 0;
            this.buttonUpdateLayout.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonUpdateLayout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUpdateLayout.Image = global::OSM2019.Properties.Resources.icon_update;
            this.buttonUpdateLayout.Location = new System.Drawing.Point(3, 3);
            this.buttonUpdateLayout.Name = "buttonUpdateLayout";
            this.buttonUpdateLayout.Size = new System.Drawing.Size(54, 54);
            this.buttonUpdateLayout.TabIndex = 38;
            this.buttonUpdateLayout.UseVisualStyleBackColor = false;
            this.buttonUpdateLayout.Click += new System.EventHandler(this.buttonUpdateLayout_Click);
            // 
            // trackBarRadius
            // 
            this.trackBarRadius.BackColor = System.Drawing.SystemColors.ControlDark;
            this.trackBarRadius.LargeChange = 1;
            this.trackBarRadius.Location = new System.Drawing.Point(406, 3);
            this.trackBarRadius.Maximum = 20;
            this.trackBarRadius.Minimum = 1;
            this.trackBarRadius.Name = "trackBarRadius";
            this.trackBarRadius.Size = new System.Drawing.Size(151, 45);
            this.trackBarRadius.TabIndex = 40;
            this.trackBarRadius.Value = 10;
            // 
            // comboBoxLayout
            // 
            this.comboBoxLayout.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLayout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxLayout.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxLayout.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBoxLayout.FormattingEnabled = true;
            this.comboBoxLayout.Location = new System.Drawing.Point(63, 16);
            this.comboBoxLayout.Name = "comboBoxLayout";
            this.comboBoxLayout.Size = new System.Drawing.Size(252, 27);
            this.comboBoxLayout.TabIndex = 39;
            // 
            // pictureBoxAnimation
            // 
            this.pictureBoxAnimation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxAnimation.BackColor = System.Drawing.Color.White;
            this.pictureBoxAnimation.Location = new System.Drawing.Point(0, 63);
            this.pictureBoxAnimation.Name = "pictureBoxAnimation";
            this.pictureBoxAnimation.Size = new System.Drawing.Size(820, 542);
            this.pictureBoxAnimation.TabIndex = 29;
            this.pictureBoxAnimation.TabStop = false;
            this.pictureBoxAnimation.Click += new System.EventHandler(this.pictureBoxAnimation_Click);
            this.pictureBoxAnimation.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxAnimation_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.trackBar2);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.trackBarRadius);
            this.panel1.Controls.Add(this.buttonUpdateLayout);
            this.panel1.Controls.Add(this.comboBoxLayout);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(820, 69);
            this.panel1.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(563, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 24);
            this.label3.TabIndex = 45;
            this.label3.Text = "Rotation";
            // 
            // trackBar2
            // 
            this.trackBar2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.trackBar2.LargeChange = 1;
            this.trackBar2.Location = new System.Drawing.Point(641, 3);
            this.trackBar2.Maximum = 20;
            this.trackBar2.Minimum = 1;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(145, 45);
            this.trackBar2.TabIndex = 44;
            this.trackBar2.Value = 1;
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(406, 36);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(151, 45);
            this.trackBar1.TabIndex = 43;
            this.trackBar1.Value = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(321, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 24);
            this.label2.TabIndex = 42;
            this.label2.Text = " Distance";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(342, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 24);
            this.label1.TabIndex = 41;
            this.label1.Text = "Radius";
            // 
            // AnimationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 600);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBoxAnimation);
            this.Name = "AnimationForm";
            this.Text = "AnimationForm";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnimation)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxAnimation;
        private System.Windows.Forms.Button buttonUpdateLayout;
        private System.Windows.Forms.TrackBar trackBarRadius;
        private System.Windows.Forms.ComboBox comboBoxLayout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar2;
    }
}