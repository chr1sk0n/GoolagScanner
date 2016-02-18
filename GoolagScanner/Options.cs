// $Id$

/*
	GoolagScanner BETA V1.0
		
    Copyright (C) 2008  CULT OF THE DEAD COW

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace GoolagScanner
{
    /// <summary>
    /// Options dialog. Shows and manages all user-avail options. Also,
    /// validation, the loading and saving from/to properties (to config.xml)
    /// is done here.
    /// </summary>
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            dorkFileTextBox.Text = Properties.Settings.Default.DorkFile;
            preferredBrowserTextBox.Text = Properties.Settings.Default.PreferredBrowser;
            warnScanTextBox.Text = Properties.Settings.Default.WarnScan.ToString();
            timeOutTextBox.Text = Properties.Settings.Default.ScanTimeOut.ToString();
            summaryCheck.Checked = Properties.Settings.Default.ShowSummary;
            stealthTime.Text = Properties.Settings.Default.StealthTime.ToString();
            requestPages.Text = Properties.Settings.Default.RequestPages.ToString();
            freeScanCB.Checked = Properties.Settings.Default.AllowFreeScan;
            showSplashCheckBox.Checked = Properties.Settings.Default.ShowSplash;
            showProgressDialogCB.Checked = Properties.Settings.Default.ShowMassScanDialog;
            UseSysBrowserCB.Checked = Properties.Settings.Default.UseSystemBrowser;
            UseSysProxyCB.Checked = Properties.Settings.Default.UseSystemProxy;
            ProxyText.Text = Properties.Settings.Default.ProxyAddress;
            ParallelScansTextBox.Text = Properties.Settings.Default.MaxParallelScans.ToString();
            MimicBrowserTB.Text = Properties.Settings.Default.UserAgent;
            BlockDetectComboBox.SelectedIndex = Properties.Settings.Default.BlockDetectMode;
            RandomOrderCB.Checked = Properties.Settings.Default.RandomScanOrder;

            UpdateOptions();
            abortButton.Focus();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DorkFile = dorkFileTextBox.Text;
            Properties.Settings.Default.PreferredBrowser = preferredBrowserTextBox.Text;

            Properties.Settings.Default.ShowSummary = summaryCheck.Checked;
            Properties.Settings.Default.AllowFreeScan = freeScanCB.Checked;
            Properties.Settings.Default.ShowSplash = showSplashCheckBox.Checked;
            Properties.Settings.Default.ShowMassScanDialog = showProgressDialogCB.Checked;
            Properties.Settings.Default.RandomScanOrder = RandomOrderCB.Checked;

            Properties.Settings.Default.UseSystemBrowser = UseSysBrowserCB.Checked;
            Properties.Settings.Default.UseSystemProxy = UseSysProxyCB.Checked;
            Properties.Settings.Default.ProxyAddress = ProxyText.Text;
            Properties.Settings.Default.UserAgent = MimicBrowserTB.Text;

            try
            {
                Properties.Settings.Default.WarnScan = Convert.ToInt32(warnScanTextBox.Text);
                Properties.Settings.Default.ScanTimeOut = Convert.ToInt32(timeOutTextBox.Text);
                Properties.Settings.Default.StealthTime = Convert.ToInt32(stealthTime.Text);
                Properties.Settings.Default.RequestPages = Convert.ToInt32(requestPages.Text);
                Properties.Settings.Default.MaxParallelScans = Convert.ToInt32(ParallelScansTextBox.Text);
                Properties.Settings.Default.BlockDetectMode = Convert.ToInt32(BlockDetectComboBox.SelectedIndex);
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid numeric value.");
                Properties.Settings.Default.Reload();
                return;
            }

            Properties.Settings.Default.Save();
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo, "Settings saved.");
            this.Dispose();
        }

        /// <summary>
        /// UI-Event. User clicked on 'Cancel'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abortButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Show certain text-edits only enables if the 'default'-checkboxes are
        /// not checked.
        /// </summary>
        private void UpdateOptions()
        {
            preferredBrowserTextBox.Enabled = !UseSysBrowserCB.Checked;
            ProxyText.Enabled = !UseSysProxyCB.Checked;
        }

        /// <summary>
        /// Open dialog to choose to dork-file to load by default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseDorkFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            string fname = "";
            ofd1.AddExtension = true;
            ofd1.Filter = "XML file (*.xml)|*.xml";
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                fname = ofd1.FileName.Trim();
                if (fname.Length > 0)
                {
                    dorkFileTextBox.Text = fname;
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Open dialog to choose browser-executable to use by default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            string fname = "";
            ofd1.AddExtension = true;
            ofd1.Filter = "Browser Executable (*.exe)|*.exe";
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                fname = ofd1.FileName.Trim();
                if (fname.Length > 0)
                {
                    preferredBrowserTextBox.Text = fname;
                    this.Update();
                }
            }
        }

        /// <summary>
        /// Enable/Disable browser-selection if system default is choosen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UseSysBrowserCB_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

        /// <summary>
        /// Enable/Disable proxy-selection if system default is choosen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UseSysProxyCB_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptions();
        }

    }
}
