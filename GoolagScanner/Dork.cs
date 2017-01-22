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
    /// The basic Dork object (pure data, without metadata or gui context)
    /// </summary>
    public class Dork : IComparable
    {
        protected string theName;
        protected string theTitle;
        protected string theCategory;
        protected string theQuery;
        protected string theComment;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Dork()
        {
            theName = "";
            theTitle = "";
            theCategory = "";
            theQuery = "";
            theComment = "";
        }

        /// <summary>
        /// Gets or sets the name of a Dork.
        /// </summary>
        public string Name
        {
            get
            {
                return theName;
            }
            set
            {
                theName = value;
            }
        }

        /// <summary>
        /// Gets or sets the query of a Dork.
        /// </summary>
        public string Query
        {
            set
            {
                theQuery = value;
            }
            get
            {
                return theQuery;
            }
        }

        /// <summary>
        /// Gets or sets the title of a Dork.
        /// </summary>
        public string Title
        {
            set
            {
                theTitle = value;
            }
            get
            {
                return theTitle;
            }
        }

        /// <summary>
        /// Gets or sets the category of a Dork.
        /// </summary>
        public string Category
        {
            set
            {
                theCategory = value;
            }
            get
            {
                return theCategory;
            }
        }

        /// <summary>
        /// Gets or sets the comment of a Dork.
        /// </summary>
        public string Comment
        {
            set
            {
                theComment = value;
            }
            get
            {
                return theComment;
            }
        }

        /// <summary>
        /// Comperator.
        /// </summary>
        /// <param name="d">Probably another Dork</param>
        /// <returns>less than zero, zero, greater that zero</returns>
        public int CompareTo(Object d)
        {
            Dork dork = d as Dork;
            return String.Compare(theName, dork?.Name, StringComparison.Ordinal);
        }
    }
}
