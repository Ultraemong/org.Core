using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.IO;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class WebSocketHandshakerBase : IWebSocketHandshaker
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public WebSocketHandshakerBase(WebSocketClient connection)
        {
            _connection = connection;
        }
        #endregion

        #region Fields
        WebSocketClient _connection = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        protected WebSocketClient CurrentConnection
        {
            get
            {
                return _connection;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public void Handshake()
        {
            WebSocketHeader _response = GetResponseHeader();

            if (_response != null)
            {
                using (NetworkStream _stream = new NetworkStream(CurrentConnection))
                {
                    if (_stream.CanWrite)
                    {
                        using (StreamWriter _writer = new StreamWriter(_stream))
                        {
                            _writer.Write(_response);
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        protected abstract WebSocketHeader GetResponseHeader();
        
        #endregion
    }
}
