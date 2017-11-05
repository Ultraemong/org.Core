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
    public class UploadProgressChangedEventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytesUploaded"></param>
        /// <param name="totalBytesToUpload"></param>
        /// <param name="userToken"></param>
        public UploadProgressChangedEventArgs(long bytesUploaded, long totalBytesToUpload, object userToken)
            : base()
        {
            _userToken              = userToken;
            _bytesUploaded          = bytesUploaded;
            _totalBytesToUpload     = totalBytesToUpload;
        }
        #endregion

        #region Fields
        readonly object     _userToken              = null;
        readonly long       _bytesUploaded          = 0L;
        readonly long       _totalBytesToUpload     = 0L;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public long BytesUploaded
        {
            get
            {
                return _bytesUploaded;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long TotalBytesToUpload
        {
            get
            {
                return _totalBytesToUpload;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal ProgressPercentage
        {
            get
            {
                if (!_bytesUploaded.Equals(_totalBytesToUpload))
                    return Math.Round(((decimal)_bytesUploaded / (decimal)_totalBytesToUpload) * 100, 2);

                return 100.00m;
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
