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
    public class DownloadProgressChangedEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytesDownloaded"></param>
        /// <param name="totalBytesToDownload"></param>
        public DownloadProgressChangedEventArgs(long bytesDownloaded, long totalBytesToDownload)
            : base()
        {
            _bytesDownloaded        = bytesDownloaded;
            _totalBytesToDownload   = totalBytesToDownload;
        }
        #endregion

        #region Fields
        readonly long _bytesDownloaded      = 0L;
        readonly long _totalBytesToDownload = 0L;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public long BytesDownloaded
        {
            get
            {
                return _bytesDownloaded;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long TotalBytesToDownload
        {
            get
            {
                return _totalBytesToDownload;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal ProgressPercentage
        {
            get
            {
                if (!_bytesDownloaded.Equals(_totalBytesToDownload))
                    return Math.Round(((decimal)_bytesDownloaded / (decimal)_totalBytesToDownload) * 100, 2);

                return 100.00m;
            }
        }
        #endregion
    }
}
