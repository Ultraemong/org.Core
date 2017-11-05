using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace org.Core.Net.Ftp
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FtpManager : IExternalActionManager
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="credential"></param>
        public FtpManager(string host, NetworkCredential credential)
        {
            _host           = host;
            _credential     = credential;
        } 
        #endregion

        #region Events
        public event ExternalUploadFailedEventHandler               UploadFailed                = null;
        public event ExternalUploadCompletedEventHandler            UploadCompleted             = null;
        public event ExternalUploadProgressChangedEventHandler      UploadProgressChanged       = null;        
        public event ExternalDownloadFailedEventHandler             DownloadFailed              = null;
        public event ExternalDownloadCompletedEventHandler          DownloadCompleted           = null;
        public event ExternalDownloadProgressChangedEventHandler    DownloadProgressChanged     = null;
        #endregion

        #region Fields
        const int                                                   BUFFER_SIZE                 = 1024 * 2;
        EventWaitHandle                                             _waitHandle                 = new EventWaitHandle(false, EventResetMode.ManualReset);

        readonly string                                             _host                       = null;
        readonly NetworkCredential                                  _credential                 = null;
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
        /// <param name="path"></param>
        /// <returns></returns>
        public IExternalResult CreateDirectory(string path)
        {
            var _resource   = new Uri(string.Format("{0}/{1}", Host, path));
            var _request    = CreateRequest(_resource, WebRequestMethods.Ftp.MakeDirectory);

            using (var _context = new ExternalActionContext(_request, false, BUFFER_SIZE, false))
            {
                _context.BeginGetResponse(x => AsyncGetResponseCallback(x));

                _waitHandle.WaitOne();
                _waitHandle.Reset();

                return _context.Result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool ExistDirectory(string path)
        {
            var _resource   = new Uri(string.Format("{0}/{1}", Host, path));
            var _request    = CreateRequest(_resource, WebRequestMethods.Ftp.ListDirectory);

            using (var _context = new ExternalActionContext(_request, false, BUFFER_SIZE, false))
            {
                _context.BeginGetResponse(x => AsyncGetResponseCallback(x));

                _waitHandle.WaitOne();
                _waitHandle.Reset();

                return !(_context.Result as FtpResult).ErrorCode.Equals(FtpStatusCode.ActionNotTakenFileUnavailable);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool ExistFile(string path)
        {
            var _resource   = new Uri(string.Format("{0}/{1}", Host, path));
            var _request    = CreateRequest(_resource, WebRequestMethods.Ftp.GetFileSize);

            using (var _context = new ExternalActionContext(_request, false, BUFFER_SIZE, false))
            {
                _context.BeginGetResponse(x => AsyncGetResponseCallback(x));

                _waitHandle.WaitOne();
                _waitHandle.Reset();

                return (_context.Result as FtpResult).ErrorCode.Equals(FtpStatusCode.FileStatus);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public IExternalResult UploadFile(string source, string destination)
        {
            return UploadFile(new FileInfo(source), destination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public IExternalResult UploadFile(FileInfo source, string destination)
        {
            using (var _source = source.OpenRead())
            {
                if (_source.CanRead)
                {
                    var _resource   = new Uri(string.Format("{0}/{1}", Host, destination));
                    var _request    = CreateRequest(_resource, WebRequestMethods.Ftp.UploadFile);

                    using (var _context = new ExternalActionContext(_request, false, BUFFER_SIZE, false, _source))
                    {
                        _context.BeginGetRequestStream(x => AsyncGetRequestStreamCallback(x));

                        _waitHandle.WaitOne();
                        _waitHandle.Reset();

                        return _context.Result;
                    }
                }

                _source.Dispose();
            }

            return new FtpResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="userToken"></param>
        public void UploadFileAsync(string source, string destination, object userToken)
        {
            UploadFileAsync(new FileInfo(source), destination, userToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="userToken"></param>
        public void UploadFileAsync(FileInfo source, string destination, object userToken)
        {
            var _fileStream = source.OpenRead();

            if (_fileStream.CanRead)
            {
                var _resource   = new Uri(string.Format("{0}/{1}", Host, destination));
                var _request    = CreateRequest(_resource, WebRequestMethods.Ftp.UploadFile);
                var _context    = new ExternalActionContext(_request, true, BUFFER_SIZE, false, _fileStream, userToken);
                
                _context.BeginGetRequestStream(x => AsyncGetRequestStreamCallback(x));
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
            using (var _destination = new FileStream(destination, FileMode.Create))
            {
                return DownloadFile(source, _destination) as FtpResult;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public IExternalResult DownloadFile(string source, FileStream destination)
        {
            if (destination.CanWrite)
            {
                var _resource   = new Uri(string.Format("{0}/{1}", Host, source));
                var _length     = GetContentLength(_resource);
                var _request    = CreateRequest(_resource, WebRequestMethods.Ftp.DownloadFile);

                using (var _context = new ExternalActionContext(_request, false, BUFFER_SIZE, destination, null, _length))
                {
                    _context.BeginGetResponse(x => AsyncGetResponseCallback(x));

                    _waitHandle.WaitOne();
                    _waitHandle.Reset();

                    return _context.Result;
                }
            }

            return new FtpResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public void DownloadFileAsync(string source, string destination, object userToken)
        {
            DownloadFileAsync(source, new FileStream(destination, FileMode.Create), userToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="userToken"></param>
        public void DownloadFileAsync(string source, FileStream destination, object userToken)
        {
            if (destination.CanWrite)
            {
                var _resource   = new Uri(string.Format("{0}/{1}", Host, source));
                var _length     = GetContentLength(_resource);
                var _request    = CreateRequest(_resource, WebRequestMethods.Ftp.DownloadFile);
                var _context    = new ExternalActionContext(_request, true, BUFFER_SIZE, destination, userToken, _length);

                _context.BeginGetResponse(x => AsyncGetResponseCallback(x));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _waitHandle.Set();
            _waitHandle.Dispose();
            _waitHandle = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        long GetContentLength(Uri source)
        {
            var _request = CreateRequest(source, WebRequestMethods.Ftp.GetFileSize);

            using (var _context = new ExternalActionContext(_request, false, BUFFER_SIZE, false))
            {
                _context.BeginGetResponse(x => AsyncGetResponseCallback(x));

                _waitHandle.WaitOne();
                _waitHandle.Reset();

                return _context.ContentLength;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void RaiseActionCompletedEvent(ExternalActionContext context)
        {
            if (context.IsAsync)
            {
                if (WebRequestMethods.Ftp.UploadFile.Equals(context.Method))
                {
                    if (null != UploadCompleted)
                    {
                        var _args = new ExternalActionEventArgs(context);

                        UploadCompleted(this, _args);
                    }
                }
                else if (WebRequestMethods.Ftp.DownloadFile.Equals(context.Method))
                {
                    if (null != DownloadCompleted)
                    {
                        var _args = new ExternalActionEventArgs(context);

                        DownloadCompleted(this, _args);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void RaiseActionFailedEvent(ExternalActionContext context)
        {
            if (context.IsAsync)
            {
                if (WebRequestMethods.Ftp.UploadFile.Equals(context.Method))
                {
                    if (null != UploadFailed)
                    {
                        var _args = new ExternalActionEventArgs(context);

                        UploadFailed(this, _args);
                    }
                }
                else if (WebRequestMethods.Ftp.DownloadFile.Equals(context.Method))
                {
                    if (null != DownloadFailed)
                    {
                        var _args = new ExternalActionEventArgs(context);

                        DownloadFailed(this, _args);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void RaiseActionProgressChangedEvent(ExternalActionContext context)
        {
            if (context.IsAsync)
            {
                if (WebRequestMethods.Ftp.UploadFile.Equals(context.Method))
                {
                    if (null != UploadProgressChanged)
                    {
                        var _args = new ExternalUploadProgressChangedEventArgs(context);

                        UploadProgressChanged(this, _args);
                    }
                }
                else if (WebRequestMethods.Ftp.DownloadFile.Equals(context.Method))
                {
                    if (null != DownloadProgressChanged)
                    {
                        var _args = new ExternalDownloadProgressChangedEventArgs(context);

                        DownloadProgressChanged(this, _args);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void AsyncGetResponseCallback(IAsyncResult asyncResult)
        {
            var _context = asyncResult.AsyncState as ExternalActionContext;

            try
            {
                var _response = _context.EndGetResponse(asyncResult) as FtpWebResponse;

                if (_context.ReadToEnd)
                {
                    _context.BeginReadResponseStream(0, BUFFER_SIZE, x => AsyncReadResponseStreamCallback(x));
                    return;
                }

                _context.Result = FtpResult.Parse(_response);
                _context.Dispose();

                RaiseActionCompletedEvent(_context);

                _waitHandle.Set();
            }
            catch (Exception ex)
            {
                HandleThrownException(_context, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void AsyncReadResponseStreamCallback(IAsyncResult asyncResult)
        {
            var _context = asyncResult.AsyncState as ExternalActionContext;

            try
            {
                int _read = _context.EndReadResponseStream(asyncResult);

                if (0 < _read)
                {
                    if (null != _context.DestinationStream)
                    {
                        _context.BeginWriteDestinationSteam(0, _read, x => AsyncWriteDestinationStreamCallback(x));
                        return;
                    }

                    _context.BeginReadResponseStream(0, BUFFER_SIZE, x => AsyncReadResponseStreamCallback(x));
                    return;
                }                

                _context.Result = FtpResult.Parse(_context.Response as FtpWebResponse);
                _context.Dispose();

                RaiseActionCompletedEvent(_context);

                _waitHandle.Set();
            }
            catch (Exception ex)
            {
                HandleThrownException(_context, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void AsyncWriteDestinationStreamCallback(IAsyncResult asyncResult)
        {
            var _context = asyncResult.AsyncState as ExternalActionContext;

            try
            {
                _context.EndWriteDestinationStream(asyncResult);

                RaiseActionProgressChangedEvent(_context);

                _context.BeginReadResponseStream(0, BUFFER_SIZE, x => AsyncReadResponseStreamCallback(x));
            }
            catch (Exception ex)
            {
                HandleThrownException(_context, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void AsyncGetRequestStreamCallback(IAsyncResult asyncResult)
        {
            var _context = asyncResult.AsyncState as ExternalActionContext;

            try
            {
                _context.EndGetRequestStream(asyncResult);
                _context.BeginReadSourceStream(x => AsyncReadSourceStreamCallback(x));
            }
            catch (Exception ex)
            {
                HandleThrownException(_context, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void AsyncReadSourceStreamCallback(IAsyncResult asyncResult)
        {
            var _context = asyncResult.AsyncState as ExternalActionContext;

            try
            {
                int _read = _context.EndReadSourceStream(asyncResult);

                if (0 < _read)
                {
                    _context.BeginWriteRequestStream(0, _read, x => AsyncWriteRequestStreamCallback(x));
                    return;
                }

                _context.FlushBuffer();
                _context.BeginGetResponse(x => AsyncGetResponseCallback(x));
            }
            catch (Exception ex)
            {
                HandleThrownException(_context, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void AsyncWriteRequestStreamCallback(IAsyncResult asyncResult)
        {
            var _context = asyncResult.AsyncState as ExternalActionContext;

            try
            {
                _context.EndWriteRequestStream(asyncResult);

                RaiseActionProgressChangedEvent(_context);

                _context.BeginReadSourceStream(x => AsyncReadSourceStreamCallback(x));
            }
            catch (Exception ex)
            {
                HandleThrownException(_context, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        void HandleThrownException(ExternalActionContext context, Exception ex)
        {
            context.Result = FtpResult.Parse(ex);

            if (null != context)
                context.Dispose();

            if (null != _waitHandle)
                _waitHandle.Set();

            RaiseActionFailedEvent(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        FtpWebRequest CreateRequest(Uri uri, string method)
        {
            var _request            = FtpWebRequest.Create(uri) as FtpWebRequest;

            _request.Method          = method;
            _request.KeepAlive       = true;
            _request.UsePassive      = true;
            _request.Credentials     = Credential;

            return _request;
        }
        #endregion
    }
}
