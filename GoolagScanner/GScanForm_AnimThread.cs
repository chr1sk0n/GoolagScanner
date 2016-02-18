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
using System.Collections;
using System.IO;
using System.Threading;

namespace GoolagScanner
{
    public partial class GScanForm : Form
    {
        /// <summary>
        /// Thread-worker of animation-thread. Just animates the small progress-bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doThreadAnim(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;
            while (true)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    if (progressBar1.Value == 100)
                    {
                        progressBar1.Value = 0;
                    }
                    progressBar1.PerformStep();
                    Thread.Sleep(animSpeed);
                }
            }
        }

        /// <summary>
        /// Called from background worker when animation-thread finishes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinalThreadAnim(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Update();
        }

    }
}
