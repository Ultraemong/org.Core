using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public interface INetworkFileService
    {
        /// <summary>
        /// 
        /// </summary>
        string Host { get; }

        /// <summary>
        /// 
        /// </summary>
        int Port { get; }

        /// <summary>
        /// 
        /// </summary>
        NetworkCredential Credential { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        INetworkFileUploadable CreateFileUploader();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        INetworkFileDownloadable CreateFileDownloader();
    }
}
