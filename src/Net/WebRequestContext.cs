using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using org.Core.Collections;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class WebRequestContext : IDisposable
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestMethod"></param>
        /// <param name="isAsync"></param>
        public WebRequestContext(WebRequest request, WebRequestMethod requestMethod, bool isAsync)
        {
            _isAsync            = isAsync;
            _request            = request;
            _requestMethod      = requestMethod;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestMethod"></param>
        /// <param name="bufferSize"></param>
        /// <param name="sourceStream"></param>
        /// <param name="isAsync"></param>
        public WebRequestContext(WebRequest request, WebRequestMethod requestMethod, int bufferSize, FileStream sourceStream, bool isAsync)
            : this(request, requestMethod, isAsync)
        {
            _bufferSize         = bufferSize;
            _sourceStream       = sourceStream;
            _contentLength      = sourceStream.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestMethod"></param>
        /// <param name="bufferSize"></param>
        /// <param name="sourceStream"></param>
        /// <param name="contentLength"></param>
        /// <param name="isAsync"></param>
        public WebRequestContext(WebRequest request, WebRequestMethod requestMethod, int bufferSize, FileStream sourceStream, long contentLength, bool isAsync)
            : this(request, requestMethod, bufferSize, sourceStream, isAsync)
        {
            _contentLength      = contentLength;
        }
        #endregion

        #region Fields
        readonly bool                                   _isAsync                = false;
        readonly int                                    _bufferSize             = 1024 * 2;
        readonly long                                   _contentLength          = 0;

        readonly WebRequest                             _request                = null;
        readonly WebRequestMethod                       _requestMethod          = WebRequestMethod.Unknown;
        readonly ListCollection<WebRequestException>    _innerErrorRepository   = new ListCollection<WebRequestException>(true);

        volatile WebRequestState                        _requestState           = null;

        WebResponse                                     _response               = null;

        FileStream                                      _sourceStream           = null;

        Stream                                          _requestStream          = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool IsAsync
        {
            get
            {
                return _isAsync;
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
        public WebRequestMethod RequestMethod
        {
            get
            {
                return _requestMethod;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInError
        {
            get
            {
                return (0 < _innerErrorRepository.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public WebRequestExceptionCollection Errors
        {
            get
            {
                return new WebRequestExceptionCollection(_innerErrorRepository);
            }
        }

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
        public Stream RequestStream
        {
            get
            {
                return _requestStream;
            }

            set
            {
                _requestStream = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public WebRequestState RequestState
        {
            get
            {
                return _requestState;
            }

            internal set
            {
                if (null != value)
                {
                    if (null != _requestState)
                    {
                        _requestState.Dispose();
                    }

                    _requestState = value;
                    _requestState.Initialize(this);
                }
                else
                {
                    if (null != _requestState)
                    {
                        _requestState.Dispose();
                        _requestState = null;
                    }
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public WebResponse Response
        {
            get
            {
                return _response;
            }

            internal set
            {
                if (null == _response)
                {
                    _response = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Stream Content
        {
            get
            {
                return _sourceStream;
            }
        }
        #endregion

        #region Methods
        

        /// <summary>
        /// 
        /// </summary>
        public void WriteContent()
        {
            switch (RequestMethod)
            {
                case WebRequestMethod.DownloadFile:
                
                    if (null != _requestState && _sourceStream.CanWrite)
                    {
                        _sourceStream.Write(_requestState.ByteBuffer, 0, _requestState.BytesHandled);
                    }

                    break;

                case WebRequestMethod.UploadFile:

                    if (null != _requestState && _requestState.SourceStream.CanWrite)
                    {
                        _requestState.SourceStream.Write(_requestState.ByteBuffer, 0, _requestState.BytesHandled);
                    }

                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ReadContent()
        {
            switch(RequestMethod)
            {
                case WebRequestMethod.DownloadFile:

                    if (null != _requestState && _requestState.SourceStream.CanRead)
                    {
                        _requestState.BytesHandled         = _requestState.SourceStream.Read(_requestState.ByteBuffer, 0, _requestState.ByteBuffer.Length);
                        _requestState.TotalBytesHandled    += _requestState.BytesHandled;

                        return _requestState.BytesHandled;
                    }

                    break;

                case WebRequestMethod.UploadFile:

                    if (null != _sourceStream && _sourceStream.CanRead)
                    {
                        _requestState.BytesHandled         = _sourceStream.Read(_requestState.ByteBuffer, 0, _requestState.ByteBuffer.Length);
                        _requestState.TotalBytesHandled    += _requestState.BytesHandled;

                        return _requestState.BytesHandled;
                    }

                    break;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IAsyncResult BeginWriteContent(AsyncCallback callback)
        {
            switch (RequestMethod)
            {
                case WebRequestMethod.DownloadFile:

                    if (null != _requestState && _sourceStream.CanWrite)
                    {
                        return _sourceStream.BeginWrite(_requestState.ByteBuffer, 0, _requestState.BytesHandled, callback, this);
                    }

                    break;

                case WebRequestMethod.UploadFile:

                    if (null != _requestState && _requestState.SourceStream.CanWrite)
                    {
                        return _requestState.SourceStream.BeginWrite(_requestState.ByteBuffer, 0, _requestState.BytesHandled, callback, this);
                    }

                    break;
            }

            return new NullAsyncResult(); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        public void EndWriteContent(IAsyncResult asyncResult)
        {
            if (!(asyncResult is NullAsyncResult))
            {
                switch (RequestMethod)
                {
                    case WebRequestMethod.DownloadFile:

                        if (null != _sourceStream && _sourceStream.CanWrite)
                        {
                            _sourceStream.EndWrite(asyncResult);
                        }

                        break;

                    case WebRequestMethod.UploadFile:

                        if (null != _requestState && _requestState.SourceStream.CanWrite)
                        {
                            _requestState.SourceStream.EndWrite(asyncResult);
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IAsyncResult BeginReadContent(AsyncCallback callback)
        {
            switch (RequestMethod)
            {
                case WebRequestMethod.DownloadFile:

                    if (null != _requestState && _requestState.SourceStream.CanRead)
                    {
                        return _requestState.SourceStream.BeginRead(_requestState.ByteBuffer, 0, _requestState.ByteBuffer.Length, callback, this);
                    }

                    break;

                case WebRequestMethod.UploadFile:

                    if (null != _sourceStream && _sourceStream.CanRead)
                    {
                        return _sourceStream.BeginRead(_requestState.ByteBuffer, 0, _requestState.ByteBuffer.Length, callback, this);
                    }

                    break;
            }

            return new NullAsyncResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        public int EndReadContent(IAsyncResult asyncResult)
        {
            if (!(asyncResult is NullAsyncResult))
            {
                switch (RequestMethod)
                {
                    case WebRequestMethod.DownloadFile:

                        if (null != _requestState && _requestState.SourceStream.CanRead)
                        {
                            _requestState.BytesHandled         = _requestState.SourceStream.EndRead(asyncResult);
                            _requestState.TotalBytesHandled    += _requestState.BytesHandled;
                
                            return _requestState.BytesHandled;
                        }

                        break;

                    case WebRequestMethod.UploadFile:

                        if (null != _sourceStream && _sourceStream.CanRead)
                        {
                            _requestState.BytesHandled         = _sourceStream.EndRead(asyncResult);
                            _requestState.TotalBytesHandled    += _requestState.BytesHandled;

                            return _requestState.BytesHandled;
                        }

                        break;
                }
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public void HandleThrownException(Exception exception)
        {
            if (!(exception is WebRequestException))
            {
                _innerErrorRepository.Add(new WebRequestException(exception));
                return;
            }

            _innerErrorRepository.Add(exception);
        }

        /// <summary>
        /// 
        /// </summary>
        void IDisposable.Dispose()
        {
            if (null != _requestState)
            {
                _requestState.Dispose();
            }

            if (null != _sourceStream)
            {
                _sourceStream.Flush();
                _sourceStream.Close();
                _sourceStream.Dispose();
            }

            if (null != _response)
            {
                _response.Close();
                _response.Dispose();
            }

            if (null != _request)
            {
                _request.Abort();
            }
        }
        #endregion
    }
}
