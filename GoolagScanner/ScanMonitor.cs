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
using System.Diagnostics;
using System.Threading;

namespace GoolagScanner
{
    /// <summary>
    /// This class takes care of multiple, parallel scans. For this, it provides
    /// two data-structures, a list for the scans to do, and a queue for scans
    /// that finished - with all neccesary accessors to them. Takes care of
    /// synchronizing. An own thread, a watcher, handles the transport between
    /// the different pipelines. Running scans are encapsulated by an inner class.
    /// So it even would be possible to use different scan engines, and or course
    /// different scan providers in one parallised scan.
    /// </summary>
    class ScanMonitor : IDisposable
    {
        /// <summary>
        /// A scan that is running or that has to be run.
        /// </summary>
        class ActiveScan
        {
            Scanner scanner;
            Thread thread;

            /// <summary>
            /// Gets or sets a scanner.
            /// </summary>
            public Scanner Scanner
            {
                get
                {
                    return scanner;
                }
                set
                {
                    scanner = value;
                }
            }

            /// <summary>
            /// Gets or sets a Thread.
            /// </summary>
            public Thread Thread
            {
                get
                {
                    return thread;
                }
                set
                {
                    thread = value;
                }
            }
        }

        private int MaxThreads;
        private Queue<Scanner> FinishedScans = new Queue<Scanner>();
        private System.Object FinishedScansLock = new System.Object();
        private List<ActiveScan> ScansTodo = new List<ActiveScan>();
        private System.Object ScansTodoLock = new System.Object();
        private Thread WatcherThread;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="MaxParallelScans">Number of scans to run in parallel.</param>
        public ScanMonitor(int MaxParallelScans)
        {
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo,
                MaxParallelScans, "Monitor initialised. Threads to use");

            MaxThreads = MaxParallelScans;
            WatcherThread = new Thread(this.Watcher);
            WatcherThread.Start();
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose,
                "Monitor: Watcher started.");
        }

        /// <summary>
        /// Add a scanner for processing.
        /// </summary>
        /// <param name="scanner">Instance of Scanner object.</param>
        public void Add(Scanner scanner)
        {
            ActiveScan NewScanThread = new ActiveScan();

            NewScanThread.Scanner = scanner;
            NewScanThread.Thread = new Thread(scanner.Do);

            lock (ScansTodoLock)
            {
                ScansTodo.Add(NewScanThread);
            }

            NewScanThread.Thread.Start();
        }

        /// <summary>
        /// Checks if a Thread is avail for scanning.
        /// </summary>
        /// <returns>True is a thread is avail.</returns>
        public bool IsThreadAvail()
        {
            bool isavail;
            lock (ScansTodoLock)
            {
                isavail = ScansTodo.Count < MaxThreads;
            }
            return isavail;
        }

        /// <summary>
        /// Checks if results a available in the queue.
        /// </summary>
        /// <returns>True if we have results.</returns>
        public bool HasResults()
        {
            bool hasresults;
            lock (FinishedScansLock)
            {
                hasresults = FinishedScans.Count > 0;
            }
            return hasresults;
        }

        /// <summary>
        /// Checks if there are currently scans running or waiting for getting processed.
        /// </summary>
        /// <returns>True if we have pending scans.</returns>
        public bool HasPendingScans()
        {
            bool haspendingscans;
            lock (ScansTodoLock)
            {
                haspendingscans = ScansTodo.Count > 0;
            }
            return haspendingscans;
        }

        /// <summary>
        /// Get the last finished Scanner from the queue.
        /// </summary>
        /// <returns></returns>
        public Scanner GetFinishedScanner()
        {
            Scanner s = null;
            lock (FinishedScansLock)
            {
                s = FinishedScans.Dequeue();
            }
            return s;
        }

        /// <summary>
        /// Worker. Takes care for finished or aborted scans, periodically.
        /// </summary>
        public void Watcher()
        {
            while (true)
            {
                lock (ScansTodoLock)
                {
                    ActiveScan activescan;
                    for (int i = 0; i < ScansTodo.Count; i++)
                    {
                        activescan = ScansTodo[i];
                        if (activescan.Scanner.ScanStatus == (int)SCANTHREADSTATE.Finished
                            || activescan.Scanner.ScanStatus == (int)SCANTHREADSTATE.Aborted)
                        {
                            lock (FinishedScansLock)
                            {
                                FinishedScans.Enqueue(activescan.Scanner);
                            }
                            ScansTodo.RemoveAt(i);
                            break;
                        }
                    }
                }
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Somehow our desctructor. Stops all active scans, aborts Watcher thread.
        /// </summary>
        public void Dispose()
        {
            lock (ScansTodoLock)
            {
                foreach (ActiveScan activescan in ScansTodo)
                {
                    activescan.Scanner.Abort();
                    activescan.Thread.Abort();
                }
            }
            WatcherThread.Abort();
        }

        /// <summary>
        /// Stops all active and pending scans and sets their status to Aborted.
        /// </summary>
        /// <returns>The count of stopped scans.</returns>
        public int StopAllActive()
        {
            int _scansstopped = 0;
            lock (ScansTodoLock)
            {
                foreach (ActiveScan activescan in ScansTodo)
                {
                    if (activescan.Scanner.ScanStatus != (int)SCANTHREADSTATE.Aborted)
                    {
                        activescan.Scanner.Abort();
                        activescan.Thread.Abort();
                        activescan.Scanner.ScanStatus = (int)SCANTHREADSTATE.Aborted;
                        _scansstopped++;
                    }
                }
            }
            return (_scansstopped);
        }
    }
}
