using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class WebRequestEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        public WebRequestEventArgs(WebRequestContext requestContext)
            : base()
        {
            _requestContext = requestContext;
        }
        #endregion

        #region Fields
        readonly WebRequestContext _requestContext = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public WebRequestContext RequestContext
        {
            get
            {
                return _requestContext;
            }
        }
        #endregion
    }
}
