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
    public class UploadCompletedEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userToken"></param>
        public UploadCompletedEventArgs(object userToken)
            : base()
        {
            _userToken = userToken;
        }
        #endregion

        #region Fields
        readonly object _userToken = null;
        #endregion

        #region Properties
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
