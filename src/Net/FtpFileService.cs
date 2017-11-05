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
    public class FtpFileService : INetworkFileService
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="credential"></param>
        public FtpFileService(string host, NetworkCredential credential)
        {
            _host           = host;
            _credential     = credential;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="credential"></param>
        public FtpFileService(string host, int port, NetworkCredential credential)
        {
            _host           = host;
            _port           = port;
            _credential     = credential;
        }
        #endregion

        #region Fields
        readonly string             _host           = null;
        readonly int                _port           = 0;
        readonly NetworkCredential  _credential     = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Host
        {
            get 
            {
                return _host;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Port
        {
            get 
            {
                return _port;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NetworkCredential Credential
        {
            get 
            {
                return _credential;
            }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public INetworkFileUploadable CreateFileUploader()
        {
            return new FtpFileUploader(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public INetworkFileDownloadable CreateFileDownloader()
        {
            return new FtpFileDownloader(this);
        } 
        #endregion
    }
}
