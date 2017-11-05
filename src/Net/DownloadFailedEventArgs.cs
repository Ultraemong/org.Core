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
    public class DownloadFailedEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public DownloadFailedEventArgs(Exception exception)
            : base()
        {
            _exception = exception;
        }
        #endregion

        #region Fields
        readonly Exception _exception = null;
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
        #endregion
    }
}
