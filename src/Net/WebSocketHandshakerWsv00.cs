using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class WebSocketHandshakerWsv00 : WebSocketHandshakerBase
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public WebSocketHandshakerWsv00(WebSocketClient connection)
            : base(connection)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        protected override WebSocketHeader GetResponseHeader()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
