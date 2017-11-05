using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FtpFileDownloader : FtpFileUtil, INetworkFileDownloadable
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileService"></param>
        public FtpFileDownloader(INetworkFileService fileService)
            : base(fileService)
        {   
        }
        #endregion

        #region Events
        public event DownloadFailedEventHandler             DownloadFailed              = null;
        public event DownloadCompletedEventHandler          DownloadCompleted           = null;
        public event DownloadProgressChangedEventHandler    DownloadProgressChanged     = null;
        #endregion

        #region Fields
        const int                                           BUFFER_SIZE                 = 1024 * 2;
        #endregion

        #region Properties
        
        #endregion

        #region Methods
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
            var _requestMethod  = WebRequestMethod.DownloadFile;
            var _resourceName   = WebRequestHelpers.ReplacePathDelimiterToUriDelimiter(source);

            var _uri            = CreateUniformResourceIdentifier(_resourceName);
            var _request        = CreateRequest(_uri, _requestMethod);

            var _length         = GetContentLength(_uri);
            var _response       = _request.GetResponse();

            var _source         = _response.GetResponseStream();

            using (var _state = new FileDownloadState(_request, BUFFER_SIZE, _source, destination, _length))
            {
                while (0 < (_state.BytesDownloaded = _state.Source.Read(_state.ByteBuffer, 0, _state.ByteBuffer.Length)))
                {
                    _state.Destination.Write(_state.ByteBuffer, 0, _state.BytesDownloaded);
                }

                return GetRequestResult(_response, _requestMethod);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public void DownloadFileAsync(string source, string destination)
        {
            DownloadFileAsync(source, new FileStream(destination, FileMode.Create, FileAccess.Write));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public void DownloadFileAsync(string source, FileStream destination)
        {
            var _requestMethod  = WebRequestMethod.DownloadFile;
            var _resourceName   = WebRequestHelpers.ReplacePathDelimiterToUriDelimiter(source);

            var _uri            = CreateUniformResourceIdentifier(_resourceName);
            var _request        = CreateRequest(_uri, _requestMethod);

            var _length         = GetContentLength(_uri);
            var _state          = new FileDownloadState(_request, BUFFER_SIZE, null, destination, _length);

            _request.BeginGetResponse(new AsyncCallback(_BeginGetResponseCallback), _state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        long GetContentLength(Uri uri)
        {
            var _request    = CreateRequest(uri, WebRequestMethod.GetFileSize);
            var _response   = _request.GetResponse();

            if (null != _response)
                return _response.ContentLength;

            return 0L;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        void RaiseDownloadFaultedEvent(Exception exception)
        {
            var _argument = new DownloadFailedEventArgs(exception);

            if (null != DownloadFailed)
                DownloadFailed(this, _argument);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void _BeginGetResponseCallback(IAsyncResult asyncResult)
        {
            var _state = asyncResult.AsyncState as FileDownloadState;

            try
            { 
                var _response   = _state.Request.EndGetResponse(asyncResult);
                var _source     = _response.GetResponseStream();

                var _newState   = new FileDownloadState(_state.Request, _state.BufferSize, _source, _state.Destination, _state.ContentLength);
                var _callback   = new AsyncCallback(_BeginReadSourceCallback);

                _newState.Source.BeginRead(_newState.ByteBuffer, 0, _newState.ByteBuffer.Length, _callback, _newState);
            }
            catch(Exception ex)
            {
                _state.Dispose();

                RaiseDownloadFaultedEvent(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void _BeginReadSourceCallback(IAsyncResult asyncResult)
        {
            var _state = asyncResult.AsyncState as FileDownloadState;
            
            try
            { 
                _state.BytesDownloaded = _state.Source.EndRead(asyncResult);

                if (0 < _state.BytesDownloaded)
                {
                    _state.Destination.BeginWrite(_state.ByteBuffer, 0, _state.BytesDownloaded, new AsyncCallback(_BeginWriteDestinationCallback), _state);
                    return;
                }

                if (null != DownloadCompleted)
                    DownloadCompleted(this, EventArgs.Empty);

                _state.Dispose();
            }
            catch(Exception ex)
            {
                _state.Dispose();

                RaiseDownloadFaultedEvent(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void _BeginWriteDestinationCallback(IAsyncResult asyncResult)
        {
            var _state = asyncResult.AsyncState as FileDownloadState;

            try
            { 
                _state.Destination.EndWrite(asyncResult);

                if (null != DownloadProgressChanged)
                {
                    var _argument = new DownloadProgressChangedEventArgs(_state.TotalBytesDownloaded, _state.ContentLength);

                    DownloadProgressChanged(this, _argument);
                }

                var _callback = new AsyncCallback(_BeginReadSourceCallback);

                _state.Source.BeginRead(_state.ByteBuffer, 0, _state.ByteBuffer.Length, _callback, _state);
            }
            catch (Exception ex)
            {
                _state.Dispose();

                RaiseDownloadFaultedEvent(ex);
            }
        }
        #endregion

        #region Internal Classes
        /// <summary>
        /// 
        /// </summary>
        class FileDownloadState : IDisposable
        {
            #region Constructors
            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="bufferSize"></param>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            public FileDownloadState(WebRequest request, int bufferSize, Stream source, Stream destination, long contentLength)
            {
                _request        = request;
                _bufferSize     = bufferSize;

                _source         = source;
                _destination    = destination;

                _contentLength  = contentLength;

                _byteBuffer     = new byte[bufferSize];
            }
            #endregion

            #region Fields
            readonly WebRequest         _request                = null;
            readonly int                _bufferSize             = 0;

            readonly Stream             _source                 = null;
            readonly Stream             _destination            = null;

            readonly long               _contentLength          = 0L;

            byte[]                      _byteBuffer             = null;

            int                         _bytesDownloaded        = 0;
            long                        _totalBytesDownloaded   = 0L;
            #endregion

            #region Properties
            /// <summary>
            /// 
            /// </summary>
            public WebRequest Request
            {
                get
                {
                    return _request;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public int BufferSize
            {
                get
                {
                    return _bufferSize;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public Stream Source
            {
                get
                {
                    return _source;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public Stream Destination
            {
                get
                {
                    return _destination;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public long ContentLength
            {
                get
                {
                    return _contentLength;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public byte[] ByteBuffer
            {
                get
                {
                    return _byteBuffer;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public int BytesDownloaded
            {
                get
                {
                    return _bytesDownloaded;
                }

                set
                {
                    _bytesDownloaded        = value;
                    _totalBytesDownloaded   += value;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public long TotalBytesDownloaded
            {
                get
                {
                    return _totalBytesDownloaded;
                }
            }
            #endregion

            #region Methods
            /// <summary>
            /// 
            /// </summary>
            public void Dispose()
            {
                _byteBuffer             = new byte[0];

                _bytesDownloaded        = 0;
                _totalBytesDownloaded   = 0L;

                if (null != _source)
                {
                    _source.Flush();
                    _source.Close();
                    _source.Dispose();
                }

                if (null != _destination)
                {
                    _destination.Flush();
                    _destination.Close();
                    _destination.Dispose();
                }

                if (null != _request)
                {
                    _request.Abort();
                }
            } 
            #endregion
        } 
        #endregion
    }
}
