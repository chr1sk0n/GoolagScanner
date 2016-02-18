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
using System.Windows.Forms;
using System.Text;

namespace GoolagScanner
{
    /// <summary>
    /// A group of dorks for display in the treeview
    /// </summary>
    public class Category
    {
        private string _Text;
        private TreeNode _Node;
        private int _Count;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Category()
        {
            _Text = "";
            _Count = 0;
        }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Text
        {
            set
            {
                _Text = value;
            }
            get
            {
                return _Text;
            }
        }

        /// <summary>
        /// Gets or sets a treenode associatied to this category.
        /// </summary>
        public TreeNode Node
        {
            set
            {
                _Node = value;
            }
            get
            {
                return _Node;
            }
        }

        /// <summary>
        /// Gets or sets the count of dorks inside this category.
        /// </summary>
        public int Count
        {
            set
            {
                _Count = value;
            }
            get
            {
                return _Count;
            }
        }
    }
}
