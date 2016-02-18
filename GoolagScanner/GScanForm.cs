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
using System.Reflection;
using System.Resources;
using System.Collections;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace GoolagScanner
{
    /// <summary>
    /// The main form of GoolagScanner.
    /// </summary>
    public partial class GScanForm : Form
    {
        /// <summary>
        /// Constructor. Initialisation starts here.
        /// </summary>
        public GScanForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            TextAreaTraceListerner tatl = new TextAreaTraceListerner(ConsoleTextBox);
            System.Diagnostics.Trace.Listeners.Add(tatl);

            Debug.Trace.TraceGoolag.Level = (TraceLevel)Properties.Settings.Default.TraceLevel;
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError,
                Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                "Welcome to GoolagScanner! Version");
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError,
                "-= CULT OF THE DEAD COW 2008 =-");
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError,
                System.Environment.OSVersion.VersionString, "OS");
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError,
                System.Environment.Version, "CLR");

            Assembly assembly = Assembly.GetExecutingAssembly();
            rm = new ResourceManager("GoolagScanner.Properties.Resources", assembly);

            /*
             * There are some gui-elements we may not show in every build,
             * especially not in release.
             * Some are simply for debugging and testing. (use #if DEBUG)
             */
#if NEVER
            testMe.Visible = true;
            pasteToolStripButton.Visible = true;
            pasteToolStripMenuItem.Visible = true;
#else
            pasteToolStripMenuItem.Visible = false;
            printToolStripButton.Visible = false;
#endif

            // load the dorks we have at startup
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, "Attempting to load dork-file.");

            if (loadDorkFile(getDataPathFile(Properties.Settings.Default.DorkFile), dorkList, DorkCategories) == false)
            {
                Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError, "Failed to load dork-file. Exiting.");
                Application.Exit();
            }

            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, "Sorting dorks.");
            dorkList.Sort(null);

            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, "Building dork-tree.");
            tvw_FillTree();
            SetTreeCategoriesCount();

            // preset some vars from resources
            mResCancel = rm.GetString("RES_CANCEL");
            mResScanning = rm.GetString("RES_SCANNING");
            mResReady = rm.GetString("RES_READY");

            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError, "Gathering proxy-settings.");

            // check the network setting, at least the proxy
            HttpSimpleGet.Proxy = new Proxify().GetProxy();

            updateUIStates(false);

            if (Program.Splash_cDc != null)
            {
                Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, "Closing splash.");
                Thread.Sleep(2900); // be sure, the splash is seen (and does not flicker)
                Program.Splash_Goolag = new Splash(285, 114, global::GoolagScanner.Properties.Resources.gscansplashd, 0.029, 0.034, 470);
                Program.Splash_Goolag.Show();
                Thread.Sleep(3000);
                Program.Splash_cDc.Close();
            }

            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo, "Initialisation completed.");
        }

        /// <summary>
        /// Called from framework when form is loaded completely.
        /// Sets title- and status-text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formLoad(object sender, EventArgs e)
        {
            RealTitleText = this.Text; // safe the title set from the designer
            SetScannerTitle();
            StatusLabel.Text = mResReady;
        }

        /// <summary>
        /// Called from the framework when the form is displayed completely.
        /// Show About box if wanted, set focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formShown(object sender, EventArgs e)
        {
            // so the app is completely loaded...

            if (Properties.Settings.Default.ShowAboutAtStart == true)
            {
                Update();
                Thread.Sleep(250);
                Application.DoEvents();

                About myAbout = new About();
                myAbout.ShowDialog();
            }

            if (Program.Splash_Goolag != null)
            {
                Program.Splash_Goolag.Dispose();
            }

            scanHostTextBox.Focus();
        }

        /// <summary>
        /// Set title bar of GoolagScanner with title and count of loaded dorks.
        /// </summary>
        private void SetScannerTitle()
        {
            this.Text = RealTitleText + "  -  (" + dorksLoaded + " " + rm.GetString("RES_DORKS_LOADED") + ")";
        }

        /// <summary>
        /// Called when exiting the app via the menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, "Exiting GoolagScanner.");
            Application.Exit();
        }

        /// <summary>
        /// Build up a path to our dorkfile.
        /// </summary>
        /// <param name="fileName">Normally gdorks.xml.</param>
        /// <returns>Full-qualified file name to use for the dork data.</returns>
        private string getDataPathFile(string fileName)
        {
            return OSUtils.getApplicationPath()
                + ".." + System.IO.Path.DirectorySeparatorChar.ToString()
                + ".." + System.IO.Path.DirectorySeparatorChar.ToString()
                + Properties.Settings.Default.DataPath
                + System.IO.Path.DirectorySeparatorChar.ToString()
                + fileName;
        }

        /// <summary>
        /// Load the standard Dork-file.
        /// This method opens and processes the xml-file from the user-settings
        /// via the XmlDorkSet and handles possible errors.
        /// </summary>
        /// <param name="DorkFile">FQFN of the XML-Dork-file</param>
        /// <param name="DorkList">List to add to Dorks to.</param>
        /// <param name="DorkCates">Categories-collection to generate from file.</param>
        /// <returns>True on success.</returns>
        private bool loadDorkFile(string DorkFile, ArrayList DorkList, Categories DorkCates)
        {
            XmlDorkSet xdorkset = new XmlDorkSet(DorkList, DorkCates);

            try
            {
                xdorkset.Open(DorkFile);
            }
            catch (FileNotFoundException fne)
            {
                MessageBox.Show(rm.GetString("RES_E_OPENFILE") + System.Environment.NewLine
                    + fne.Message, DorkFile, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            catch (System.Xml.XmlException sxe)
            {
                MessageBox.Show(rm.GetString("RES_E_XMLINCORRECT") + System.Environment.NewLine
                    + sxe.Message, DorkFile, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(rm.GetString("RES_E_GENERIC") + System.Environment.NewLine
                    + e.Message, DorkFile, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            dorksLoaded += xdorkset.Count;

            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo, xdorkset.Count, "Successful loaded dorks");
            return true;
        }

        /// <summary>
        /// Show a complete dork record, well formated, in the Richtextbox.
        /// </summary>
        /// <param name="_dork">The dork to display.</param>
        private void formatDorkToRichText(Dork _dork)
        {
            FontStyle fs = richTextBox1.SelectionFont.Style;
            richTextBox1.Clear();
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold);
            richTextBox1.AppendText(_dork.Title + System.Environment.NewLine);
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Italic);
            richTextBox1.AppendText(_dork.Query + System.Environment.NewLine + System.Environment.NewLine);
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, fs);
            richTextBox1.AppendText(_dork.Comment);
        }

        /// <summary>
        /// Click on contect-menu inside tree - scan now.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanButton_Click(object sender, EventArgs e)
        {
            if (SelectedDork.Dork != null)
            {
                singleScan();
            }
        }

        /// <summary>
        /// Enable/Disable some UI-elements, depending if we're scanning.
        /// </summary>
        private void updateScanButtons()
        {
            scanButton.Enabled = !inScanning;
            scanToolStripMenuItem.Enabled = !inScanning;
            EditScanMenuItem.Enabled = !inScanning;
            scanHostTextBox.Enabled = !inScanning;
        }

        /// <summary>
        /// UI-Event, 'New'-symbol was clicked, so clear results and un-select all Dorks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            resultListView.Items.Clear();
            setTreeNodeRecursive(tvwDorks.Nodes[0], false);
            updateUIStates(true);
        }

        /// <summary>
        /// Updates various elements of the whole GUI.
        /// </summary>
        /// <param name="isModified">Forces the update to assume that the results have changed.</param>
        private void updateUIStates(bool isModified)
        {
            bool hasItems = resultListView.Items.Count > 0;
            bool selectedItems = resultListView.SelectedItems.Count > 0;
            bool oneSelectedItem = resultListView.SelectedItems.Count == 1;

            resultModified = isModified;

            saveToolStripButton.Enabled = (resultModified & hasItems);
            saveAsToolStripMenuItem.Enabled = hasItems;
            saveToolStripMenuItem.Enabled = (resultModified & hasItems);

            cutToolStripButton.Enabled = selectedItems;
            cutToolStripMenuItem.Enabled = selectedItems;

            copyToolStripButton.Enabled = copyToolStripMenuItem.Enabled = selectedItems;

            selectAllToolStripMenuItem.Enabled = hasItems;
            clearResultsToolStripMenuItem.Enabled = hasItems;
            ClearButton.Enabled = hasItems;

            copyToClpMenuItem.Enabled = selectedItems;
            openInBrowserToolStripMenuItem.Enabled = oneSelectedItem;
            RescanMenuItem.Enabled = oneSelectedItem;
            scanMoreFromHereStrip.Enabled = oneSelectedItem;

            ClearErrorsStrip.Enabled = hasItems;
            showLeftStrip.Enabled = oneSelectedItem;
            ShowErrorStrip.Enabled = oneSelectedItem;
        }

        /// <summary>
        /// Shows our fancy About-DialogBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            About myAbout = new About();
            myAbout.ShowDialog();
        }

        /// <summary>
        /// Shows the Options-dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Options optionsdialog = new Options(verbose);
            Options optionsdialog = new Options();

            // and reset proxy, the user might have changed this and what's to see it right now

            if (optionsdialog.ShowDialog() != DialogResult.Cancel)
            {
                HttpSimpleGet.Proxy = new Proxify().GetProxy();
            }
        }

        /// <summary>
        /// Open Dork File (User Action).
        /// Shows OpenFileDialog, processes XML, adds dorks to tree and so on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (openDorkFileDialog.ShowDialog() == DialogResult.OK)
            {
                string openFile = openDorkFileDialog.FileName;
                Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo, openFile, "Opening dork file ");

                DorkCategories = new Categories();
                dorkList = new ArrayList();

                if (loadDorkFile(openFile, dorkList, DorkCategories))
                {
                    foreach (Category c in DorkCategories)
                    {
                        Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, c.Text,
                            "Got new category");
                        AddCategoryToTreeEx(tvwDorks.Nodes[0], c);
                    }

                    AddDorksToCatTree(true);
                    SetScannerTitle();
                    SetTreeCategoriesCount();
                    Update();
                    Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, "Loading completed.");
                }
                else
                {
                    Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError, "Loading failed.");
                }
            }
        }

        /// <summary>
        /// Clear tool strip event. Clears all results of the result-list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            resultListView.Items.Clear();
            updateUIStates(true);
        }

        /// <summary>
        /// Key event. Starts scanning when ENTER is pressed in the HostScan-Box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                scanMarkedButton_Click(sender, e);
            }
        }

        /// <summary>
        /// Menu-Event. Opens 'Edit and Scan' dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditScanMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedDork.Dork != null)
            {
                EditDorkScan editdorkscan = new EditDorkScan(SelectedDork, singleScan);
                editdorkscan.Show();
            }

        }
    }
}
