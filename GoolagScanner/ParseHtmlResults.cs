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
using System.Diagnostics;

namespace GoolagScanner
{
    /// <summary>
    /// Parse the html-page.
    /// </summary>
    class ParseHtmlResults
    {
        private const String hitFound = "<div class=g>";
        private const String hitUrlFound = "<a href=\"";
        private IScanProvider scanProvider;
        private string _NextPage = "0";
        private int currentResultPage;
        private bool _blocked;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="scanprovider">Implementation of IScanProvider.</param>
        public ParseHtmlResults(IScanProvider scanprovider, int resultpage)
        {
            scanProvider = scanprovider;
            currentResultPage = resultpage;
            _blocked = false;
        }

        /// <summary>
        /// The next page that's available for fetching, as reported from
        /// the ScanProvider (aka Google).
        /// </summary>
        public int NextPage
        {
            get
            {
                try
                {
                    int n = Convert.ToInt32(_NextPage);
                    return n;
                }
                catch (Exception e)
                {
                    Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError, e.Message,
                        "ParseError on NextPage: ");
                    return 0;
                }
            }
        }

        /// <summary>
        /// Parses out the next numeric value, given a string and an index to start from.
        /// Also increments the index after the operation.
        /// </summary>
        /// <param name="sline">string to parse</param>
        /// <param name="sidx">index to start from</param>
        /// <returns>Next numeric value as string. Empty string if nothing was found.</returns>
        private static string getNextDigits(string sline, ref int sidx)
        {
            string s = "";
            do
            {
                Char u = sline[sidx++];
                if (Char.IsDigit(u))
                {
                    s += u;
                }
                else
                {
                    break;
                }
            }
            while (true);
            return s;
        }

        /// <summary>
        /// Parse a complete html document.
        /// Sets NextPage to the next page to fetch.
        /// </summary>
        /// <param name="toParse">html document as string</param>
        /// <returns>List of strings of found URLs</returns>
        public List<string> Parse(string toParse)
        {
            List<string> plist = new List<string>();

            int lastIdx = 0;
            int idx = 0;

            while ((idx = toParse.IndexOf(hitFound, lastIdx)) != -1)
            {
                idx = toParse.IndexOf(hitUrlFound, idx);

                if (idx == -1)
                {
                    continue;
                }

                int startDork = idx + hitUrlFound.Length;
                int endDork = toParse.IndexOf('"', startDork);

                if (endDork == -1)
                {
                    continue;
                }

                String dork = toParse.Substring(startDork, endDork - startDork);
                int lastQuote = dork.LastIndexOf('"');

                if (lastQuote != -1)
                {
                    dork.Remove(lastQuote, 1);
                }

                plist.Add(dork);
                lastIdx = endDork;
            }
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo, plist.Count, "Count of results ");

            // here we'll be able to check if we got blocked...
            if (idx == -1 && plist.Count == 0)
            {
                // Well, this is not a 'real' block-detection, at least not the way I would
                // love it... but Google's a bitch... for now it's okay.

                if (toParse.IndexOf("<title>403 Forbidden</title>") != -1)
                {
                    Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo, "We may got blocked.");
                    _blocked = true;
                    return plist;
                }
            }

            // now, here take a look if we have something more...
            lastIdx = toParse.Length - 1;

            lastIdx = toParse.LastIndexOf(scanProvider.ResultPageModifier);
            if (lastIdx != -1)
            {
                lastIdx += scanProvider.ResultPageModifier.Length;
                _NextPage = getNextDigits(toParse, ref lastIdx);
            }
            else
            {
                _NextPage = "0";
            }
            int nextpage = Convert.ToInt32(_NextPage);
            if (nextpage <= currentResultPage)
            {
                _NextPage = "0";
            }
            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceInfo, _NextPage, "Next page to request ");
            return plist;
        }

        /// <summary>
        /// Get flag if we were blocked. True if yes.
        /// </summary>
        public bool Blocked
        {
            get
            {
                return _blocked;
            }
        }
    }
}
