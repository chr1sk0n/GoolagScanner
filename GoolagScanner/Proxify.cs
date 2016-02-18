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
using System.Net;
using System.Diagnostics;

namespace GoolagScanner
{
    /// <summary>
    /// Class for getting the actual proxy on the system, or the
    /// one that was selected by the user.
    /// </summary>
    class Proxify
    {
        /// <summary>
        /// Proxy factory.
        /// </summary>
        /// <returns>New WebProxy object, set to the proxy to use.</returns>
        public WebProxy GetProxy()
        {
            WebProxy proxy = new WebProxy();

            if (Properties.Settings.Default.UseSystemProxy == true)
            {
                // we just need the object, no request is done
                WebRequest request = WebRequest.Create("http://cultdeadcow.com");
                // Obtain the 'Proxy' of the  Default browser.  
                proxy = request.Proxy as WebProxy;
            }
            else
            {
                if (!String.IsNullOrEmpty(Properties.Settings.Default.ProxyAddress))
                {
                    try
                    {
                        Uri ProxyUri = new Uri(Properties.Settings.Default.ProxyAddress);
                        proxy.Address = ProxyUri;
                    }
                    catch (System.UriFormatException ufe)
                    {
                        proxy = null;
                        Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError, ufe.Message);
                    }
                }
                else
                {
                    // otherwise we dont use a proxy at all
                    // but there may be something set, anyhow (by ISA or something like that)
                    // will be null if there's still nothing
                    proxy = WebRequest.DefaultWebProxy as WebProxy;
                }
            }

            string ProxyAsString;

            if (proxy != null)
            {
                ProxyAsString = proxy.Address.ToString();
            }
            else
            {
                ProxyAsString = "(no proxy set)";
            }

            Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceVerbose, ProxyAsString, "Proxy is ");
            return proxy;
        }
    }
}
