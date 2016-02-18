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
    /// Saving of the result list.
    /// </summary>
    public partial class GScanForm : Form
    {
        string strFile = "";

        /// <summary>
        /// Menu Item, 'Save'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveResultsAs();
        }

        /// <summary>
        /// UI-Event, user clicked on 'Save' in toolstrip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resultModified) // so there's something to save
            {
                if (String.IsNullOrEmpty(strFile))
                {
                    SaveResultsAs();
                }
                else
                {
                    SaveToFile();
                }
            }
        }

        /// <summary>
        /// Show SaveFileDialog, uses previous name if there was one used.
        /// </summary>
        /// <returns>True on success.</returns>
        private bool SaveResultsAs()
        {
            if (strFile.Trim() != "" && strFile != null)
            {
                saveResultsFileDialog.FileName = strFile;
            }
            if (saveResultsFileDialog.ShowDialog() == DialogResult.OK)
            {
                strFile = saveResultsFileDialog.FileName;
                return (SaveToFile());
            }
            return false;
        }

        /// <summary>
        /// Write the list as file to disc.
        /// </summary>
        /// <returns>True on success.</returns>
        private bool SaveToFile()
        {
            try
            {
                StreamWriter sw = new StreamWriter(strFile, false);
                foreach (ListViewItem lv in resultListView.Items)
                {
                    string theUrl = lv.SubItems[2].Text;
                    string theDork = lv.SubItems[1].Text;
                    sw.WriteLine(theDork + "\t\t\t" + theUrl);
                }

                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            resultModified = false;
            return true;
        }

    }
}

