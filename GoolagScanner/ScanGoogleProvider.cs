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
    /// Implementation of IScanProvider. This one is Google.
    /// </summary>
    class ScanGoogleProvider : IScanProvider
    {
        /// <summary>
        /// URL to Google.
        /// </summary>
        string IScanProvider.HostUrl
        {
            get
            {
                return "https://www.google.com";
            }
        }

        /// <summary>
        /// Command to query something from Google.
        /// </summary>
        string IScanProvider.QueryCommand
        {
            get
            {
                return "/search?q=";
            }
        }

        /// <summary>
        /// Command to search for a site with Google.
        /// </summary>
        string IScanProvider.TargetSite
        {
            get
            {
                return "+site:";
            }
        }

        /// <summary>
        /// Command to omit a site using Google.
        /// </summary>
        string IScanProvider.OmitSite
        {
            get
            {
                return "-site:";
            }
        }

        /// <summary>
        /// Command to specify a page with results using Google.
        /// </summary>
        string IScanProvider.ResultPageModifier
        {
            get
            {
                return "&start=";
            }
        }
    }
}
