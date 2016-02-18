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

namespace GoolagScanner
{
    /// <summary>
    /// Interface which describes what we require for (ab)using a search engine.
    /// Google is just one implementation of that. Others are possible.
    /// </summary>
    interface IScanProvider
    {
        /// <summary>
        /// URL to the search engine.
        /// </summary>
        string HostUrl
        {
            get;
        }

        /// <summary>
        /// Command to query something from a search engine.
        /// </summary>
        string QueryCommand
        {
            get;
        }

        /// <summary>
        /// Command to search for a site with that search engine.
        /// </summary>
        string TargetSite
        {
            get;
        }

        /// <summary>
        /// Command to omit a site using that search engine.
        /// </summary>
        string OmitSite
        {
            get;
        }

        /// <summary>
        /// Command to specify a page with results using that search engine.
        /// </summary>
        string ResultPageModifier
        {
            get;
        }
    }
}
