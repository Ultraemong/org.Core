using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WebSocketServer<T> : IDisposable
        where T : WebSocketClient, new()
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public WebSocketServer()
        {
        }
        #endregion

        #region Fields
        string                      _origin             = string.Empty;
        string                      _location           = string.Empty;
        int                         _maxQueueLength     = Int32.MinValue;

        Socket                      _listener           = null;
        IPEndPoint                  _endPoint           = null;
        WebSocketClientCollection   _clients            = null;
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public virtual Socket Listener
        {
            get
            {
                if (_listener == null)
                    _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

                return _listener;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public WebSocketClientCollection Clients
        {
            get
            {
                if (_clients == null)
                {
                    _clients = new WebSocketClientCollection();
                }

                return _clients;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Origin
        {
            get
            {
                //if (string.IsNullOrEmpty(_origin))
                //    _origin = Global.Settings.WebSocket.Origin;

                return _origin;
            }

            set
            {
                _origin = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Location
        {
            get
            {
                //if (string.IsNullOrEmpty(_location))
                //    _location = Global.Settings.WebSocket.Location;

                return _location;
            }

            set
            {
                _location = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual IPEndPoint EndPoint
        {
            get
            {
                //if (null == _endPoint)
                //{
                //    if (string.IsNullOrEmpty(Global.Settings.WebSocket.EndPointAddress))
                //        throw new InvalidOperationException(string.Format("An instance of {0} is unable to create due to the fact that address is null.", GetType()));

                //    else if (Global.Settings.WebSocket.EndPointPort.Equals(Int32.MinValue))
                //        throw new InvalidOperationException(string.Format("An instance of {0} is unable to create due to the fact that port number is null.", GetType()));

                //    _endPoint = new IPEndPoint(IPAddressParser.Parse(Global.Settings.WebSocket.EndPointAddress), Global.Settings.WebSocket.EndPointPort);
                //}

                return _endPoint;
            }

            set
            {
                _endPoint = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int MaxQueueLength
        {
            get
            {
                //if (_maxQueueLength.Equals(Int32.MinValue))
                //    _maxQueueLength = Global.Settings.WebSocket.MaxQueueLength;

                return _maxQueueLength;
            }

            set
            {
                _maxQueueLength = value;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            Listener.Bind(EndPoint);
            Listener.Listen(MaxQueueLength);

            Listener.BeginAccept(new AsyncCallback(_AcceptCallback), null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        void _AcceptCallback(IAsyncResult result)
        {
            Socket _connection = Listener.EndAccept(result);

            try
            {
                WebSocketClient         _client     = WebSocketClient.Create<T>(_connection);
                IWebSocketHandshaker    _handshaker = WebSocketHandshakerFactory.CreateWebSocketHandshaker(_client);

                _handshaker.Handshake();

                Clients.Add(_client);
            }
            catch (Exception ex)
            {
                ex = ex = null;

                _connection.Close();
                _connection.Dispose();
            }
            finally
            {
                Listener.BeginAccept(new AsyncCallback(_AcceptCallback), null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (null != _listener)
            {
                _listener.Close();
                _listener.Dispose();
            }
        }
        #endregion
    }
}
