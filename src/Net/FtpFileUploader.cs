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
    public class FtpFileUploader : FtpFileUtil, INetworkFileUploadable
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileManager"></param>
        public FtpFileUploader(INetworkFileService fileManager)
            : base(fileManager)
        {   
        }
        #endregion

        #region Events
        public event UploadFailedEventHandler               UploadFailed            = null;
        public event UploadCompletedEventHandler            UploadCompleted         = null;
        public event UploadProgressChangedEventHandler      UploadProgressChanged   = null;
        #endregion

        #region Fields
        const int                                           BUFFER_SIZE             = 1024 * 2;
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
                return UploadFile(_source, destination);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public IExternalResult UploadFile(FileStream source, string destination)
        {
            return UploadFile(source as Stream, destination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public IExternalResult UploadFile(Stream source, string destination)
        {
            var _requestMethod  = WebRequestMethod.UploadFile;
            var _directoryName  = Path.GetDirectoryName(destination);

            if (!string.IsNullOrEmpty(_directoryName))
            {
                CreateDirectory(_directoryName);
            }

            var _uri        = CreateUniformResourceIdentifier(WebRequestHelpers.ReplacePathDelimiterToUriDelimiter(destination));
            var _request    = CreateRequest(_uri, _requestMethod);

            using (var _destination = _request.GetRequestStream())
            {
                var _buffer         = new byte[source.Length];
                var _bytesToRead    = (int)source.Length;
                var _bytesRead      = 0;

                while (0 < _bytesToRead)
                {
                    var _read = 0;

                    if (0 < (_read = source.Read(_buffer, _bytesRead, _bytesToRead)))
                    {
                        _bytesRead   += _read;
                        _bytesToRead -= _read;

                        continue;
                    }

                    break;
                }

                _destination.Write(_buffer, 0, _buffer.Length);
            }

            return GetRequestResult(_request.GetResponse(), _requestMethod);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="userToken"></param>
        public void UploadFileAsync(string source, string destination, object userToken = null)
        {
            UploadFileAsync(new FileInfo(source), destination, userToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="userToken"></param>
        public void UploadFileAsync(FileInfo source, string destination, object userToken = null)
        {
            UploadFileAsync(source.OpenRead(), destination, userToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public void UploadFileAsync(FileStream source, string destination, object userToken = null)
        {
            UploadFileAsync(source as Stream, destination, userToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public void UploadFileAsync(Stream source, string destination, object userToken = null)
        {
            var _requestMethod  = WebRequestMethod.UploadFile;
            var _directoryName  = Path.GetDirectoryName(destination);

            if (!string.IsNullOrEmpty(_directoryName))
            {
                CreateDirectory(_directoryName);
            }

            var _uri            = CreateUniformResourceIdentifier(WebRequestHelpers.ReplacePathDelimiterToUriDelimiter(destination));
            var _request        = CreateRequest(_uri, _requestMethod);
            var _state          = new FileUploadAsyncState(_request, BUFFER_SIZE, source, null, source.Length, userToken);

            _request.BeginGetRequestStream(new AsyncCallback(_BeginGetRequestStream), _state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="state"></param>
        void RaiseUploadFaultedEvent(Exception exception, FileUploadAsyncState state)
        {
            var _argument = new UploadFailedEventArgs(exception, state.UserToken);

            if (null != UploadFailed)
                UploadFailed(this, _argument);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void _BeginGetRequestStream(IAsyncResult asyncResult)
        {
            var _state = asyncResult.AsyncState as FileUploadAsyncState;

            try
            {
                var _destination    = _state.Request.EndGetRequestStream(asyncResult);

                var _newState       = new FileUploadAsyncState(_state.Request, _state.BufferSize, _state.Source, _destination, _state.ContentLength, _state.UserToken);
                var _callback       = new AsyncCallback(_BeginReadSourceCallback);

                _newState.Source.BeginRead(_newState.ByteBuffer, 0, _newState.ByteBuffer.Length, _callback, _newState);
            }
            catch (Exception ex)
            {
                RaiseUploadFaultedEvent(ex, _state);

                _state.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void _BeginReadSourceCallback(IAsyncResult asyncResult)
        {
            var _state = asyncResult.AsyncState as FileUploadAsyncState;
            
            try
            { 
                _state.BytesUploaded = _state.Source.EndRead(asyncResult);

                if (0 < _state.BytesUploaded)
                {
                    _state.Destination.BeginWrite(_state.ByteBuffer, 0, _state.BytesUploaded, new AsyncCallback(_BeginWriteDestinationCallback), _state);
                    return;
                }

                _state.Destination.Flush();
                _state.Destination.Close();
                _state.Destination.Dispose();

                _state.Request.BeginGetResponse(new AsyncCallback(_BeginGetResponseCallback), _state);
            }
            catch(Exception ex)
            {
                RaiseUploadFaultedEvent(ex, _state);

                _state.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void _BeginGetResponseCallback(IAsyncResult asyncResult)
        {
            var _state = asyncResult.AsyncState as FileUploadAsyncState;

            try
            {
                var _response       = _state.Request.EndGetResponse(asyncResult);
                var _requestResult  = GetRequestResult(_response, WebRequestMethod.UploadFile);

                if (_requestResult.ErrorCode.Equals(0))
                {
                    if (null != UploadCompleted)
                    {
                        var _argumemnts = new UploadCompletedEventArgs(_state.UserToken);

                        UploadCompleted(this, _argumemnts);
                    }

                    _state.Dispose();
                }
            }
            catch (Exception ex)
            {
                RaiseUploadFaultedEvent(ex, _state);

                _state.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void _BeginWriteDestinationCallback(IAsyncResult asyncResult)
        {
            var _state = asyncResult.AsyncState as FileUploadAsyncState;

            try
            { 
                _state.Destination.EndWrite(asyncResult);

                if (null != UploadProgressChanged)
                {
                    var _argument = new UploadProgressChangedEventArgs(_state.TotalBytesUploaded, _state.ContentLength, _state.UserToken);

                    UploadProgressChanged(this, _argument);
                }

                var _callback = new AsyncCallback(_BeginReadSourceCallback);

                _state.Source.BeginRead(_state.ByteBuffer, 0, _state.ByteBuffer.Length, _callback, _state);
            }
            catch (Exception ex)
            {
                RaiseUploadFaultedEvent(ex, _state);

                _state.Dispose();
            }
        }
        #endregion

        #region Internal Classes
        /// <summary>
        /// 
        /// </summary>
        class FileUploadAsyncState : IDisposable
        {
            #region Constructors
            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="bufferSize"></param>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            public FileUploadAsyncState(WebRequest request, int bufferSize, Stream source, Stream destination, long contentLength)
            {
                _request        = request;
                _bufferSize     = bufferSize;

                _source         = source;
                _destination    = destination;

                _contentLength  = contentLength;

                _byteBuffer     = new byte[bufferSize];
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="bufferSize"></param>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            /// <param name="contentLength"></param>
            public FileUploadAsyncState(WebRequest request, int bufferSize, Stream source, Stream destination, long contentLength, object userToken)
                : this(request, bufferSize, source, destination, contentLength)
            {
                _userToken = userToken;
            }
            #endregion

            #region Fields
            readonly WebRequest         _request                = null;
            readonly int                _bufferSize             = 0;

            readonly Stream             _source                 = null;
            readonly Stream             _destination            = null;

            readonly long               _contentLength          = 0L;
            readonly object             _userToken              = null;

            byte[]                      _byteBuffer             = null;

            int                         _bytesUploaded          = 0;
            long                        _totalBytesUploaded     = 0L;
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
            public int BytesUploaded
            {
                get
                {
                    return _bytesUploaded;
                }

                set
                {
                    _bytesUploaded          = value;
                    _totalBytesUploaded    += value;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public long TotalBytesUploaded
            {
                get
                {
                    return _totalBytesUploaded;
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

            #region Methods
            /// <summary>
            /// 
            /// </summary>
            public void Dispose()
            {
                _byteBuffer             = new byte[0];
                _bytesUploaded          = 0;
                _totalBytesUploaded     = 0L;

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
