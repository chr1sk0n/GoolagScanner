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
using System.Text;
using System.Windows.Forms;

namespace GoolagScanner
{
    /// <summary>
    /// The currently selected Dork, which also means the Dork
    /// currently in usage. It's a structure because it keeps just a few
    /// references.
    /// </summary>
    public struct SSelectedDork
    {
        private Dork _dork;
        private TreeNode _treenode;
        private int _nextpage;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dork">Dork</param>
        /// <param name="treenode">TreeNode</param>
        public SSelectedDork(Dork dork, TreeNode treenode)
        {
            this._dork = dork;
            this._treenode = treenode;
            this._nextpage = 0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dork">Dork</param>
        /// <param name="treenode">TreeNode</param>
        /// <param name="nextpage">NextPage</param>
        public SSelectedDork(Dork dork, TreeNode treenode, int nextpage)
        {
            this._dork = dork;
            this._treenode = treenode;
            this._nextpage = nextpage;
        }

        /// <summary>
        /// Gets or sets the Dork of this selection.
        /// </summary>
        public Dork Dork
        {
            get
            {
                return _dork;
            }
            set
            {
                _dork = value;
            }
        }

        /// <summary>
        /// Gets or sets the TreeNode of this selection.
        /// </summary>
        public TreeNode TreeNode
        {
            get
            {
                return _treenode;
            }

            set
            {
                _treenode = value;
            }
        }

        /// <summary>
        /// Gets or sets the NextPage of this selection.
        /// </summary>
        public int NextPage
        {
            get
            {
                return _nextpage;
            }
            set
            {
                _nextpage = value;
            }
        }
    }
}
