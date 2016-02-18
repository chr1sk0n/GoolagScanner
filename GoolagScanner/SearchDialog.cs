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
    /// Classic search dialog to find a Dork in the TreeView.
    /// </summary>
    public partial class SearchDialog : Form
    {
        static string _searchTerm = "";
        static TreeNode _startNode = null;
        static bool _inName = true;
        static bool _inComment = true;
        static bool _inQuery = true;

        private TreeNode lastStartNode;
        private TreeNode rootnode;
        private TreeView treeview;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="TreeToSearch">TreeView to search.</param>
        /// <param name="StartHere">TreeNode to start the search from.</param>
        public SearchDialog(TreeView TreeToSearch, TreeNode StartHere)
        {
            InitializeComponent();

            // reset last search options

            NameCheckBox.Checked = _inName;
            CommentCheckBox.Checked = _inComment;
            QueryCheckBox.Checked = _inQuery;
            Term.Text = _searchTerm;

            _startNode = StartHere;
            rootnode = TreeToSearch.Nodes[0]; // ->RootNode;
            lastStartNode = null;
            this.treeview = TreeToSearch;
        }

        /// <summary>
        /// Click-event on 'Find'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindButton_Click(object sender, EventArgs e)
        {
            // store settings for next search attempt
            _searchTerm = Term.Text;
            _inName = NameCheckBox.Checked;
            _inQuery = QueryCheckBox.Checked;
            _inComment = CommentCheckBox.Checked;

            TreeNode found = searchNodeEx(_startNode, Term.Text.ToLower(), _inName, _inQuery, _inComment);

            if (found != null)
            {
                Dork d = found.Tag as Dork;
                found.EnsureVisible();
                treeview.SelectedNode = found;
                _startNode = found;
                lastStartNode = found;
            }
            else
            {
                MessageBox.Show("No Dork could be found.", "Find Dork", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// UI-Event. Close dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Search a TreeNode.
        /// </summary>
        /// <param name="startNode">TreeNode to start the search.</param>
        /// <param name="searchTerm">String to search for.</param>
        /// <param name="inName">True to search in Dork's name.</param>
        /// <param name="inQuery">True to search in Dork's query.</param>
        /// <param name="inComment">True to search in Dork's comment.</param>
        /// <returns>TreeNode which fits which matches, null if noone was found.</returns>
        private TreeNode searchNodeEx(TreeNode startNode, string searchTerm,
                                    bool inName, bool inQuery, bool inComment)
        {
            TreeNode currentNode = startNode;

            if (currentNode == lastStartNode)
            {
                currentNode = GetNextDorkNode(startNode);
            }

            if (currentNode == rootnode)
            {
                currentNode = rootnode.Nodes[0];
            }

            lastStartNode = currentNode;

            while (true)
            {
                Dork dork = currentNode.Tag as Dork;
                if ((currentNode.Tag is Category) || (dork == null)) // it's a category
                {
                    currentNode = currentNode.Nodes[0];
                    dork = currentNode.Tag as Dork;
                }
                if (inName)
                {
                    if (dork.Name.ToLower().Contains(searchTerm))
                    {
                        return currentNode;
                    }
                }
                if (inQuery)
                {
                    if (dork.Query.ToLower().Contains(searchTerm))
                    {
                        return currentNode;
                    }
                }
                if (inComment)
                {
                    if (dork.Comment.ToLower().Contains(searchTerm))
                    {
                        return currentNode;
                    }
                }

                currentNode = GetNextDorkNode(currentNode);

                if (currentNode == lastStartNode)
                {
                    break;
                }
            }
            return null;
        }

        /// <summary>
        /// Get the next 'dork' node (not category, not root) - regardless where we
        /// are actually in the hierachie.
        /// </summary>
        /// <param name="currentNode">Any node.</param>
        /// <returns>Next possible Dork-node.</returns>
        private TreeNode GetNextDorkNode(TreeNode currentNode)
        {
            if (currentNode.NextNode == null)
            {
                if (currentNode.Parent == null)
                {
                    currentNode = rootnode.Nodes[0];
                }
                else
                {
                    currentNode = GetNextDorkNode(currentNode.Parent);
                }
            }
            else
            {
                currentNode = currentNode.NextNode;
            }
            return currentNode;
        }

        /// <summary>
        /// Handles keyevents, so ENTER starts a search, ESC closes the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TermKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindButton_Click(sender, e);
            }
            else
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Dispose();
                }
            }
        }
    }
}
