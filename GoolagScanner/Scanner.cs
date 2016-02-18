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
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace GoolagScanner
{
    /// <summary>
    /// Scanner, the complete process. Scans a Dork, independent of a IScanProvider,
    /// parses its results, and keeps the results and the status fo this scan.
    /// </summary>
    class Scanner
    {
        private IScanProvider scanProvider;
        private List<string> parsedResults;
        private HttpSimpleGet httpGet;
        private DorkDone firstResultDork = null;
        private DorkDone _DorkToScan;
        private volatile int _ScanStatus;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ScanProvider">Instanciated implementation of an IScanProvider.</param>
        public Scanner(IScanProvider ScanProvider)
        {
            scanProvider = ScanProvider;
            _ScanStatus = (int)SCANTHREADSTATE.Waiting;
            httpGet = null;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ScanProvider">Instanciated implementation of an IScanProvider.</param>
        /// <param name="dorkToScan">A DorkDone, which represents the Dork to scan.</param>
        public Scanner(IScanProvider ScanProvider, DorkDone dorkToScan)
        {
            scanProvider = ScanProvider;
            _ScanStatus = (int)SCANTHREADSTATE.Waiting;
            _DorkToScan = dorkToScan;
            httpGet = null;
        }

        /// <summary>
        /// Run a scan, with the DorkDone given in the constructor.
        /// </summary>
        public void Do()
        {
            DoDork(_DorkToScan);
        }

        /// <summary>
        /// Run a scan with a DorkDone.
        /// </summary>
        /// <param name="DorkToScan">DorkDone to scan.</param>
        /// <returns>True if no errors occured.</returns>
        public bool DoDork(DorkDone DorkToScan)
        {
            _ScanStatus = (int)SCANTHREADSTATE.Working;
            RequestBuilder req = new RequestBuilder(scanProvider);
            httpGet = new HttpSimpleGet(Properties.Settings.Default.ScanTimeOut);

            string currentRequest = req.getRequest(DorkToScan.Query,
                                                    DorkToScan.Host,
                                                    DorkToScan.NextPage);

            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo, currentRequest, "ScanURL:");

            if (!httpGet.Do(currentRequest))
            {
                DorkToScan.ErrorMessage = httpGet.GetErrorMessage();
                DorkToScan.ScanResult = (int)RESULT_STATUS.Failure;
                _ScanStatus = (int)SCANTHREADSTATE.Finished;
                firstResultDork = DorkToScan;
                return false;
            }

            ParseHtmlResults parser = new ParseHtmlResults(scanProvider, DorkToScan.NextPage);
            parsedResults = parser.Parse(httpGet.GetResults());

            if (parsedResults.Count > 0)
            {
                DorkToScan.ScanResult = (int)RESULT_STATUS.ScanWithResult;
                DorkToScan.NextPage = parser.NextPage;

                DorkDone tmpDork = null;

                foreach (string parsedUrl in parsedResults)
                {
                    DorkDone newDork = new DorkDone();
                    newDork = (DorkDone)DorkToScan.Clone();
                    newDork.ResultURL = parsedUrl;
                    newDork.Next = tmpDork;
                    tmpDork = newDork;
                }

                firstResultDork = tmpDork;
            }
            else
            {
                DorkToScan.Next = null;
                firstResultDork = DorkToScan;

                if (parser.Blocked)
                {
                    DorkToScan.ScanResult = (int)RESULT_STATUS.Blocked;
                    DorkToScan.ResultURL = httpGet.ResponseUri;
                }
                else
                {
                    DorkToScan.ScanResult = (int)RESULT_STATUS.Nothing;
                }
            }

            _ScanStatus = (int)SCANTHREADSTATE.Finished;
            return true;
        }

        /// <summary>
        /// Abort the current http-request.
        /// </summary>
        public void Abort()
        {
            if (httpGet != null)
            {
                httpGet.Abort();
            }
        }

        /// <summary>
        /// Gets the DorkDone after the scan, with the result, status, etc.
        /// If there is more than one result, this DorkDone is the start of
        /// a single linked list, ended with null.
        /// </summary>
        public DorkDone ResultDork
        {
            get
            {
                return firstResultDork;
            }
        }

        /// <summary>
        /// Gets the count of results a scan delivered.
        /// </summary>
        public int Count
        {
            get
            {
                return parsedResults.Count;
            }
        }

        /// <summary>
        /// Gets the status of a finished scan.
        /// </summary>
        public int ScanStatus
        {
            get
            {
                return _ScanStatus;
            }
            set
            {
                _ScanStatus = value;
            }
        }
    }
}
