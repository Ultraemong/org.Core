using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using org.Core.Collections;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class WebFileDownloader : WebRequestHandler, INetworkFileDownloadable
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileManager"></param>
        public WebFileDownloader(INetworkFileService fileManager)
            : base()
        {
            _fileService = fileManager;
        }
        #endregion

        #region Events
        public event DownloadFailedEventHandler             DownloadFailed              = null;
        public event DownloadCompletedEventHandler          DownloadCompleted           = null;
        public event DownloadProgressChangedEventHandler    DownloadProgressChanged     = null;
        #endregion

        #region Fields
        const int                                           BUFFER_SIZE                 = 1024 * 2;

        readonly INetworkFileService                        _fileService                = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public INetworkFileService FileService
        {
            get 
            {
                return _fileService;
            }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected abstract IExternalResult GetRequestResult(WebRequestContext requestContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected virtual Uri CreateUniformResourceIdentifier(string path)
        {
            return new Uri(string.Format("{0}/{1}", _fileService.Host, path));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public IExternalResult CreateDirectory(string directoryPath)
        {
            var _uri        = CreateUniformResourceIdentifier(directoryPath);
            var _request    = CreateRequest(_uri, WebRequestMethod.MakeDirectory);

            using (var _context = CreateRequestContext(_uri, WebRequestMethod.MakeDirectory))
            {
                HandleRequestResponse(_context);

                return GetRequestResult(_context);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public bool ExistDirectory(string directoryPath)
        {
            var _uri = CreateUniformResourceIdentifier(directoryPath);

            using (var _context = CreateRequestContext(_uri, WebRequestMethod.ExistDirectory))
            {
                HandleRequestResponse(_context);

                var _result = GetRequestResult(_context);

                return (_result.ErrorCode.Equals(0));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ExistFile(string filePath)
        {
            var _uri = CreateUniformResourceIdentifier(filePath);
            
            using (var _context = CreateRequestContext(_uri, WebRequestMethod.ExistFile))
            {
                HandleRequestResponse(_context);

                var _result = GetRequestResult(_context);

                return (_result.ErrorCode.Equals(0));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public IExternalResult DownloadFile(string source, string destination)
        {
            return DownloadFile(source, new FileStream(destination, FileMode.Create, FileAccess.Write));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public IExternalResult DownloadFile(string source, FileStream destination)
        {
            var _uri    = CreateUniformResourceIdentifier(source);
            var _length = GetContentLength(_uri);
            
            using (var _context = CreateRequestContext(_uri, WebRequestMethod.DownloadFile, BUFFER_SIZE, destination, _length))
            {
                HandleRequestResponse(_context);
                HandleRequestResponseStream(_context);

                return GetRequestResult(_context);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="userToken"></param>
        public void DownloadFileAsync(string source, string destination)
        {
            DownloadFileAsync(source, new FileStream(destination, FileMode.Create, FileAccess.Write));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="userToken"></param>
        public void DownloadFileAsync(string source, FileStream destination)
        {
            var _uri        = CreateUniformResourceIdentifier(source);
            var _length     = GetContentLength(_uri);
            var _context    = CreateRequestContext(_uri, WebRequestMethod.DownloadFile, BUFFER_SIZE, destination, _length, true);

            HandleRequestResponse(_context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGettingCompleted(WebRequestEventArgs e)
        {
            base.OnGettingCompleted(e);

            if (e.RequestContext.IsAsync)
            {
                if (e.RequestContext.RequestMethod.Equals(WebRequestMethod.DownloadFile))
                {
                    HandleRequestResponseStream(e.RequestContext);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnWritingProgressChanged(WebRequestWritingProgressChangedEventArgs e)
        {
            base.OnWritingProgressChanged(e);

            if (e.RequestContext.IsAsync)
            {
                if (e.RequestContext.RequestMethod.Equals(WebRequestMethod.DownloadFile))
                {
                    if (null != DownloadProgressChanged)
                    {
                        DownloadProgressChanged(this, e);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnReadingCompleted(WebRequestEventArgs e)
        {
            base.OnReadingCompleted(e);

            if (e.RequestContext.IsAsync)
            {
                if (e.RequestContext.RequestMethod.Equals(WebRequestMethod.DownloadFile))
                {
                    if (null != DownloadCompleted)
                    {
                        DownloadCompleted(this, e);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFaulted(WebRequestEventArgs e)
        {
            base.OnFaulted(e);

            if (null != DownloadFailed)
            {
                DownloadFailed(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        long GetContentLength(Uri uri)
        {
            using (var _context = CreateRequestContext(uri, WebRequestMethod.GetFileSize))
            {
                HandleRequestResponse(_context);

                if (null != _context.Response)
                    return _context.Response.ContentLength;
            }

            return -1L;
        }
        #endregion
    }
}
