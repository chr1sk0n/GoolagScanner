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

namespace GoolagScanner
{
    /// <summary>
    /// Class for gathering statistics of a scan.
    /// </summary>
    public class SummaryStat
    {
        private int _scansSuccess;
        private int _scansFailed;
        private int _scansNoResult;
        private System.DateTime _starttime;
        private System.DateTime _endtime;

        /// <summary>
        /// Constructor. Resets all stats.
        /// </summary>
        public SummaryStat()
        {
            _scansSuccess = 0;
            _scansFailed = 0;
            _scansNoResult = 0;
        }

        /// <summary>
        /// Constructor. Sets stats.
        /// </summary>
        /// <param name="scansSuccess">Scans that succeeded</param>
        /// <param name="scansFailed">Scans that failed</param>
        /// <param name="scansNoResult">Scans that got no results</param>
        public SummaryStat(int scansSuccess, int scansFailed, int scansNoResult)
        {
            _scansSuccess = scansSuccess;
            _scansFailed = scansFailed;
            _scansNoResult = scansNoResult;
        }

        /// <summary>
        /// Number of all scanned dorks.
        /// </summary>
        public int Count
        {
            get
            {
                return _scansFailed + _scansNoResult + _scansSuccess;
            }
        }

        public int ScansSuccess
        {
            get
            {
                return _scansSuccess;
            }
            set
            {
                _scansSuccess = value;
            }
        }

        public int ScansFailed
        {
            get
            {
                return _scansFailed;
            }
            set
            {
                _scansFailed = value;
            }
        }

        public int ScansNoResult
        {
            get
            {
                return _scansNoResult;
            }
            set
            {
                _scansNoResult = value;
            }
        }

        public void captureStartTime()
        {
            _starttime = System.DateTime.Now;
        }

        public void captureEndTime()
        {
            _endtime = System.DateTime.Now;
        }

        public System.DateTime StartTime
        {
            get
            {
                return _starttime;
            }
        }

        public System.DateTime EndTime
        {
            get
            {
                return _endtime;
            }
        }
    }
}
