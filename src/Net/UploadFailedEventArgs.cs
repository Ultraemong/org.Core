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
    public class UploadFailedEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="userToken"></param>
        public UploadFailedEventArgs(Exception exception, object userToken)
            : base()
        {
            _exception  = exception;
            _userToken  = userToken;
        }
        #endregion

        #region Fields
        readonly object     _userToken  = null;
        readonly Exception  _exception  = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Exception ThrownException
        {
            get
            {
                return _exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object UserToken
        {
            get
            {
                return _userToken;
            }
        }
        #endregion
    }
}
