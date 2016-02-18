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
    /// Dialog. Shows the statistics of a scan to the user.
    /// </summary>
    public partial class SummaryReport : Form
    {
        private SummaryStat summaryStat;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="hostScanned">Host that was scanned.</param>
        /// <param name="summaryStat">Instance of SummaryStat object.</param>
        public SummaryReport(string hostScanned, SummaryStat summaryStat)
        {
            this.summaryStat = summaryStat;
            InitializeComponent();
            hostScannedBox.Text = hostScanned;

            if (summaryStat != null)
            {
                this.labelScanStart.Text = summaryStat.StartTime.ToLocalTime().ToString();
                System.TimeSpan dt = summaryStat.EndTime - summaryStat.StartTime;
                this.labelScanDuration.Text = string.Format("{0:##00}:{1:##00}:{2:##00}", dt.Hours, dt.Minutes, dt.Seconds);
                sumListView.Items[0].SubItems.Add("Successful dorks");
                sumListView.Items[0].SubItems.Add(summaryStat.ScansSuccess.ToString());
                sumListView.Items[1].SubItems.Add("Dorks without result");
                sumListView.Items[1].SubItems.Add(summaryStat.ScansNoResult.ToString());
                sumListView.Items[2].SubItems.Add("Failures while scanning");
                sumListView.Items[2].SubItems.Add(summaryStat.ScansFailed.ToString());
            }
        }

        /// <summary>
        /// UI-Event. One button that simply closes this.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
