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
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace GoolagScanner
{
    /// <summary>
    /// About dialog. Basically generated from Studio, just a little pimped up.
    /// </summary>
    partial class About : Form
    {
        /// <summary>
        /// URL to Google's Terms of Service.
        /// </summary>
        const string URI_Google_TOS
            = "http://www.google.com/accounts/TOS";

        /// <summary>
        /// URL to GoolagScanner's License.
        /// </summary>
        const string URI_agplv3
            = "http://www.fsf.org/agplv3-pr";

        /// <summary>
        /// Constructor. Sets some elemets of of assembly and properties.
        /// </summary>
        public About()
        {
            InitializeComponent();

            //  Initialize the AboutBox to display the product information from the assembly information.
            //  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
            this.Text = String.Format("About {0}", AssemblyTitle);
            //this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            //this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;
            this.showAtStartCB.Checked = Properties.Settings.Default.ShowAboutAtStart;
            this.okButton.Focus();
            Update();
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        /// <summary>
        /// Open browser and navigate to the license (GNU AGPL v3.0).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showLicenseButton_Click(object sender, EventArgs e)
        {
            OSUtils.OpenInBrowser(URI_agplv3);
        }

        /// <summary>
        /// Open browser and navigate to Google's Terms of Service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showGTOSButton_Click(object sender, EventArgs e)
        {
            OSUtils.OpenInBrowser(URI_Google_TOS);
        }

        /// <summary>
        /// UI-Event. Ok was clicked, checks if this should be shown at start-up,
        /// (and saves this state in the settings).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOKClick(object sender, EventArgs e)
        {
            if (showAtStartCB.Checked != Properties.Settings.Default.ShowAboutAtStart)
            {
                Properties.Settings.Default.ShowAboutAtStart = showAtStartCB.Checked;
                Properties.Settings.Default.Save();
            }
            this.Dispose();
        }
    }
}
