using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.IO;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class WebSocketHandshakerWsv13 : WebSocketHandshakerBase
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public WebSocketHandshakerWsv13(WebSocketClient connection)
            : base(connection)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override WebSocketHeader GetResponseHeader()
        {
            WebSocketHeader _response   = new WebSocketHeader();

            _response.Version           = "HTTP/1.1 101 Switching Protocols";
            _response.Upgrade           = "websocket";
            _response.Connection        = "Upgrade";
            _response.WebSocketAccept   = GetComputedBase64String();

            return _response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] ComputeHash(string key)
        {
            string _key2 = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
            SHA1CryptoServiceProvider _sha1 = new SHA1CryptoServiceProvider();

            return _sha1.ComputeHash(UTF8Encoding.ASCII.GetBytes(string.Format("{0}{1}", key, _key2)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetComputedBase64String()
        {
            return Convert.ToBase64String(ComputeHash(CurrentConnection.Header.WebSocketKey));
        }

        #endregion
    }
}
