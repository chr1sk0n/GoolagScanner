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
    /// Dialog box to edit the currently selected Dork. 
    /// So this is a kind of a simple Dork-debugger.
    /// Provides functions to scan directly and undo changes.
    /// </summary>
    public partial class EditDorkScan : Form
    {
        SSelectedDork dorktoedit;
        string originalQuery;
        public delegate void singleScan(SSelectedDork s);
        public singleScan _singleScan;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="DorkToEdit">The currently selected Dork.</param>
        /// <param name="SingleScanMethod">Delegate to a method which scans for us.</param>
        public EditDorkScan(SSelectedDork DorkToEdit, singleScan SingleScanMethod)
        {
            _singleScan = SingleScanMethod;
            dorktoedit = DorkToEdit;
            originalQuery = DorkToEdit.Dork.Query;
            InitializeComponent();
            SetTextElements();
        }

        /// <summary>
        /// Set required textfields.
        /// </summary>
        private void SetTextElements()
        {
            DorkNameLabel.Text = dorktoedit.Dork.Name;
            QueryTextBox.Text = originalQuery;
        }

        /// <summary>
        /// Restore query to its original state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Revert_Click(object sender, EventArgs e)
        {
            QueryTextBox.Text = originalQuery;
            Update();
        }

        /// <summary>
        /// UI-Event. Close this dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// UI_Event. Run scanner with custom (edited) Dork.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scanbutton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(QueryTextBox.Text))
            {
                dorktoedit.Dork.Query = QueryTextBox.Text;
                _singleScan(dorktoedit);
            }
        }
    }
}
