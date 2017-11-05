using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebSocketUtility
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static WebSocketUtility()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static WebSocketVersion GetWebSocketVersionByValue(string val)
        {
            switch (val)
            {
                case "6":
                case "06" :
                    return WebSocketVersion.WSV06;

                case "13" :
                    return WebSocketVersion.WSV13;
            }

            return WebSocketVersion.WSV00;
        }
        #endregion
    }
}
