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

namespace GoolagScanner
{
    /// <summary>
    /// The little neat and fancy animated dialogbox which shows progress while scanning.
    /// </summary>
    public partial class ScanningDialog : Form
    {
        private int DorkCount;
        public delegate void DStop();
        DStop StopScanner;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dorkcount">Count of Dorks that will be scanned.</param>
        /// <param name="stopscan">Method that can stop the scanning.</param>
        public ScanningDialog(int dorkcount, DStop stopscan)
        {
            DorkCount = dorkcount;
            StopScanner = stopscan;
            InitializeComponent();
        }

        /// <summary>
        /// Set the currently displayed percentage of finished scans.
        /// Updates both, ProgressBar and numeric output.
        /// </summary>
        /// <param name="percentage">Percentage (obvious between 0 and 100).</param>
        public void SetPercentage(int percentage)
        {
            if (percentage > 100) percentage = 100;
            DialogProgressBar.Value = percentage;
            PercentageOutput.Text = percentage.ToString() + "%";
        }

        /// <summary>
        /// Update dialog titlebar with the index of the dork which is currently scanned.
        /// </summary>
        /// <param name="currentDork">Index of current Dork.</param>
        public void UpdateTitle(int currentDork)
        {
            Text = "Scanning... (" + currentDork.ToString() + "/" + DorkCount.ToString() + ")";
            this.Update();
        }

        /// <summary>
        /// Update titlebar to something special while scans are pending (and we wait for).
        /// </summary>
        public void UpdateTitleWaiting()
        {
            Text = "Waiting for pending scans...";
            this.Update();
        }

        /// <summary>
        /// UI-Event. User clicked on 'Abort'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbortButton_Click(object sender, EventArgs e)
        {
            AbortButton.Enabled = false;
            StopScanner();
        }

    }
}
