namespace GoolagScanner
{
    partial class SummaryReport
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("", 1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("", 2);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SummaryReport));
            this.OkayButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelScanDuration = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelScanStart = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.hostScannedBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sumListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.resultStates = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OkayButton
            // 
            this.OkayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OkayButton.Location = new System.Drawing.Point(179, 190);
            this.OkayButton.Name = "OkayButton";
            this.OkayButton.Size = new System.Drawing.Size(102, 24);
            this.OkayButton.TabIndex = 0;
            this.OkayButton.Text = "OK";
            this.OkayButton.UseVisualStyleBackColor = true;
            this.OkayButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelScanDuration);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.labelScanStart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.hostScannedBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 89);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // labelScanDuration
            // 
            this.labelScanDuration.AutoSize = true;
            this.labelScanDuration.Location = new System.Drawing.Point(96, 62);
            this.labelScanDuration.Name = "labelScanDuration";
            this.labelScanDuration.Size = new System.Drawing.Size(94, 13);
            this.labelScanDuration.TabIndex = 5;
            this.labelScanDuration.Text = "labelScanDuration";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Scan duration";
            // 
            // labelScanStart
            // 
            this.labelScanStart.AutoSize = true;
            this.labelScanStart.Location = new System.Drawing.Point(96, 40);
            this.labelScanStart.Name = "labelScanStart";
            this.labelScanStart.Size = new System.Drawing.Size(76, 13);
            this.labelScanStart.TabIndex = 3;
            this.labelScanStart.Text = "labelScanStart";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Scan started";
            // 
            // hostScannedBox
            // 
            this.hostScannedBox.Location = new System.Drawing.Point(99, 13);
            this.hostScannedBox.Name = "hostScannedBox";
            this.hostScannedBox.ReadOnly = true;
            this.hostScannedBox.Size = new System.Drawing.Size(342, 20);
            this.hostScannedBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host scanned";
            // 
            // sumListView
            // 
            this.sumListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.sumListView.FullRowSelect = true;
            this.sumListView.GridLines = true;
            this.sumListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.sumListView.Location = new System.Drawing.Point(6, 103);
            this.sumListView.Name = "sumListView";
            this.sumListView.Size = new System.Drawing.Size(449, 72);
            this.sumListView.SmallImageList = this.resultStates;
            this.sumListView.TabIndex = 2;
            this.sumListView.UseCompatibleStateImageBehavior = false;
            this.sumListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 26;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 325;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 75;
            // 
            // resultStates
            // 
            this.resultStates.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("resultStates.ImageStream")));
            this.resultStates.TransparentColor = System.Drawing.Color.Transparent;
            this.resultStates.Images.SetKeyName(0, "haken_blue.bmp");
            this.resultStates.Images.SetKeyName(1, "warn_yellow.bmp");
            this.resultStates.Images.SetKeyName(2, "red_cross_ball.bmp");
            // 
            // SummaryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 226);
            this.Controls.Add(this.sumListView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OkayButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SummaryReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Summary";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkayButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelScanDuration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelScanStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox hostScannedBox;
        private System.Windows.Forms.ListView sumListView;
        private System.Windows.Forms.ImageList resultStates;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}