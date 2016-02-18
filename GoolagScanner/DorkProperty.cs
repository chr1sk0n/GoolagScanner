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
    /// Simple toolbox-style dialog to display the dork properties separately.
    /// </summary>
    public partial class DorkProperty : Form
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public DorkProperty()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set dork to display.
        /// </summary>
        /// <param name="t">Dork to display</param>
        public void setDork(Dork t)
        {
            if (t != null)
            {
                this.Text = t.Title;
                this.textBox1.Text = t.Name;
                this.textBox2.Text = t.Category;
                this.textBox3.Text = t.Query;
                this.richTextBox1.Text = t.Comment;
            }
        }

        /// <summary>
        /// One button which simply closes the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}