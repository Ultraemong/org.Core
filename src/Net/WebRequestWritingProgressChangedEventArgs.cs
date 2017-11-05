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
    public class WebRequestWritingProgressChangedEventArgs : WebRequestEventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="bytesWritten"></param>
        /// <param name="totalBytesToWrite"></param>
        public WebRequestWritingProgressChangedEventArgs(WebRequestContext requestContext, long bytesWritten, long totalBytesToWrite)
            : base(requestContext)
        {
            _bytesWritten       = bytesWritten;
            _totalBytesToWrite  = totalBytesToWrite;
        }
        #endregion

        #region Fields
        readonly long   _bytesWritten       = 0L;
        readonly long   _totalBytesToWrite  = 0L;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public long BytesWritten
        {
            get
            {
                return _bytesWritten;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long TotalBytesToWrite
        {
            get
            {
                return _totalBytesToWrite;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal ProgressPercentage
        {
            get
            {
                return 0.0m;
            }
        }
        #endregion
    }
}
