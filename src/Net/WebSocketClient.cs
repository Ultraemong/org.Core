using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class WebSocketClient
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public WebSocketClient()
        {
        }
        #endregion

        #region Fields
        WebSocketHeader     _header         = null;
        byte[]              _buffer         = new byte[1024];
        Socket              _connection     = null;
        WebSocketStatus     _status         = WebSocketStatus.Initialized;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public WebSocketStatus Status
        {
            get
            {
                return _status;
            }

            private set
            {
                _status = value;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public WebSocketHeader Header
        {
            get
            {
                if (_header == null)
                    _header = WebSocketHeaderParser.Parse(_connection);

                return _header;
            }
        }
        #endregion

        #region Methods
        public void Listen()
        {
            _connection.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, _Read, null);
        }

        void _Read(IAsyncResult result)
        {
            int sizeOfReceivedData = _connection.EndReceive(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="connection"></param>
        internal static T Create<T>(Socket connection)
            where T : WebSocketClient, new()
        {
            T _retVal = new T();

            _retVal._connection = connection;
            //_retVal._connection.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, Global.Settings.WebSocket.ReceiveTimeout);

            return _retVal;
        }

        internal static void StartListen(WebSocketClient client)
        {
            client.Status = WebSocketStatus.Connected;

            //client._connection.BeginReceive(client._buffer, 0, client._buffer.Length, SocketFlags.None, _Read, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static implicit operator Socket(WebSocketClient client)
        {
            return client._connection;
        }
        #endregion
    }
}
