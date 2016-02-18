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

namespace GoolagScanner
{
    /// <summary>
    /// Out own implementation of a TraceListener. Reason to do this is that we
    /// want the trace output inside of a textarea inside our application.
    /// </summary>
    class TextAreaTraceListerner : TraceListener
    {
        private System.Windows.Forms.TextBox consoletb;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ctextbox">System.Windows.Forms.TextBox to log to.</param>
        public TextAreaTraceListerner(System.Windows.Forms.TextBox ctextbox)
            : base()
        {
            consoletb = ctextbox;
        }

        /// <summary>
        /// Write message to Trace window.
        /// </summary>
        /// <param name="tracemsg">Trace message.</param>
        public override void Write(string tracemsg)
        {
            if (!consoletb.IsDisposed)
            {
                consoletb.AppendText(tracemsg);
            }
        }

        /// <summary>
        /// Write message to Trace window, add newline at the end.
        /// </summary>
        /// <param name="tracemsg">Trace message.</param>
        public override void WriteLine(string tracemsg)
        {
            if (!consoletb.IsDisposed)
            {
                consoletb.AppendText(tracemsg + System.Environment.NewLine);
            }
        }
    }
}
