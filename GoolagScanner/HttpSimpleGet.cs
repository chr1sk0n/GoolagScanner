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
using System.Web;
using System.IO;
using System.Diagnostics;

namespace GoolagScanner
{
    /// <summary>
    /// Class to do a Http GET request. Simple, because it does nothing more.
    /// Nonetheless, it supports timeout and proxy.
    /// </summary>
    class HttpSimpleGet
    {
        private string _rawResults = "";
        private string _errMessage = "";
        private int _timeOut = 10000;
        private HttpWebRequest _webRequest = null;
        private static WebProxy _proxy;
        private string _responseUri;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="timeout">Time-out in msecs.</param>
        public HttpSimpleGet(int timeout)
        {
            _timeOut = timeout;
        }

        /// <summary>
        /// Do the web request.
        /// </summary>
        /// <param name="request">The http-request to get.</param>
        /// <returns>True on success.</returns>
        public bool Do(string request)
        {
            HttpWebResponse webResponse;

            _webRequest = (HttpWebRequest)WebRequest.Create(request);
            _webRequest.UserAgent = Properties.Settings.Default.UserAgent;
            _webRequest.Accept = "text/xml,application/xml,application/xhtml+xml,text/html,text/plain,image/png,*/*";
            _webRequest.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8");
            _webRequest.Method = "GET";
            _webRequest.ProtocolVersion = HttpVersion.Version11;
            _webRequest.Timeout = this._timeOut;
            _webRequest.AllowAutoRedirect = true;
            _webRequest.MaximumAutomaticRedirections = 5;

            if (_proxy != null)
            {
                try
                {
                    _webRequest.Proxy = _proxy;
                }
                catch (System.NotSupportedException snse)
                {
                    _webRequest.Proxy = null;
                    Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError, snse.Message,
                        "HttpSimpleGet proxy error");
                }
            }

            try
            {
                webResponse = (HttpWebResponse)_webRequest.GetResponse();
            }
            catch (Exception exception)
            {
                _errMessage = exception.Message;
                return false;
            }

            _responseUri = webResponse.ResponseUri.ToString();

            try
            {
                Stream receiveStream = webResponse.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                _rawResults = readStream.ReadToEnd();
                readStream.Close();

            }
            catch (WebException wex)
            {
                Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError, "WebResponse empty or not readable.");

                _errMessage = wex.Message;
                return false;
            }
            catch (IOException iox)
            {
                Trace.WriteLineIf(Debug.Trace.TraceGoolag.TraceError, "IO-Error on network-device.");

                _errMessage = iox.Message;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Abort the request. Believe in the framework :)
        /// </summary>
        public void Abort()
        {
            _webRequest?.Abort();
        }

        /// <summary>
        /// Get the content of the requested page as string.
        /// </summary>
        /// <returns>Requested page, represented as string</returns>
        public string GetResults()
        {
            return _rawResults;
        }

        /// <summary>
        /// Get last error message.
        /// </summary>
        /// <returns>Error message</returns>
        public string GetErrorMessage()
        {
            return _errMessage;
        }

        /// <summary>
        /// Sets the proxy to use.
        /// </summary>
        public static WebProxy Proxy
        {
            set
            {
                _proxy = value;
            }
        }

        /// <summary>
        /// Get the URI with which the request responded.
        /// </summary>
        public string ResponseUri => _responseUri;
    }
}
