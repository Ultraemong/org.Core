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
    public class WebSocketHeader
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public WebSocketHeader()
        {
            _repository = new Dictionary<string, string>();
        }
        #endregion

        #region Fields
        Dictionary<string, string> _repository = default(Dictionary<string, string>);
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Version
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Host
        {
            get
            {
                return GetValueByKey("Host", string.Empty);
            }

            set
            {
                SetValueWithKey("Host", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserAgent
        {
            get
            {
                return GetValueByKey("User-Agent", string.Empty);
            }

            set
            {
                SetValueWithKey("User-Agent", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Origin
        {
            get
            {
                return GetValueByKey("Origin", string.Empty);
            }

            set
            {
                SetValueWithKey("Origin", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Connection
        {
            get
            {
                return GetValueByKey("Connection", string.Empty);
            }

            set
            {
                SetValueWithKey("Connection", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Upgrade
        {
            get
            {
                return GetValueByKey("Upgrade", string.Empty);
            }

            set
            {
                SetValueWithKey("Upgrade", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string WebSocketKey
        {
            get
            {
                return GetValueByKey("Sec-WebSocket-Key", string.Empty);
            }

            set
            {
                SetValueWithKey("Sec-WebSocket-Key", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string WebSocketAccept
        {
            get
            {
                return GetValueByKey("Sec-WebSocket-Accept", string.Empty);
            }

            set
            {
                SetValueWithKey("Sec-WebSocket-Accept", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string WebSocketProtocol
        {
            get
            {
                return GetValueByKey("Sec-WebSocket-Protocol", string.Empty);
            }

            set
            {
                SetValueWithKey("Sec-WebSocket-Protocol", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public WebSocketVersion WebSocketVersion
        {
            get
            {
                return WebSocketUtility.GetWebSocketVersionByValue(GetValueByKey("Sec-WebSocket-Version", string.Empty));
            }

            set
            {
                SetValueWithKey("Sec-WebSocket-Version", value.ToString());
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public string GetValueByKey(string key, string defVal = null)
        {
            if (_repository.ContainsKey(key))
                return _repository[key] ?? defVal;

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void SetValueWithKey(string key, string val)
        {
            if (_repository.ContainsKey(key))
                _repository.Remove(key);

            _repository.Add(key, val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var _builder = new StringBuilder();

            _builder.AppendFormat("{0}{1}", Version, Environment.NewLine);

            foreach (string _key in _repository.Keys)
                _builder.AppendFormat("{0}: {1}{2}", _key, _repository[_key], Environment.NewLine);

            _builder.Append(Environment.NewLine);

            return _builder.ToString();
        }
        #endregion
    }
}
