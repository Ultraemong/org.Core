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
    /// <param name="sender"></param>
    public delegate void ExternalDownloadProgressChangedEventHandler(object sender, ExternalDownloadProgressChangedEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public sealed class ExternalDownloadProgressChangedEventArgs : ExternalActionEventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ExternalDownloadProgressChangedEventArgs(ExternalActionContext context)
            : base(context)
        {
        }
        #endregion

        #region Fields
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public long BytesDownloaded
        {
            get
            {
                return Context.BytesRead;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long TotalBytesToDownload
        {
            get
            {
                return Context.ContentLength;
            }
        }
        #endregion
    }
}
