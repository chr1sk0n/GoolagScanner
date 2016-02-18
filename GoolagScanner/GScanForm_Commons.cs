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
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

/// Notice that we keep the Debug things in an extra namespace.
namespace GoolagScanner.Debug
{
    /// <summary>
    /// Goolag's very own TraceSwitch.
    /// </summary>
    public class Trace
    {
        public static TraceSwitch TraceGoolag = new TraceSwitch("GoolagTrace", "GoolagScanner");
    }
}

namespace GoolagScanner
{
    /// <summary>
    /// Status a active scan/dork can have.
    /// </summary>
    internal enum RESULT_STATUS
    {
        Nothing = 0,
        WhileScan,
        ScanWithResult,
        Failure,
        Blocked,
        Cancel
    }

    /// <summary>
    /// Status a (scan) thread can have.
    /// </summary>
    internal enum SCANTHREADSTATE
    {
        Waiting,
        Working,
        Finished,
        Aborted
    }

    /// <summary>
    /// Strategies of how we can react of getting blocked.
    /// </summary>
    internal enum BLOCKING_MODE
    {
        Single = 0,
        SingleAndStop,
        Ignore
    }

    /// <summary>
    /// Verbosity of errors/messages while scanning. Depreciated.
    /// </summary>
    internal enum VERBOSITY
    {
        No = 0,
        All
    }

    /// <summary>
    /// All commons and global definitions are done here.
    /// </summary>
    public partial class GScanForm : Form
    {
        private string RealTitleText = "";
        private System.Resources.ResourceManager rm;

        Categories DorkCategories = new Categories();
        ArrayList dorkList = new ArrayList();

        List<SSelectedDork> dorksToScan = null;
        SummaryStat summaryStat;
        ScanningDialog scanningdialog;

        SSelectedDork SelectedDork;
        int dorksLoaded = 0;
        bool resultModified = false;

        /// <summary>
        /// Indicates if a scan is running.
        /// </summary>
        volatile bool inScanning = false;

        private readonly string mResScanning;
        private readonly string mResReady;
        private readonly string mResCancel;

        const int animSpeed = 300;
    }
}
