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
using System.Collections;
using System.IO;
using System.Threading;

namespace GoolagScanner
{
    /// <summary>
    /// Clipboard handling of our app.
    /// </summary>
    public partial class GScanForm : Form
    {
        /// <summary>
        /// Select all.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resultListView.Focus();
            for (int i = 0; i < resultListView.Items.Count; i++)
            {
                resultListView.Items[i].Selected = true;
            }
        }

        /// <summary>
        /// Copy to clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            string allResults = "";
            foreach (ListViewItem lv in resultListView.SelectedItems)
            {
                string lurl = lv.SubItems[1].Text;
                string ldork = lv.SubItems[2].Text;
                allResults += lurl + "\t\t\t" + ldork + System.Environment.NewLine;
            }
            Clipboard.SetDataObject(allResults);
        }

        /// <summary>
        /// Cut to clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            string allResults = "";
            foreach (ListViewItem lv in resultListView.SelectedItems)
            {
                string lurl = lv.SubItems[1].Text;
                string ldork = lv.SubItems[2].Text;
                allResults += lurl + "\t\t\t" + ldork + System.Environment.NewLine;
                resultListView.Items.Remove(lv);
            }
            Clipboard.SetDataObject(allResults);
        }
    }
}
