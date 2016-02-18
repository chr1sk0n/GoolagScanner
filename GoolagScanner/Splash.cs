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
using System.Threading;
using System.Diagnostics;

namespace GoolagScanner
{
    /// <summary>
    /// Splashscreen. Basically a Form which fades in/out a background-image.
    /// </summary>
    public partial class Splash : Form
    {
        private Thread OpacThread;
        private double fadeinOffset;
        private double fadeoutOffset;
        private int waittime;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="width">Width of picture/form in pixel</param>
        /// <param name="height">Height of picture/form in pixel</param>
        /// <param name="bitmap">System.Drawing.Bitmap object</param>
        /// <param name="FadeInOffset">Time to fade-in in msecs</param>
        /// <param name="FadeOutOffset">Time to fade-out in msecs</param>
        /// <param name="WaitTime">Time to display the picture after fading-in in msecs</param>
        public Splash(int width, int height, System.Drawing.Bitmap bitmap, double FadeInOffset, double FadeOutOffset, int WaitTime)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            fadeinOffset = FadeInOffset;
            fadeoutOffset = FadeOutOffset;
            waittime = WaitTime;
            this.BackgroundImage = bitmap;
            this.ClientSize = new System.Drawing.Size(width, height);
            this.Opacity = 0;
            Update();
            OpacThread = new Thread(OpacAnim);
            OpacThread.Start();
        }

        /// <summary>
        /// Worker. Fade-in, wait, fade-out.
        /// </summary>
        private void OpacAnim()
        {
            while (this.Opacity < 1)
            {
                this.Opacity += fadeinOffset;
                Application.DoEvents();
                Thread.Sleep(60);
            }

            Thread.Sleep(waittime);
            
            while (this.Opacity > 0)
            {
                this.Opacity -= fadeoutOffset;
                Application.DoEvents();
                Thread.Sleep(60);
            }
        }
    }
}