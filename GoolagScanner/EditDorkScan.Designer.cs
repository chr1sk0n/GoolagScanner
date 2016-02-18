namespace GoolagScanner
{
    partial class EditDorkScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDorkScan));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.QueryTextBox = new System.Windows.Forms.TextBox();
            this.Revert = new System.Windows.Forms.Button();
            this.CancelEditButtton = new System.Windows.Forms.Button();
            this.Scanbutton = new System.Windows.Forms.Button();
            this.DorkNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dork";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Query";
            // 
            // QueryTextBox
            // 
            this.QueryTextBox.Location = new System.Drawing.Point(62, 39);
            this.QueryTextBox.Name = "QueryTextBox";
            this.QueryTextBox.Size = new System.Drawing.Size(337, 20);
            this.QueryTextBox.TabIndex = 2;
            // 
            // Revert
            // 
            this.Revert.Location = new System.Drawing.Point(348, 65);
            this.Revert.Name = "Revert";
            this.Revert.Size = new System.Drawing.Size(51, 21);
            this.Revert.TabIndex = 3;
            this.Revert.Text = "Revert";
            this.Revert.UseVisualStyleBackColor = true;
            this.Revert.Click += new System.EventHandler(this.Revert_Click);
            // 
            // CancelEditButtton
            // 
            this.CancelEditButtton.Location = new System.Drawing.Point(323, 93);
            this.CancelEditButtton.Name = "CancelEditButtton";
            this.CancelEditButtton.Size = new System.Drawing.Size(77, 26);
            this.CancelEditButtton.TabIndex = 4;
            this.CancelEditButtton.Text = "Cancel";
            this.CancelEditButtton.UseVisualStyleBackColor = true;
            this.CancelEditButtton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // Scanbutton
            // 
            this.Scanbutton.Location = new System.Drawing.Point(240, 93);
            this.Scanbutton.Name = "Scanbutton";
            this.Scanbutton.Size = new System.Drawing.Size(77, 26);
            this.Scanbutton.TabIndex = 5;
            this.Scanbutton.Text = "Scan";
            this.Scanbutton.UseVisualStyleBackColor = true;
            this.Scanbutton.Click += new System.EventHandler(this.Scanbutton_Click);
            // 
            // DorkNameLabel
            // 
            this.DorkNameLabel.AutoSize = true;
            this.DorkNameLabel.Location = new System.Drawing.Point(59, 12);
            this.DorkNameLabel.Name = "DorkNameLabel";
            this.DorkNameLabel.Size = new System.Drawing.Size(35, 13);
            this.DorkNameLabel.TabIndex = 6;
            this.DorkNameLabel.Text = "label3";
            // 
            // EditDorkScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 129);
            this.Controls.Add(this.DorkNameLabel);
            this.Controls.Add(this.Scanbutton);
            this.Controls.Add(this.CancelEditButtton);
            this.Controls.Add(this.Revert);
            this.Controls.Add(this.QueryTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditDorkScan";
            this.Text = "Edit Dork";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox QueryTextBox;
        private System.Windows.Forms.Button Revert;
        private System.Windows.Forms.Button CancelEditButtton;
        private System.Windows.Forms.Button Scanbutton;
        private System.Windows.Forms.Label DorkNameLabel;
    }
}