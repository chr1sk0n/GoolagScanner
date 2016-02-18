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
    /// Intense of this class is simply the concatenation of strings, respecting Uri encoding,
    /// uses a ScanProvider to build up a valid Url.
    /// </summary>
    class RequestBuilder
    {
        private IScanProvider scanprovider;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="myScanProvider">Implementation of ISanProvider, to supply the needed keywords.</param>
        public RequestBuilder(IScanProvider myScanProvider)
        {
            scanprovider = myScanProvider;
        }

        /// <summary>
        /// Builds up a valid request to a scan provider (aka google).
        /// </summary>
        /// <param name="mdork">Dork to use.</param>
        /// <param name="site">Site to scan for.</param>
        /// <param name="resPage">Page of results to start with.</param>
        /// <returns>The request to query.</returns>
        public string getRequest(string mdork, string site, int resPage)
        {
            string validDork = StripSite(mdork);
            string myRequest = scanprovider.HostUrl + scanprovider.QueryCommand
                                + Uri.EscapeUriString(validDork);

            if (!String.IsNullOrEmpty(site))
            {
                myRequest = myRequest + " " + scanprovider.TargetSite + site;
            }

            if (resPage > 0)
            {
                myRequest = myRequest + scanprovider.ResultPageModifier + resPage.ToString();
            }

            return myRequest;
        }

        /// <summary>
        /// If we got a Dork which itself tries to omit some page, well, we remove this command.
        /// </summary>
        /// <param name="dorkquery">Query.</param>
        /// <returns>Query without any omit-site-commands.</returns>
        protected string StripSite(string dorkquery)
        {
            int omitidx = 0;
            while ((omitidx = dorkquery.IndexOf(scanprovider.OmitSite)) != -1) // so there is something like a '-site:'
            {
                int nextspace = dorkquery.IndexOf(' ', omitidx);
                if (nextspace == -1)
                {
                    nextspace = dorkquery.Length;
                }
                dorkquery = dorkquery.Remove(omitidx, nextspace - omitidx);
            }
            return dorkquery;
        }
    }
}
