using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

using org.Core.Extensions;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class WebSocketHeaderParser
    {
        #region Constructors
        static WebSocketHeaderParser()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public static WebSocketHeader Parse(Socket socket)
        {
            WebSocketHeader _retVal = null;

            using (NetworkStream _stream = new NetworkStream(socket))
            {
                _retVal = Parse(_stream);
            }

            return _retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static WebSocketHeader Parse(NetworkStream stream)
        {
            WebSocketHeader _retVal = null;

            if (stream.CanRead)
            {
                _retVal = new WebSocketHeader();

                int _byteRcvd   = Int32.MinValue;
                int _delimiter  = 23;

                List<byte> _buffer = new List<byte>();

                while (stream.DataAvailable)
                {
                    _byteRcvd = stream.ReadByte();

                    if (_buffer.Count > 1)
                    {
                        if (_delimiter.Equals(_byteRcvd + _buffer[(_buffer.Count - 1)]))
                        {
                            string _line = UTF8Encoding.ASCII.GetString(_buffer.ToArray(), 0, (_buffer.Count - 1));
                            string[] _tokens = _line.Trim().Split(new char[] { ':' }, 2);

                            if (_tokens.Length > 1)
                            {
                                string _key = _tokens.GetValueOrDefault(0, string.Empty).Trim();
                                string _val = _tokens.GetValueOrDefault(1, string.Empty).Trim();

                                if (!string.IsNullOrEmpty(_key)
                                    && !string.IsNullOrEmpty(_val))
                                {
                                    _retVal.SetValueWithKey(_key, _val);
                                }
                            }
                            else
                            {
                                if (_line.ContainsAny(new string[] { "HTTP", "GET" }))
                                {
                                    _retVal.Version = _line;
                                }
                            }

                            _buffer.Clear();

                            continue;
                        }
                    }

                    _buffer.Add((byte)_byteRcvd);
                }
            }

            return _retVal;
        }
        #endregion
    }
}
