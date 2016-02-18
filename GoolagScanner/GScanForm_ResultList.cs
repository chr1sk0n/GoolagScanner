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
using System.Diagnostics;

namespace GoolagScanner
{
    /// <summary>
    /// Handling of the result list of the scanned Dorks.
    /// </summary>
    public partial class GScanForm : Form
    {
        /// <summary>
        /// Keeps the last sort order of a column.
        /// </summary>
        static int lastSortedColumn = -1;

        /// <summary>
        /// Called when clicking on a column of the result-list.
        /// Simply changes the sort order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resultColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (!inScanning)
            {
                if (e.Column == lastSortedColumn)
                {
                    if (resultListView.Sorting == SortOrder.Ascending)
                    {
                        resultListView.Sorting = SortOrder.Descending;
                        resultListView.ListViewItemSorter = new ListViewItemComparerDesc(e.Column);
                    }
                    else
                    {
                        resultListView.Sorting = SortOrder.Ascending;
                        resultListView.ListViewItemSorter = new ListViewItemComparer(e.Column);
                    }
                }
                else
                {
                    resultListView.Sorting = SortOrder.Ascending;
                    lastSortedColumn = e.Column;
                }
            }
        }

        /// <summary>
        /// Implements the manual sorting of items by columns. Ascending.
        /// </summary>
        class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }

        /// <summary>
        /// Implements the manual sorting of items by columns. Descending.
        /// </summary>
        class ListViewItemComparerDesc : IComparer
        {
            private int col;
            public ListViewItemComparerDesc()
            {
                col = 0;
            }
            public ListViewItemComparerDesc(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                return -String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }

        /// <summary>
        /// Called when double-clicking on an item in the result-list.
        /// Opens browser with the Url, or (if unsuccessfull scan) opens
        /// dialog with description of error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resultList_DoubleClick(object sender, EventArgs e)
        {
            if (resultListView.SelectedItems.Count == 1)
            {
                DorkDone ddone = (DorkDone)resultListView.SelectedItems[0].Tag;

                if (ddone.ScanResult == (int)RESULT_STATUS.ScanWithResult
                    || ddone.ScanResult == (int)RESULT_STATUS.Blocked)
                {
                    openInBrowserToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    if (ddone.ScanResult == (int)RESULT_STATUS.Failure)
                    {
                        ShowErrorStrip_Click(sender, e);
                    }
                }
            }
        }

        /// <summary>
        /// Called whenever the selection from the user in the result-list changes.
        /// Mostly items from the context-menu are enabled/disabled here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resultSelectionChanged(object sender, EventArgs e)
        {
            bool selectedItems = resultListView.SelectedItems.Count > 0;
            bool oneSelectedItem = resultListView.SelectedItems.Count == 1;

            cutToolStripButton.Enabled = cutToolStripMenuItem.Enabled = selectedItems;
            copyToolStripButton.Enabled = copyToolStripMenuItem.Enabled = selectedItems;
            copyToClpMenuItem.Enabled = selectedItems;

            openInBrowserToolStripMenuItem.Enabled = oneSelectedItem;
            RescanMenuItem.Enabled = oneSelectedItem;
            showLeftStrip.Enabled = oneSelectedItem;
            scanMoreFromHereStrip.Enabled = oneSelectedItem;

            if (oneSelectedItem)
            {
                DorkDone ddone = resultListView.SelectedItems[0].Tag as DorkDone;
                if (ddone != null)
                {
                    bool isSuccessItem = (ddone.ScanResult == (int)RESULT_STATUS.ScanWithResult);

                    ShowErrorStrip.Enabled = (ddone.ScanResult == (int)RESULT_STATUS.Failure);

                    scanMoreFromHereStrip.Enabled = (ddone.NextPage != 0) && (isSuccessItem);
                    openInBrowserToolStripMenuItem.Enabled = isSuccessItem;

                    // again, here, show it to the user
                    formatDorkToRichText(ddone as Dork);
                }
            }
            else
            {
                ShowErrorStrip.Enabled = false;
                openInBrowserToolStripMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Opens the selected result of a Dork in the user's browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resultListView.SelectedItems.Count > 0)
            {
                string ResultURLItem = resultListView.SelectedItems[0].SubItems[2].Text;
                OSUtils.OpenInBrowser(ResultURLItem);
            }
        }

        /// <summary>
        /// Shows the Dork of the selected result back in the TreeView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showLeftStrip_Click(object sender, EventArgs e)
        {
            tvwDorks.Focus();
            DorkDone dork = (DorkDone)resultListView.SelectedItems[0].Tag;
            TreeNode treenode = dork.LastNode;
            treenode.Expand();
            treenode.EnsureVisible();
            tvwDorks.SelectedNode = treenode;
        }

        /// <summary>
        /// Scans the selected Dork again, requests the next page of results (if any).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanMoreFromHereStrip_Click(object sender, EventArgs e)
        {
            DorkDone ddone = (DorkDone)resultListView.SelectedItems[0].Tag;
            SelectedDork.Dork = ddone as Dork;
            SelectedDork.TreeNode = ddone.LastNode;
            SelectedDork.NextPage = ddone.NextPage;
            singleScan();
        }

        /// <summary>
        /// If a Dork failed, show its error message in a MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowErrorStrip_Click(object sender, EventArgs e)
        {
            DorkDone ddone = (DorkDone)resultListView.SelectedItems[0].Tag;
            MessageBox.Show(ddone.ErrorMessage, rm.GetString("RES_E_SCANERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Clear all un-successful Dorks/results out of the ResultView.
        /// Leaves just positives.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearErrorsStrip_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listItem in resultListView.Items)
            {
                DorkDone ddone = (DorkDone)listItem.Tag;
                if (ddone.ScanResult != (int)RESULT_STATUS.ScanWithResult)
                {
                    resultListView.Items.Remove(listItem);
                }
            }
        }

        /// <summary>
        /// Re-scan (so, scan again) the selected Dork.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RescanMenuItem_Click(object sender, EventArgs e)
        {
            DorkDone ddone = (DorkDone)resultListView.SelectedItems[0].Tag;
            SelectedDork.Dork = ddone as Dork;
            SelectedDork.TreeNode = ddone.LastNode;
            SelectedDork.NextPage = 0;
            singleScan();
        }

        /// <summary>
        /// Set all Items left over from 'Scan' to 'Cancel' after a scan.
        /// </summary>
        private void ResetStatus()
        {
            foreach (ListViewItem listItem in resultListView.Items)
            {
                if (listItem.ImageIndex == (int)RESULT_STATUS.WhileScan)
                {
                    listItem.ImageIndex = (int)RESULT_STATUS.Cancel;
                    listItem.SubItems[0].Text = rm.GetString("RES_CANCELSCAN");
                }
            }
        }

    }
}
