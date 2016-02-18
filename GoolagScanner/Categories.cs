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
    /// Container for the dork categories,
    /// a set of objects of type Category
    /// </summary>
    public class Categories
    {
        /// <summary>
        /// Sample categories, normally they should never be used, as
        /// the categories are read from the xml.
        /// </summary>
        private string[] cats = {
            "Advisories and Vulnerabilities",
            "Error Messages",
            "Files containing juicy info",
            "Files containing passwords",
            "Files containing usernames",
            "Footholds",
            "Pages containing login portals",
            "Pages containing network or vulnerability data",
            "Sensitive Directories",
            "Sensitive Online Shopping Info",
            "Various Online Devices",
            "Vulnerable Files",
            "Vulnerable Servers",
            "Web Server Detection"
        };

        List<Category> l_categories;

        /// <summary>
        /// Constructor. Can preset some categories.
        /// </summary>
        /// <param name="usePreset">True to create build-in categories</param>
        protected Categories(bool usePreset)
        {
            l_categories = new List<Category>();

            if (usePreset)
            {
                for (int i = 0; i < cats.Length; i++)
                {
                    Category c = new Category();
                    c.Text = cats[i];
                    c.Node = null;
                    l_categories.Add(c);
                }
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Categories()
        {
            l_categories = new List<Category>();
        }

        /// <summary>
        /// Creates a new category from a string and adds it to the collection.
        /// </summary>
        /// <param name="catName">Name of the category to add</param>
        public void Add(string catName)
        {
            Category c = new Category();
            c.Text = catName;
            c.Node = null;
            l_categories.Add(c);
        }

        /// <summary>
        /// Gets a category by index.
        /// </summary>
        /// <param name="i"></param>
        /// <returns>Returns Category object at the given index</returns>
        public Category getCategoryByIndex(int i)
        {
            return l_categories[i];
        }

        /// <summary>
        /// Gets the number of available categories.
        /// </summary>
        public int Count
        {
            get
            {
                return l_categories.Count;
            }
        }

        /// <summary>
        /// Enumerates trough the categroies.
        /// </summary>
        /// <returns>Current Category</returns>
        public IEnumerator<Category> GetEnumerator()
        {
            for (int i = 0; i < l_categories.Count; i++)
            {
                yield return l_categories[i];
            }
        }
    }
}
