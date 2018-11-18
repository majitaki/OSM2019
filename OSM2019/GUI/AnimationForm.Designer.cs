﻿namespace OSM2019.GUI
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAnimation)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.trackBarRadius.Location = new System.Drawing.Point(438, 3);
            this.trackBarRadius.Maximum = 20;
            this.trackBarRadius.Minimum = 1;
            this.trackBarRadius.Name = "trackBarRadius";
            this.trackBarRadius.Size = new System.Drawing.Size(190, 45);
            this.trackBarRadius.TabIndex = 40;
            this.trackBarRadius.Value = 1;
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
            this.pictureBoxAnimation.BackColor = System.Drawing.Color.White;
            this.pictureBoxAnimation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxAnimation.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxAnimation.Name = "pictureBoxAnimation";
            this.pictureBoxAnimation.Size = new System.Drawing.Size(820, 600);
            this.pictureBoxAnimation.TabIndex = 29;
            this.pictureBoxAnimation.TabStop = false;
            this.pictureBoxAnimation.Click += new System.EventHandler(this.pictureBoxAnimation_Click);
            this.pictureBoxAnimation.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxAnimation_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(331, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 24);
            this.label1.TabIndex = 41;
            this.label1.Text = "Agent Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(343, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 24);
            this.label2.TabIndex = 42;
            this.label2.Text = " Distance";
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(438, 33);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(190, 45);
            this.trackBar1.TabIndex = 43;
            this.trackBar1.Value = 1;
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
    }
}