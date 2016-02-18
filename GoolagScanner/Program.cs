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
using System.Windows.Forms;
using System.Diagnostics;

namespace GoolagScanner
{
    /// <summary>
    /// Application instance.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Properties.Settings.Default.ShowSplash)
            {
                Splash_cDc = new Splash(320, 428, global::GoolagScanner.Properties.Resources.Cdc009, 0.025, 0.03, 3);
                Splash_cDc.Show();
                Application.DoEvents();
            }

            Application.Run(new GScanForm());
        }

        internal static Splash Splash_cDc = null;
        internal static Splash Splash_Goolag = null;
    }
}
