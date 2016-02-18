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
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace GoolagScanner
{
    /// <summary>
    /// Everything relevant for starting and stopping scans goes here.
    /// </summary>
    public partial class GScanForm : Form
    {
        /// <summary>
        /// Initiate a single scan of the currently selected dork.
        /// </summary>
        private void singleScan()
        {
            dorksToScan = new List<SSelectedDork>();
            dorksToScan.Add(SelectedDork);
            InitializeScanning();
        }

        private void singleScan(SSelectedDork selectedDork)
        {
            dorksToScan = new List<SSelectedDork>();
            dorksToScan.Add(selectedDork);
            InitializeScanning();
        }

        /// <summary>
        /// Initailize scanning.
        /// This is called before a scan starts.
        /// In fact, this checks some preconditions and starts the backgroundworker
        /// for scanning and also the backgroundworker for the animation.
        /// </summary>
        private void InitializeScanning()
        {
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, "InitializeScanning()");

            if (inScanning)
            {
                Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceWarning,
                    "..there is already a scan running.");
                return;
            }

            if (Properties.Settings.Default.AllowFreeScan == false)
            {
                if (String.IsNullOrEmpty(scanHostTextBox.Text.Trim()))
                {
                    Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceWarning,
                        "..no host entered. AllowFreeScan not set.");
                    MessageBox.Show(rm.GetString("RES_E_ENTERHOST"), rm.GetString("RES_E_SCANERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            inScanning = true;

            summaryStat = new SummaryStat();
            summaryStat.captureStartTime();

            resultListView.Sorting = SortOrder.None;
            StatusLabel.Text = mResScanning + "...";

            pictureMenuAnim.Enabled = true;

            updateScanButtons();
            progressBar1.Value = 0;
            backgroundScan.RunWorkerAsync();
            backgroundAnim.RunWorkerAsync();

            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose,
                "Backgroundworker for scanning started.");

            this.Update();

            if (dorksToScan.Count > 1 && Properties.Settings.Default.ShowMassScanDialog == true)
            {
                scanningdialog = new ScanningDialog(dorksToScan.Count, StopScanning);
                scanningdialog.Show();
                scanningdialog.UpdateTitle(0);
            }
        }

        /// <summary>
        /// Called after scanning. Stops all started threads, closes open scan-dialog,
        /// updates ui-elements, and displays summary report.
        /// </summary>
        private void FinalizeScanning()
        {
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, "FinalizeScanning()");

            backgroundAnim.CancelAsync();
            inScanning = false;

            summaryStat.captureEndTime();

            updateScanButtons();
            StatusLabel.Text = mResReady;
            progressBar1.Value = 0;

            pictureMenuAnim.Enabled = false;

            updateUIStates(true);

            if (scanningdialog != null)
            {
                scanningdialog.Dispose();
            }

            ResetStatus();

            this.Update();

            if (Properties.Settings.Default.ShowSummary && dorksToScan.Count > 1)
            {
                SummaryReport sumReport = new SummaryReport(scanHostTextBox.Text.Trim(), summaryStat);
                sumReport.ShowDialog();
            }
        }

        /// <summary>
        /// Creates new DorkDone object from a given SSelectedDork object.
        /// Presets attributes like status, time, host
        /// </summary>
        /// <param name="scanDork">A selected Dork (eg from ui)</param>
        /// <returns>Corresponding DorkDone object</returns>
        private DorkDone CreateDorkForScanning(SSelectedDork scanDork)
        {
            DorkDone dorkdone = new DorkDone(scanDork);
            dorkdone.ScanResult = (int)RESULT_STATUS.WhileScan;
            dorkdone.Host = scanHostTextBox.Text.Trim();
            dorkdone.Now();
            return dorkdone;
        }

        /// <summary>
        /// Creates a ListViewItem of the currently selected Dork and
        /// displays it in the ResultListView with 'scanning' status.
        /// Also updates the Dork infobox and statusline.
        /// </summary>
        /// <param name="scanDork">Selected dork</param>
        /// <returns>ListViewItem</returns>
        private ListViewItem ShowTemporaryDork(SSelectedDork scanDork)
        {
            ListViewItem lv = resultListView.Items.Add(rm.GetString("RES_SCAN") + "...",
                                                        (int)RESULT_STATUS.WhileScan);
            lv.SubItems.Add(scanDork.Dork.Title);
            lv.SubItems.Add("...");
            resultListView.EnsureVisible(lv.Index);

            formatDorkToRichText(scanDork.Dork);

            StatusLabel.Text = mResScanning + ": " + scanDork.Dork.Category + " ...";

            return lv;
        }

        /// <summary>
        /// Show/Hide ScanningDialog, if the user wants to see it.
        /// </summary>
        /// <param name="show">True to show.</param>
        private void ShowScanningDialog(bool show)
        {
            if (scanningdialog != null)
            {
                scanningdialog.Visible = show;
                Update();
            }
        }

        /// <summary>
        /// This is one of the hearts of gS.
        /// Like it is named, it checks the results and with them controls
        /// the outer interface of the ScanMonitor. It's somehow a respectful
        /// connection between the UI and the parallel scanner in the backend.
        /// </summary>
        /// <param name="scanmonitor">Instance of ScanMonitor while scanning.</param>
        private void CheckAndDisplayResults(ScanMonitor scanmonitor)
        {
            if (scanmonitor.HasResults())
            {
                Scanner scanner = scanmonitor.GetFinishedScanner();
                DorkDone dorkdone = scanner.ResultDork;
                if (dorkdone == null) return;

                if (dorkdone.ScanResult == (int)RESULT_STATUS.Cancel)
                {
                    Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo, "Scan was canceled.");
                    dorkdone.ViewItem.ImageIndex = (int)RESULT_STATUS.Cancel;
                    dorkdone.ViewItem.SubItems[0].Text = rm.GetString("RES_CANCELSCAN");
                    dorkdone.ViewItem.SubItems[2].Text = "";
                    return;
                }
                if (dorkdone.ScanResult == (int)RESULT_STATUS.Nothing)
                {
                    Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo, "Scan returned no results.");
                    dorkdone.ViewItem.ImageIndex = (int)RESULT_STATUS.Nothing;
                    dorkdone.ViewItem.SubItems[0].Text = rm.GetString("RES_NORESULT");
                    dorkdone.ViewItem.SubItems[2].Text = "";
                    summaryStat.ScansNoResult++;
                    return;
                }
                if (dorkdone.ScanResult == (int)RESULT_STATUS.Failure)
                {
                    dorkdone.ViewItem.ImageIndex = (int)RESULT_STATUS.Failure;
                    dorkdone.ViewItem.SubItems[0].Text = rm.GetString("RES_FAILED");
                    dorkdone.ViewItem.SubItems[2].Text = "";
                    summaryStat.ScansFailed++;
                    Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo, "Scan failed.");
                    return;
                }
                if (dorkdone.ScanResult == (int)RESULT_STATUS.Blocked)
                {
                    dorkdone.ViewItem.ImageIndex = (int)RESULT_STATUS.Blocked;
                    dorkdone.ViewItem.SubItems[0].Text = rm.GetString("RES_BLOCKED");
                    dorkdone.ViewItem.SubItems[2].Text = dorkdone.ResultURL;
                    summaryStat.ScansFailed++;
                    Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo, "Scan was blocked.");

                    if (Properties.Settings.Default.BlockDetectMode == (int)BLOCKING_MODE.SingleAndStop)
                    {
                        int stoppedscans = scanmonitor.StopAllActive();
                        Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, stoppedscans, "Scans canceled in queue");

                        while (scanmonitor.HasPendingScans())
                        {
                            Thread.Sleep(200);
                        }
                    }

                    if (Properties.Settings.Default.BlockDetectMode != (int)BLOCKING_MODE.Ignore)
                    {
                        ShowScanningDialog(false);

                        DialogResult dr = MessageBox.Show("Start browser to unlock block? Cancel will stop scanning.", "Block detected!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Stop);

                        if (dr == DialogResult.Yes)
                        {
                            OSUtils.OpenInBrowser(dorkdone.ResultURL);
                            MessageBox.Show("Ready to resume?", "Resume scanning.", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                        else if (dr == DialogResult.Cancel)
                        {
                            int stoppedscans = scanmonitor.StopAllActive();
                            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, stoppedscans, "Scans canceled in queue");

                            while (scanmonitor.HasPendingScans())
                            {
                                Thread.Sleep(100);
                            }
                            StopScanning();
                        }

                        ShowScanningDialog(true);
                    }

                    return;
                }
                if (dorkdone.ScanResult == (int)RESULT_STATUS.ScanWithResult)
                {
                    resultListView.Items.Remove(dorkdone.ViewItem); // remove the one that is displayed while scanning

                    if (scanner.Count > 0)
                    {
                        int lastIdx = 0;
                        DorkDone resDork = scanner.ResultDork;
                        DorkDone followDork = null;

                        if (resDork.NextPage != 0)
                        {
                            if ((resDork.NextPage / 10) < Properties.Settings.Default.RequestPages)
                            {
                                followDork = new DorkDone();
                                followDork = (DorkDone)resDork.Clone();
                                followDork.ScanResult = (int)RESULT_STATUS.WhileScan;
                                Scanner nextscanner = new Scanner(new ScanGoogleProvider(), followDork);

                                while (!scanmonitor.IsThreadAvail())
                                {
                                    Thread.Sleep(300);
                                }

                                scanmonitor.Add(nextscanner);
                                Thread.Sleep(100);
                            }
                        }

                        do
                        {
                            ListViewItem lv1 = resultListView.Items.Add(rm.GetString("RES_SUCCESS"),
                                                (int)RESULT_STATUS.ScanWithResult);

                            lv1.SubItems.Add(resDork.Title);
                            lv1.SubItems.Add(resDork.ResultURL);
                            lastIdx = lv1.Index;
                            lv1.Tag = resDork;

                            summaryStat.ScansSuccess++;
                            resDork = resDork.Next;

                            resultListView.EnsureVisible(lastIdx);

                        } while (resDork != null);

                    }
                }
            }
        }

        /// <summary>
        /// This is the Background worker which starts and controls the ScanMonitor.
        /// Does some UI things, also takes care of the ScanningDialog. 
        /// This seems a doubling of the ScanMonitor's Watcher, but it is needed to keep the UI running independently.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doThreadScan(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;
            int ScanCountSession = 0;
            double frac = 100.0 / ((double)dorksToScan.Count);
            double realstat = 0;

            using (ScanMonitor scanmonitor = new ScanMonitor(Properties.Settings.Default.MaxParallelScans))
            {
                foreach (SSelectedDork _dork in dorksToScan)
                {
                    ScanCountSession++;

                    if (scanningdialog != null)
                    {
                        scanningdialog.UpdateTitle(ScanCountSession);
                        Update();
                    }

                    DorkDone dorkdone = CreateDorkForScanning(_dork);
                    ListViewItem lv = ShowTemporaryDork(_dork);
                    lv.Tag = dorkdone;
                    dorkdone.ViewItem = lv;
                    Update();

                    Scanner scanner = new Scanner(new ScanGoogleProvider(), dorkdone);

                    while (!scanmonitor.IsThreadAvail())
                    {
                        CheckAndDisplayResults(scanmonitor);
                        Thread.Sleep(300);
                    }

                    scanmonitor.Add(scanner);
                    Thread.Sleep(200);

                    CheckAndDisplayResults(scanmonitor);

                    if (scanningdialog != null)
                    {
                        realstat += frac / Properties.Settings.Default.RequestPages;
                        scanningdialog.SetPercentage((int)Math.Round(realstat));
                        Update();
                    }

                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    int stealthtime = Properties.Settings.Default.StealthTime / 1000;

                    if (stealthtime > 0)
                    {
                        if (stealthtime > 3)
                        {
                            progressBar1.ForeColor = Color.DarkGray;
                        }

                        for (int i = 0; i < stealthtime; i++)
                        {
                            Thread.Sleep(1000);
                            if (bw.CancellationPending)
                            {
                                e.Cancel = true;
                                break;
                            }
                        }

                        if (stealthtime > 3)
                        {
                            progressBar1.ForeColor = Color.DarkBlue;
                        }
                    }

                    if (scanningdialog != null)
                    {
                        realstat = (frac * ScanCountSession);
                        scanningdialog.SetPercentage((int)Math.Round(realstat));
                        Update();
                    }

                    // be sure to cancel
                    if (e.Cancel == true)
                    {
                        break;
                    }

                } // foreach

                if (scanningdialog != null)
                {
                    if (scanmonitor.HasResults() || scanmonitor.HasPendingScans())
                    {
                        scanningdialog.UpdateTitleWaiting();
                    }
                }

                while (scanmonitor.HasResults() ||
                    (!e.Cancel && scanmonitor.HasPendingScans())
                    )
                {
                    CheckAndDisplayResults(scanmonitor);
                    Thread.Sleep(200);
                }
            }
        }

        /// <summary>
        /// Called from backgroundworker when scan is finished.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinalThreadScan(object sender, RunWorkerCompletedEventArgs e)
        {
            FinalizeScanning();
        }

        /// <summary>
        /// Fills the dorksToScan-list recursively.
        /// </summary>
        /// <param name="treenode">TreeNode to start from.</param>
        private void getTreeCheckedRecursive(TreeNode treenode)
        {
            foreach (TreeNode subnode in treenode.Nodes)
            {
                if (subnode.Tag is Category)
                {
                    getTreeCheckedRecursive(subnode);
                }
                else
                {
                    if (subnode.Checked)
                    {
                        dorksToScan.Add(new SSelectedDork(subnode.Tag as Dork, subnode));
                    }
                }
            }
        }

        /// <summary>
        /// After the list Dorks is collected before a real mass-scan, the
        /// order of the scans can be randomized with this, if the user wants to.
        /// </summary>
        /// <param name="listofdorks">List of Dorks for scanning.</param>
        private void RandomizeOrder(List<SSelectedDork> listofdorks)
        {
            Random r = new Random();
            int capa = listofdorks.Count;
            SSelectedDork[] sd = new SSelectedDork[capa];
            int u = 0;
            do
            {
                int d = r.Next(listofdorks.Count);

                if (sd[u].Dork == null)
                {
                    sd[u] = listofdorks[d];
                    listofdorks.RemoveAt(d);
                    u++;
                }
            } while (listofdorks.Count > 0);

            // rebuild list
            for (int i = 0; i < capa; i++)
            {
                listofdorks.Add(sd[i]);
            }
        }

        /// <summary>
        /// Request scan-backgroundworker to stop, update some ui-elements.
        /// </summary>
        public void StopScanning()
        {
            if (inScanning)
            {
                Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, "Requesting to stop scan...");
                StatusLabel.Text = mResCancel;
                Update();
                backgroundScan.CancelAsync();
            }
            else
            {
                Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError, "Error: StopScanning() without active scan.");
            }
        }

        /// <summary>
        /// Scan all marked dorks. Initiates mass-scan. If just one dork is selected,
        /// simply scan this one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanMarkedButton_Click(object sender, EventArgs e)
        {
            dorksToScan = new List<SSelectedDork>();

            // gather all selected dorks, start from the root node
            getTreeCheckedRecursive(tvwDorks.Nodes[0]);

            // randomize scan order if user wants us to

            if (dorksToScan.Count > 1 && Properties.Settings.Default.RandomScanOrder == true)
            {
                RandomizeOrder(dorksToScan);
            }

            if (dorksToScan.Count == 0)
            {
                // if no dorks are marked, check if a single dork is selected
                if (SelectedDork.Dork != null)
                {
                    singleScan();
                }
                else
                {
                    MessageBox.Show(rm.GetString("RES_E_SELECTDORK"));
                }
                return;
            }

            // we're polite, most of the time
            if (Properties.Settings.Default.WarnScan < dorksToScan.Count
                && Properties.Settings.Default.WarnScan != 0)
            {
                Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceWarning,
                    Properties.Settings.Default.WarnScan, "Mass scan, count of dorks exceeds");

                string lsmsg = String.Format(rm.GetString("RES_W_LARGESCAN"), dorksToScan.Count.ToString());

                if (
                    MessageBox.Show(lsmsg, rm.GetString("RES_LARGSCAN"),
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                    != DialogResult.OK)
                {
                    return;
                }
            }

            InitializeScanning();
        }

        /// <summary>
        /// Click event on Stop-button. Will stop scanning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopScanButton_Click(object sender, EventArgs e)
        {
            this.StopScanning();
        }

    }
}
