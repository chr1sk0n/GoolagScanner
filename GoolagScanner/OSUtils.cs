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
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace GoolagScanner
{
    /// <summary>
    /// Methods, mostly static, for accessing very basic parts of the framework or operating system.
    /// </summary>
    sealed class OSUtils
    {
        /// <summary>
        /// Constructor. Never used.
        /// </summary>
        private OSUtils()
        {
        }

        /// <summary>
        /// Get path to the execution assembly, which is the .exe started.
        /// </summary>
        /// <returns>Full qualified name (absolute) to the execution assembly, ending with a separator.</returns>
        public static string getApplicationPath()
        {
            System.Reflection.Module runningModule = Assembly.GetExecutingAssembly().GetModules()[0];
            int len1 = runningModule.Name.Length;
            string path = runningModule.FullyQualifiedName;
            path = path.Remove((path.Length - len1), len1);
            if (!path.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
            {
                path += System.IO.Path.DirectorySeparatorChar.ToString();
            }
            return path;
        }

        /// <summary>
        /// Run (fork) a given process with the given arguments.
        /// </summary>
        /// <param name="mycmd">Process to run.</param>
        /// <param name="myarg">Argv to use.</param>
        public static void runProcess(string mycmd, string myarg)
        {
            Process p = new Process();
            p.EnableRaisingEvents = false;
            p.StartInfo.FileName = mycmd;
            p.StartInfo.Arguments = myarg;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            try
            {
                p.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Could not start process: " + mycmd);
            }
        }

        /// <summary>
        /// Open document as forked process on basis of the document's association.
        /// </summary>
        /// <param name="Document">Document.</param>
        public static void runDocument(string Document)
        {
            Process process = new Process();
            process.EnableRaisingEvents = false;
            process.StartInfo.FileName = Document;
            process.StartInfo.Arguments = "";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.StartInfo.Verb = "open";
            process.StartInfo.UseShellExecute = true;

            try
            {
                process.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Could not open process for document: " + Document);
            }
        }

        /// <summary>
        /// This ecapsulated both methods we have to open the browser. This should
        /// be the prefered method to achive this.
        /// </summary>
        /// <param name="DocumentURL">The full http-address to the document.</param>
        public static void OpenInBrowser(string DocumentURL)
        {
            if (Properties.Settings.Default.UseSystemBrowser)
            {
                runDocument(DocumentURL);
            }
            else
            {
                runProcess(Properties.Settings.Default.PreferredBrowser, DocumentURL);
            }
        }

    }
}
