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
using System.IO;
using System.Xml;
using System.Collections;

namespace GoolagScanner
{
    /// <summary>
    /// XmlReader for reading and interpreting xml-dork-data.
    /// Sets one collection of Categories and one for the Dorks.
    /// </summary>
    class XmlDorkSet
    {
        protected XmlDocument xmldoc;
        protected ArrayList dorkcollection;
        protected Categories categorycollection;
        private int dorksCount;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dorklist">Dorklist to fill.</param>
        /// <param name="categories">Categories to fill.</param>
        public XmlDorkSet(ArrayList dorklist, Categories categories)
        {
            dorksCount = 0;
            dorkcollection = dorklist;
            categorycollection = categories;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected XmlDorkSet()
        {
            dorksCount = 0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dorklist">Dorklist to fill.</param>
        protected XmlDorkSet(ArrayList dorklist)
        {
            dorkcollection = dorklist;
            dorksCount = 0;
        }

        /// <summary>
        /// Set a dorklist (normally done by construction).
        /// </summary>
        /// <param name="dorklist">Dorklist to fill.</param>
        public void setDorkList(ArrayList dorklist)
        {
            dorkcollection = dorklist;
        }

        /// <summary>
        /// Set a categories object (normally done by construction).
        /// </summary>
        /// <param name="categories">Categories to fill.</param>
        public void setCategoriesContainer(Categories categories)
        {
            categorycollection = categories;
        }

        /// <summary>
        /// Open XML Dork file.
        /// </summary>
        /// <param name="filename">Name of the file to open (qfn).</param>
        /// <returns>Zero on success.</returns>
        public int Open(string filename)
        {
            if (File.Exists(filename))
            {
                xmldoc = new XmlDocument();

                try
                {
                    xmldoc.Load(filename);
                }
                catch (XmlException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ConformanceLevel = ConformanceLevel.Auto;
                settings.IgnoreWhitespace = true;
                settings.IgnoreComments = true;

                XmlNodeReader nodereader = new XmlNodeReader(xmldoc);
                XmlReader reader = XmlReader.Create(nodereader, settings);
                Dork myDork = null;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "CategoryItem")
                        {
                            reader.Read();
                            categorycollection.Add( reader.Value );
                        }

                        if (reader.Name == "Dork")
                        {
                            myDork = new Dork();
                            reader.Read();
                            myDork.Name = reader.Value;
                        }

                        if (reader.Name == "Title")
                        {
                            reader.Read();
                            myDork.Title = reader.Value;
                        }

                        if (reader.Name == "Category")
                        {
                            reader.Read();
                            myDork.Category = reader.Value;
                        }

                        if (reader.Name == "Query")
                        {
                            reader.Read();
                            myDork.Query = reader.Value;
                        }

                        if (reader.Name == "Comment")
                        {
                            reader.Read();
                            myDork.Comment = reader.Value;

                            if (String.IsNullOrEmpty(myDork.Title))
                            {
                                myDork.Title = myDork.Name;
                            }
                            dorkcollection.Add(myDork);
                            dorksCount++;
                        }

                    } //isElement

                } // while
                return 0;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        /// <summary>
        /// Get Dork count.
        /// </summary>
        /// <returns>Number of dorks loaded.</returns>
        protected int getDorksCount()
        {
            return dorksCount;
        }

        /// <summary>
        /// Get number of dorks loaded.
        /// </summary>
        public int Count
        {
            get
            {
                return dorksCount;
            }
        }
    }
}
