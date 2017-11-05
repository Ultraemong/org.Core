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
    public static class WebSocketHandshakerFactory
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static WebSocketHandshakerFactory()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static IWebSocketHandshaker CreateWebSocketHandshaker(WebSocketClient connection)
        {
            switch (connection.Header.WebSocketVersion)
            {
                case WebSocketVersion.WSV13 :
                    return new WebSocketHandshakerWsv13(connection);

                case WebSocketVersion.WSV06 :
                    return new WebSocketHandshakerWsv06(connection);
            }

            return new WebSocketHandshakerWsv00(connection);
        }
        #endregion
    }
}
