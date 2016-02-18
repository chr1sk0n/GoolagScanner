namespace GoolagScanner
{
    partial class ScanningDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanningDialog));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AbortButton = new System.Windows.Forms.Button();
            this.DialogProgressBar = new System.Windows.Forms.ProgressBar();
            this.PercentageOutput = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GoolagScanner.Properties.Resources.throbber;
            this.pictureBox1.Location = new System.Drawing.Point(8, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // AbortButton
            // 
            this.AbortButton.BackColor = System.Drawing.SystemColors.Control;
            this.AbortButton.Location = new System.Drawing.Point(311, 12);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(90, 26);
            this.AbortButton.TabIndex = 1;
            this.AbortButton.Text = "Abort";
            this.AbortButton.UseVisualStyleBackColor = false;
            this.AbortButton.Click += new System.EventHandler(this.AbortButton_Click);
            // 
            // DialogProgressBar
            // 
            this.DialogProgressBar.Location = new System.Drawing.Point(60, 18);
            this.DialogProgressBar.Name = "DialogProgressBar";
            this.DialogProgressBar.Size = new System.Drawing.Size(238, 15);
            this.DialogProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.DialogProgressBar.TabIndex = 2;
            // 
            // PercentageOutput
            // 
            this.PercentageOutput.AutoSize = true;
            this.PercentageOutput.Location = new System.Drawing.Point(168, 36);
            this.PercentageOutput.Name = "PercentageOutput";
            this.PercentageOutput.Size = new System.Drawing.Size(21, 13);
            this.PercentageOutput.TabIndex = 3;
            this.PercentageOutput.Text = "0%";
            // 
            // ScanningDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(411, 52);
            this.ControlBox = false;
            this.Controls.Add(this.PercentageOutput);
            this.Controls.Add(this.DialogProgressBar);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScanningDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scanning...";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button AbortButton;
        private System.Windows.Forms.ProgressBar DialogProgressBar;
        private System.Windows.Forms.Label PercentageOutput;
    }
}