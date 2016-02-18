namespace GoolagScanner
{
    partial class Options
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
            this.saveButton = new System.Windows.Forms.Button();
            this.abortButton = new System.Windows.Forms.Button();
            this.OptionsTab = new System.Windows.Forms.TabControl();
            this.Scanner = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.UseSysProxyCB = new System.Windows.Forms.CheckBox();
            this.ProxyText = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RandomOrderCB = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.MimicBrowserTB = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.BlockDetectComboBox = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.ParallelScansTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.showProgressDialogCB = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.freeScanCB = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.requestPages = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.stealthTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.summaryCheck = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.timeOutTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.warnScanTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Miscelaneous = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.showSplashCheckBox = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.UseSysBrowserCB = new System.Windows.Forms.CheckBox();
            this.browseBrowser = new System.Windows.Forms.Button();
            this.preferredBrowserTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.browseDorkFile = new System.Windows.Forms.Button();
            this.dorkFileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OptionsTab.SuspendLayout();
            this.Scanner.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.Miscelaneous.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(240, 415);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(92, 26);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // abortButton
            // 
            this.abortButton.Location = new System.Drawing.Point(345, 415);
            this.abortButton.Name = "abortButton";
            this.abortButton.Size = new System.Drawing.Size(92, 26);
            this.abortButton.TabIndex = 1;
            this.abortButton.Text = "Cancel";
            this.abortButton.UseVisualStyleBackColor = true;
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
            // 
            // OptionsTab
            // 
            this.OptionsTab.Controls.Add(this.Scanner);
            this.OptionsTab.Controls.Add(this.Miscelaneous);
            this.OptionsTab.Location = new System.Drawing.Point(3, 5);
            this.OptionsTab.Name = "OptionsTab";
            this.OptionsTab.SelectedIndex = 0;
            this.OptionsTab.Size = new System.Drawing.Size(435, 396);
            this.OptionsTab.TabIndex = 6;
            // 
            // Scanner
            // 
            this.Scanner.Controls.Add(this.groupBox5);
            this.Scanner.Controls.Add(this.groupBox3);
            this.Scanner.Location = new System.Drawing.Point(4, 22);
            this.Scanner.Name = "Scanner";
            this.Scanner.Padding = new System.Windows.Forms.Padding(3);
            this.Scanner.Size = new System.Drawing.Size(427, 370);
            this.Scanner.TabIndex = 0;
            this.Scanner.Text = "Scanner";
            this.Scanner.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.UseSysProxyCB);
            this.groupBox5.Controls.Add(this.ProxyText);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Location = new System.Drawing.Point(3, 290);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(418, 71);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Proxification";
            // 
            // UseSysProxyCB
            // 
            this.UseSysProxyCB.AutoSize = true;
            this.UseSysProxyCB.Location = new System.Drawing.Point(107, 43);
            this.UseSysProxyCB.Name = "UseSysProxyCB";
            this.UseSysProxyCB.Size = new System.Drawing.Size(200, 17);
            this.UseSysProxyCB.TabIndex = 2;
            this.UseSysProxyCB.Text = "Use system default (browser settings)";
            this.UseSysProxyCB.UseVisualStyleBackColor = true;
            // 
            // ProxyText
            // 
            this.ProxyText.Location = new System.Drawing.Point(107, 17);
            this.ProxyText.Name = "ProxyText";
            this.ProxyText.Size = new System.Drawing.Size(293, 20);
            this.ProxyText.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Proxy address";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RandomOrderCB);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.MimicBrowserTB);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.BlockDetectComboBox);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.ParallelScansTextBox);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.showProgressDialogCB);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.freeScanCB);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.requestPages);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.stealthTime);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.summaryCheck);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.timeOutTextBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.warnScanTextBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(3, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(418, 282);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scanning";
            // 
            // RandomOrderCB
            // 
            this.RandomOrderCB.AutoSize = true;
            this.RandomOrderCB.Location = new System.Drawing.Point(187, 178);
            this.RandomOrderCB.Name = "RandomOrderCB";
            this.RandomOrderCB.Size = new System.Drawing.Size(15, 14);
            this.RandomOrderCB.TabIndex = 25;
            this.RandomOrderCB.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 179);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(113, 13);
            this.label18.TabIndex = 24;
            this.label18.Text = "Randomize scan order";
            // 
            // MimicBrowserTB
            // 
            this.MimicBrowserTB.Location = new System.Drawing.Point(187, 249);
            this.MimicBrowserTB.Name = "MimicBrowserTB";
            this.MimicBrowserTB.Size = new System.Drawing.Size(213, 20);
            this.MimicBrowserTB.TabIndex = 23;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 252);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(131, 13);
            this.label17.TabIndex = 22;
            this.label17.Text = "Mimic Browser User Agent";
            // 
            // BlockDetectComboBox
            // 
            this.BlockDetectComboBox.FormattingEnabled = true;
            this.BlockDetectComboBox.Items.AddRange(new object[] {
            "Select on each block",
            "Select once, stop all ongoing scans",
            "Ignore blocks"});
            this.BlockDetectComboBox.Location = new System.Drawing.Point(187, 222);
            this.BlockDetectComboBox.Name = "BlockDetectComboBox";
            this.BlockDetectComboBox.Size = new System.Drawing.Size(213, 21);
            this.BlockDetectComboBox.TabIndex = 21;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 225);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "Blocking detection";
            // 
            // ParallelScansTextBox
            // 
            this.ParallelScansTextBox.Location = new System.Drawing.Point(187, 197);
            this.ParallelScansTextBox.Name = "ParallelScansTextBox";
            this.ParallelScansTextBox.Size = new System.Drawing.Size(47, 20);
            this.ParallelScansTextBox.TabIndex = 19;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 200);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(105, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "Parallel scan threads";
            // 
            // showProgressDialogCB
            // 
            this.showProgressDialogCB.AutoSize = true;
            this.showProgressDialogCB.Location = new System.Drawing.Point(187, 156);
            this.showProgressDialogCB.Name = "showProgressDialogCB";
            this.showProgressDialogCB.Size = new System.Drawing.Size(15, 14);
            this.showProgressDialogCB.TabIndex = 17;
            this.showProgressDialogCB.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 156);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(176, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "Show progress dialog on mass scan";
            // 
            // freeScanCB
            // 
            this.freeScanCB.AutoSize = true;
            this.freeScanCB.Location = new System.Drawing.Point(187, 135);
            this.freeScanCB.Name = "freeScanCB";
            this.freeScanCB.Size = new System.Drawing.Size(15, 14);
            this.freeScanCB.TabIndex = 15;
            this.freeScanCB.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 135);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(177, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Allow scanning without host entered";
            // 
            // requestPages
            // 
            this.requestPages.Location = new System.Drawing.Point(187, 109);
            this.requestPages.Name = "requestPages";
            this.requestPages.Size = new System.Drawing.Size(47, 20);
            this.requestPages.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 112);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Request pages at once";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(259, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "msec";
            // 
            // stealthTime
            // 
            this.stealthTime.Location = new System.Drawing.Point(187, 85);
            this.stealthTime.Name = "stealthTime";
            this.stealthTime.Size = new System.Drawing.Size(66, 20);
            this.stealthTime.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Sleep between requests";
            // 
            // summaryCheck
            // 
            this.summaryCheck.AutoSize = true;
            this.summaryCheck.Location = new System.Drawing.Point(187, 68);
            this.summaryCheck.Name = "summaryCheck";
            this.summaryCheck.Size = new System.Drawing.Size(15, 14);
            this.summaryCheck.TabIndex = 8;
            this.summaryCheck.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Show summary";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "msec";
            // 
            // timeOutTextBox
            // 
            this.timeOutTextBox.Location = new System.Drawing.Point(187, 42);
            this.timeOutTextBox.Name = "timeOutTextBox";
            this.timeOutTextBox.Size = new System.Drawing.Size(66, 20);
            this.timeOutTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Time-out";
            // 
            // warnScanTextBox
            // 
            this.warnScanTextBox.Location = new System.Drawing.Point(187, 19);
            this.warnScanTextBox.Name = "warnScanTextBox";
            this.warnScanTextBox.Size = new System.Drawing.Size(47, 20);
            this.warnScanTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Warn if scanning more dorks than";
            // 
            // Miscelaneous
            // 
            this.Miscelaneous.Controls.Add(this.groupBox4);
            this.Miscelaneous.Controls.Add(this.groupBox2);
            this.Miscelaneous.Controls.Add(this.groupBox1);
            this.Miscelaneous.Location = new System.Drawing.Point(4, 22);
            this.Miscelaneous.Name = "Miscelaneous";
            this.Miscelaneous.Padding = new System.Windows.Forms.Padding(3);
            this.Miscelaneous.Size = new System.Drawing.Size(427, 370);
            this.Miscelaneous.TabIndex = 1;
            this.Miscelaneous.Text = "Miscellaneous";
            this.Miscelaneous.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.showSplashCheckBox);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Location = new System.Drawing.Point(6, 138);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(415, 51);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Appearance";
            // 
            // showSplashCheckBox
            // 
            this.showSplashCheckBox.AutoSize = true;
            this.showSplashCheckBox.Location = new System.Drawing.Point(187, 23);
            this.showSplashCheckBox.Name = "showSplashCheckBox";
            this.showSplashCheckBox.Size = new System.Drawing.Size(15, 14);
            this.showSplashCheckBox.TabIndex = 1;
            this.showSplashCheckBox.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(117, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Show splash on startup";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.UseSysBrowserCB);
            this.groupBox2.Controls.Add(this.browseBrowser);
            this.groupBox2.Controls.Add(this.preferredBrowserTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(6, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 70);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Browsing";
            // 
            // UseSysBrowserCB
            // 
            this.UseSysBrowserCB.AutoSize = true;
            this.UseSysBrowserCB.Location = new System.Drawing.Point(107, 45);
            this.UseSysBrowserCB.Name = "UseSysBrowserCB";
            this.UseSysBrowserCB.Size = new System.Drawing.Size(115, 17);
            this.UseSysBrowserCB.TabIndex = 6;
            this.UseSysBrowserCB.Text = "Use system default";
            this.UseSysBrowserCB.UseVisualStyleBackColor = true;
            // 
            // browseBrowser
            // 
            this.browseBrowser.Location = new System.Drawing.Point(380, 18);
            this.browseBrowser.Name = "browseBrowser";
            this.browseBrowser.Size = new System.Drawing.Size(25, 20);
            this.browseBrowser.TabIndex = 4;
            this.browseBrowser.Text = "...";
            this.browseBrowser.UseVisualStyleBackColor = true;
            // 
            // preferredBrowserTextBox
            // 
            this.preferredBrowserTextBox.Location = new System.Drawing.Point(107, 19);
            this.preferredBrowserTextBox.Name = "preferredBrowserTextBox";
            this.preferredBrowserTextBox.Size = new System.Drawing.Size(267, 20);
            this.preferredBrowserTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Preferred Browser";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.browseDorkFile);
            this.groupBox1.Controls.Add(this.dorkFileTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 50);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dorks";
            // 
            // browseDorkFile
            // 
            this.browseDorkFile.Location = new System.Drawing.Point(380, 17);
            this.browseDorkFile.Name = "browseDorkFile";
            this.browseDorkFile.Size = new System.Drawing.Size(25, 20);
            this.browseDorkFile.TabIndex = 3;
            this.browseDorkFile.Text = "...";
            this.browseDorkFile.UseVisualStyleBackColor = true;
            // 
            // dorkFileTextBox
            // 
            this.dorkFileTextBox.Location = new System.Drawing.Point(70, 17);
            this.dorkFileTextBox.Name = "dorkFileTextBox";
            this.dorkFileTextBox.Size = new System.Drawing.Size(304, 20);
            this.dorkFileTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dork File";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 454);
            this.Controls.Add(this.abortButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.OptionsTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.TopMost = true;
            this.OptionsTab.ResumeLayout(false);
            this.Scanner.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.Miscelaneous.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button abortButton;
        private System.Windows.Forms.TabControl OptionsTab;
        private System.Windows.Forms.TabPage Scanner;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox UseSysProxyCB;
        private System.Windows.Forms.TextBox ProxyText;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox MimicBrowserTB;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox BlockDetectComboBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox ParallelScansTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox showProgressDialogCB;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox freeScanCB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox requestPages;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox stealthTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox summaryCheck;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox timeOutTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox warnScanTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage Miscelaneous;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button browseDorkFile;
        private System.Windows.Forms.TextBox dorkFileTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox showSplashCheckBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox UseSysBrowserCB;
        private System.Windows.Forms.Button browseBrowser;
        private System.Windows.Forms.TextBox preferredBrowserTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox RandomOrderCB;
    }
}