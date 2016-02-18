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
    /// Dork that is to process or that had been processed.
    /// It's inherited from the Dork object, but knows it's
    /// status, treenode, last scan time and error message (if occurred)
    /// </summary>
    class DorkDone : Dork, ICloneable
    {
        private System.DateTime _LastScanTime;
        private int _NextPage;
        private int _ScanResult;
        private TreeNode _LastNode;
        private string _ErrorMessage;
        private string _HostScan;
        private DorkDone _Next;
        private string _ResultURL;
        private ListViewItem _ViewItem;
        private int _Requested;

        /// <summary>
        /// Constructor. Empty Dork.
        /// </summary>
        public DorkDone()
            : base()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dork">Dork (like the one read from the XML).</param>
        public DorkDone(Dork dork)
        {
            this.Category = dork.Category;
            this.Comment = dork.Comment;
            this.Name = dork.Name;
            this.Query = dork.Query;
            this.Title = dork.Title;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dork">SSelectedDork (normally provided from the UI).</param>
        public DorkDone(SSelectedDork dork)
        {
            this.Category = dork.Dork.Category;
            this.Comment = dork.Dork.Comment;
            this.Name = dork.Dork.Name;
            this.Query = dork.Dork.Query;
            this.Title = dork.Dork.Title;
            _LastNode = dork.TreeNode;
            _NextPage = dork.NextPage;
        }

        /// <summary>
        /// Gets or sets the time this Dork was scanned the last time.
        /// </summary>
        public System.DateTime LastScanned
        {
            set
            {
                _LastScanTime = value;
            }
            get
            {
                return _LastScanTime;
            }
        }

        /// <summary>
        /// Gets or sets the next (html-) page to request from the search engine.
        /// </summary>
        public int NextPage
        {
            set
            {
                _NextPage = value;
            }
            get
            {
                return _NextPage;
            }
        }

        /// <summary>
        /// Gets or sets the result of the scan this Dork produced.
        /// </summary>
        public int ScanResult
        {
            set
            {
                _ScanResult = value;
            }
            get
            {
                return _ScanResult;
            }
        }

        /// <summary>
        /// Gets or sets the TreeNode if it is managed by a TreeView.
        /// </summary>
        public TreeNode LastNode
        {
            set
            {
                _LastNode = value;
            }
            get
            {
                return _LastNode;
            }
        }

        /// <summary>
        /// Gets or sets the error message (if any) if scanning of this Dork failed.
        /// </summary>
        public string ErrorMessage
        {
            set
            {
                _ErrorMessage = value;
            }
            get
            {
                return _ErrorMessage;
            }
        }

        /// <summary>
        /// Gets or sets the host (as target) against which this Dork was run.
        /// </summary>
        public string Host
        {
            set
            {
                _HostScan = value;
            }
            get
            {
                return _HostScan;
            }
        }

        /// <summary>
        /// Gets or sets the next DorkDone object. So DorkDone can be used as a 
        /// single linked list.
        /// </summary>
        public DorkDone Next
        {
            set
            {
                _Next = value;
            }
            get
            {
                return _Next;
            }
        }

        /// <summary>
        /// Gets or sets the URL this Dork, so what the search engine got for us :)
        /// </summary>
        public string ResultURL
        {
            set
            {
                _ResultURL = value;
            }
            get
            {
                return _ResultURL;
            }
        }

        /// <summary>
        /// Gets or sets this DorkDone's relation in a ListView.
        /// </summary>
        public ListViewItem ViewItem
        {
            set
            {
                _ViewItem = value;
            }
            get
            {
                return _ViewItem;
            }
        }

        /// <summary>
        /// Gets or sets the count of how often this Dork was scanned or rescanned.
        /// Not used by now.
        /// </summary>
        public int Requested
        {
            set
            {
                _Requested = value;
            }
            get
            {
                return _Requested;
            }
        }

        /// <summary>
        /// Sets the LastScanTime to the current date and time.
        /// </summary>
        public void Now()
        {
            _LastScanTime = System.DateTime.Now;
        }

        /// <summary>
        /// Clone this DorkDone memberwise.
        /// </summary>
        /// <returns>DorkDone object</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
