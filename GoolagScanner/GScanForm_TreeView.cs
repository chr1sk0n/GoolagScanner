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
    public partial class GScanForm : Form
    {
        /// <summary>
        /// TreeView-Event, after a Dork was selected inside the tree,
        /// enable approbriate menu-items and show information about it
        /// in the richtextbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvw_AfterSelect(object sender, TreeViewEventArgs e)
        {
            richTextBox1.Clear();
            bool isNodeDorkEntry = e.Node.Tag is Dork;
            SelectedDork.Dork = e.Node.Tag as Dork;
            SelectedDork.TreeNode = e.Node;
            SelectedDork.NextPage = 0;

            if (isNodeDorkEntry)
            {
                formatDorkToRichText(SelectedDork.Dork);
            }

            scanToolStripMenuItem.Enabled = isNodeDorkEntry && !inScanning;
            EditScanMenuItem.Enabled = isNodeDorkEntry;
            propertiesToolStripMenuItem.Enabled = isNodeDorkEntry;
            OpenInBrowser.Enabled = isNodeDorkEntry;
        }

        /// <summary>
        /// Fills up the TreeView. Sets the rootnode from a resource-string,
        /// sets categories, and fills in the dorks, ordered by the categories.
        /// </summary>
        private void tvw_FillTree()
        {
            tvwDorks.Nodes.Clear();

            TreeNode RootNode = new TreeNode(rm.GetString("RES_DORKS"));
            Category RootCat = new Category();
            RootNode.Tag = RootCat;
            tvwDorks.Nodes.Add(RootNode);

            for (int i = 0; i < DorkCategories.Count; i++)
            {
                AddCategoryToTree(RootNode, i);
            }

            RootNode.Expand();

            AddDorksToCatTree(false);
        }

        /// <summary>
        /// Add all dorks, that have been load for example, to the
        /// Category they belong to. Also, set a different color if its
        /// a Dork from a user.
        /// Also increases the count of Dorks of each Category.
        /// </summary>
        /// <param name="userdork">True if it's a users' Dork.</param>
        private void AddDorksToCatTree(bool userdork)
        {
            foreach (Dork dork in dorkList)
            {
                string DorkCat = dork.Category;
                for (int i = 0; i < DorkCategories.Count; i++)
                {
                    Category Cat = DorkCategories.getCategoryByIndex(i);
                    if (DorkCat == Cat.Text)
                    {
                        TreeNode treenode = Cat.Node.Nodes.Add(dork.Name);
                        treenode.Tag = dork;
                        if (userdork)
                        {
                            treenode.ForeColor = Color.Blue;
                        }
                        else
                        {
                            treenode.ForeColor = SystemColors.ControlText;
                        }
                        Cat.Count++;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Adds a Category under the rootnode of a TreeView by its index.
        /// </summary>
        /// <param name="RootNode">Rootnode of a TreeView.</param>
        /// <param name="i">Index of the Category.</param>
        private void AddCategoryToTree(TreeNode RootNode, int i)
        {
            Category c = DorkCategories.getCategoryByIndex(i);
            TreeNode categoryNode = new TreeNode(c.Text);
            c.Node = categoryNode;
            categoryNode.Tag = c;
            RootNode.Nodes.Add(categoryNode);
        }

        /// <summary>
        /// Adds a given Category under the rootnode of a TreeView.
        /// </summary>
        /// <param name="RootNode">Rootnode of a TreeView.</param>
        /// <param name="c">Category to add.</param>
        private void AddCategoryToTreeEx(TreeNode RootNode, Category c)
        {
            TreeNode categoryNode = new TreeNode(c.Text);
            c.Node = categoryNode;
            categoryNode.Tag = c;
            categoryNode.ForeColor = Color.Blue;
            RootNode.Nodes.Add(categoryNode);
        }

        /// <summary>
        /// Reformates the Categories in the TreeView to display the count
        /// of Dorks inside them.
        /// </summary>
        private void SetTreeCategoriesCount()
        {
            this.UseWaitCursor = true;

            TreeNode rootnode = tvwDorks.Nodes[0];
            foreach (TreeNode node in rootnode.Nodes)
            {
                if (node.Tag is Category)
                {
                    Category cat = node.Tag as Category;
                    if (cat != null)
                    {
                        StringBuilder cattext = new StringBuilder(cat.Text, cat.Text.Length + 10);
                        cattext.Append("  (");
                        cattext.Append(cat.Count);
                        cattext.Append(")");
                        node.Text = cattext.ToString();
                        Update();
                    }
                }
            }

            this.UseWaitCursor = false;
        }

        /// <summary>
        /// Starts a SingleScan if the user double-clicked a Dork in the TreeView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvw_DoubleClick(object sender, EventArgs e)
        {
            TreeNode tn = tvwDorks.SelectedNode;
            if (tn.Tag is Dork)
            {
                SelectedDork.Dork = (Dork)tn.Tag;
                SelectedDork.TreeNode = tn;
                scanButton_Click(sender, e);
            }
        }

        /// <summary>
        /// Called after an item is checked. If it's a category, every dork belonging
        /// to that is checked (or unchecked), too.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvw_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is Category)
            {
                setTreeNodeRecursive(e.Node, e.Node.Checked);
            }
        }

        /// <summary>
        /// Sets TreeNode, and all TreeNodes beyond this to a given state, recursively.
        /// </summary>
        /// <param name="tn">TreeNode to start from.</param>
        /// <param name="checkedState">State (will show up checked/unchecked).</param>
        private void setTreeNodeRecursive(TreeNode tn, bool checkedState)
        {
            foreach (TreeNode _treeNode in tn.Nodes)
            {
                _treeNode.Checked = checkedState;
                if (_treeNode.Tag is Category)
                {
                    setTreeNodeRecursive(_treeNode, checkedState);
                    Update();
                }
            }
        }

        /// <summary>
        /// Set the currently selected node (and all below) of the Dorks-TreeView to a given state.
        /// </summary>
        /// <param name="checkedState">State (will show up checked/unchecked).</param>
        private void setSelectedNodeRecursing(bool checkedState)
        {
            TreeNode tn = tvwDorks.SelectedNode;
            tn.Checked = checkedState;
            setTreeNodeRecursive(tn, checkedState);
        }

        /// <summary>
        /// UI-Event. Set the currently selected node (and all below) of the Dorks-TreeView to checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setMarkedMenuItem_Click(object sender, EventArgs e)
        {
            setSelectedNodeRecursing(true);
        }

        /// <summary>
        /// UI-Event. Set the currently selected node (and all below) of the Dorks-TreeView to unchecked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setUnmarkedMenuItem_Click(object sender, EventArgs e)
        {
            setSelectedNodeRecursing(false);
        }

        /// <summary>
        /// Get the corresponding Category from a selected TreeNode.
        /// </summary>
        /// <returns>TreeNode that represents a Category.</returns>
        private TreeNode GetSelectedCategory()
        {
            TreeNode treenode = tvwDorks.SelectedNode;
            if (treenode.Tag is Dork)
            {
                treenode = tvwDorks.SelectedNode.Parent;
            }
            return treenode;
        }

        /// <summary>
        /// UI-Event. Mark all Dorks of the TreeView which have the same Category as the one selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void markallMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tn = tvwDorks.SelectedNode;
            if (tn.Tag is Dork)
            {
                tn = tvwDorks.SelectedNode.Parent;
            }
            tn.Checked = true;
            setTreeNodeRecursive(tn, true);
        }

        /// <summary>
        /// UI-Event. Unmark all Dorks of the TreeView which have the same Category as the one selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void unmarkallMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tn = tvwDorks.SelectedNode;
            if (tn.Tag is Dork)
            {
                tn = tvwDorks.SelectedNode.Parent;
            }
            tn.Checked = false;
            setTreeNodeRecursive(tn, false);
        }

        /// <summary>
        /// MouseMove-event. This enables drag-and-drop if a dork is dragged somewhere,
        /// (to a browser eg).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvw_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                TreeNode treenode = tvwDorks.SelectedNode;
                if (treenode != null)
                {
                    Dork dork = treenode.Tag as Dork;
                    if (dork != null)
                    {
                        IScanProvider scanProvider = new ScanGoogleProvider();
                        RequestBuilder req = new RequestBuilder(scanProvider);

                        string currentRequest = req.getRequest(dork.Query, "", 0);
                        tvwDorks.DoDragDrop(currentRequest, DragDropEffects.Copy);
                    }
                }
            }
        }

        /// <summary>
        /// Extracts the complete URL (which means ScanProvider plus query) of
        /// the selected Dork.
        /// </summary>
        /// <returns>URL of the selected Dork.</returns>
        private string GetRequestFromSelected()
        {
            TreeNode treenode = tvwDorks.SelectedNode;
            if (treenode != null)
            {
                Dork dork = treenode.Tag as Dork;
                if (dork != null)
                {
                    IScanProvider scanProvider = new ScanGoogleProvider();
                    RequestBuilder req = new RequestBuilder(scanProvider);

                    return req.getRequest(dork.Query, scanHostTextBox.Text.Trim(), 0);
                }
            }
            return null;
        }

        /// <summary>
        /// Shows Property-dialog of a selected dork.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode treenode = tvwDorks.SelectedNode;
            if (treenode.Tag != null) // not a category
            {
                DorkProperty dorkprop = new DorkProperty();
                dorkprop.setDork((Dork)treenode.Tag);
                dorkprop.Show();
            }
        }

        /// <summary>
        /// Open SearchDialog, starts with the currently selected Dork.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode node = tvwDorks.SelectedNode;
            if (node == null) // so nothing was selected
            {
                node = tvwDorks.Nodes[0]; // take root node
            }
            SearchDialog searchdialog = new SearchDialog(tvwDorks, node);
            searchdialog.ShowDialog();
        }

        /// <summary>
        /// Open browser with the selected Dork in the TreeView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenInBrowser_Click(object sender, EventArgs e)
        {
            string req = GetRequestFromSelected();
            if (req != null)
            {
                OSUtils.OpenInBrowser(req);
            }
        }
    }
}
