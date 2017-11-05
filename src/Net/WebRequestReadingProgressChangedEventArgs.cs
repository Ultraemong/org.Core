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
    public class WebRequestReadingProgressChangedEventArgs : WebRequestEventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="bytesRead"></param>
        /// <param name="totalBytesToRead"></param>
        public WebRequestReadingProgressChangedEventArgs(WebRequestContext requestContext, long bytesRead, long totalBytesToRead)
            : base(requestContext)
        {
            _bytesRead          = bytesRead;
            _totalBytesToRead   = totalBytesToRead;
        }
        #endregion

        #region Fields
        readonly long   _bytesRead              = 0L;
        readonly long   _totalBytesToRead       = 0L;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public long BytesRead
        {
            get
            {
                return _bytesRead;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long TotalBytesToRead
        {
            get
            {
                return _totalBytesToRead;
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
