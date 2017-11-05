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
    /// <param name="e"></param>
    public delegate void ExternalUploadProgressChangedEventHandler(object sender, ExternalUploadProgressChangedEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public sealed class ExternalUploadProgressChangedEventArgs : ExternalActionEventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ExternalUploadProgressChangedEventArgs(ExternalActionContext context)
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
        public long BytesUploaded
        {
            get
            {
                return Context.BytesRead;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long TotalBytesToUpload
        {
            get
            {
                return Context.ContentLength;
            }
        }
        #endregion
    }
}
