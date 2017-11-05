using org.Core.ServiceModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class ExternalActionContext : IDisposable
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isAsync"></param>
        public ExternalActionContext(WebRequest request, bool isAsync)
        {
            _request            = request;
            _method             = request.Method;
            _isAsync            = isAsync;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isAsync"></param>
        /// <param name="bufferSize"></param>
        public ExternalActionContext(WebRequest request, bool isAsync, int bufferSize)
            : this(request, isAsync)
        {
            _bufferSize         = bufferSize;
            _byteBuffer         = new byte[_bufferSize];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isAsync"></param>
        /// <param name="bufferSize"></param>
        /// <param name="readToEnd"></param>
        public ExternalActionContext(WebRequest request, bool isAsync, int bufferSize, bool readToEnd)
            : this(request, isAsync, bufferSize)
        {   
            _readToEnd          = readToEnd;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isAsync"></param>
        /// <param name="bufferSize"></param>
        /// <param name="readToEnd"></param>
        /// <param name="sourceStream"></param>
        public ExternalActionContext(WebRequest request, bool isAsync, int bufferSize, bool readToEnd, FileStream sourceStream)
            : this(request, isAsync, bufferSize, readToEnd)
        {
            _sourceStream       = sourceStream;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isAsync"></param>
        /// <param name="bufferSize"></param>
        /// <param name="readToEnd"></param>
        /// <param name="sourceStream"></param>
        /// <param name="userToken"></param>
        public ExternalActionContext(WebRequest request, bool isAsync, int bufferSize, bool readToEnd, FileStream sourceStream, object userToken)
            : this(request, isAsync, bufferSize, readToEnd, sourceStream)
        {
            _userToken          = userToken;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isAsync"></param>
        /// <param name="bufferSize"></param>
        /// <param name="destinationStream"></param>
        /// <param name="userToken"></param>
        public ExternalActionContext(WebRequest request, bool isAsync, int bufferSize, FileStream destinationStream, object userToken, long? contentLength)
            : this(request, isAsync, bufferSize)
        {
            _userToken          = userToken;
            _destinationStream  = destinationStream;
            _contentLength      = contentLength;
        }
        #endregion

        #region Fields
        WebRequest              _request            = null;
        Stream                  _requestStream      = null;

        WebResponse             _response           = null;
        Stream                  _responseStream     = null;

        FileStream              _sourceStream       = null;
        FileStream              _destinationStream  = null;
        
        int                     _bufferSize         = 1024;
        long                    _bytesRead          = 0;
        bool                    _readToEnd          = true;
        bool                    _isAsync            = false;
        long?                   _contentLength      = null;
        string                  _method             = null;
        
        byte[]                  _byteBuffer         = null;
        object                  _userToken          = null;

        IExternalResult         _result             = null;
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
        public WebResponse Response
        {
            get
            {
                return _response;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] BytesReceived
        {
            get
            {
                return _byteBuffer;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long BytesRead
        {
            get
            {
                return _bytesRead;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long ContentLength
        {
            get
            {
                return _contentLength.GetValueOrDefault(0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ReadToEnd
        {
            get
            {
                return _readToEnd;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FileStream SourceStream
        {
            get
            {
                return _sourceStream;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FileStream DestinationStream
        {
            get
            {
                return _destinationStream;
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

        /// <summary>
        /// 
        /// </summary>
        public IExternalResult Result
        {
            get
            {
                return _result;
            }

            set
            {
                _result = value;
            }
        }

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
        public string Method
        {
            get
            {
                return _method;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IAsyncResult BeginGetRequestStream(AsyncCallback callback)
        {
            return _request.BeginGetRequestStream(callback, this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        public void EndGetRequestStream(IAsyncResult asyncResult)
        {
            _requestStream = _request.EndGetRequestStream(asyncResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IAsyncResult BeginWriteRequestStream(int offset, int count, AsyncCallback callback)
        {
            if (null != _requestStream)
            {
                return _requestStream.BeginWrite(_byteBuffer, offset, count, callback, this);
            }

            throw new NullReferenceException("A stream of the request cannot be read because the stream is null.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        public void EndWriteRequestStream(IAsyncResult asyncResult)
        {
            if (null != _requestStream)
            {
                _requestStream.EndWrite(asyncResult);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="offset"></param>
        /// <param name="numBytes"></param>
        /// <param name="userCallback"></param>
        /// <returns></returns>
        public IAsyncResult BeginReadSourceStream(AsyncCallback callback)
        {
            if (null != _sourceStream)
            {
                return _sourceStream.BeginRead(_byteBuffer, 0, _bufferSize, callback, this);
            }

            throw new NullReferenceException("A stream of the source cannot be read because the stream is null.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        public int EndReadSourceStream(IAsyncResult asyncResult)
        {
            if (null != _sourceStream)
            {
                int _read = _sourceStream.EndRead(asyncResult);

                if (0 < _read)
                {
                    _bytesRead += _read;

                    return _read;
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IAsyncResult BeginGetResponse(AsyncCallback callback)
        {
            return _request.BeginGetResponse(callback, this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        public WebResponse EndGetResponse(IAsyncResult asyncResult)
        {
            _response = _request.EndGetResponse(asyncResult);

            if (!_contentLength.HasValue)
            {
                _contentLength = _response.ContentLength;

                if (null != _sourceStream)
                    _contentLength = _sourceStream.Length;
            }

            return _response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IAsyncResult BeginReadResponseStream(int offset, int count, AsyncCallback callback)
        {
            if (_readToEnd)
            {
                if (null != _response)
                {
                    if (null == _responseStream)
                        _responseStream = _response.GetResponseStream();

                    return _responseStream.BeginRead(_byteBuffer, offset, count, callback, this);
                }

                throw new NullReferenceException("A stream of the response cannot be read because the stream is null.");
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        public int EndReadResponseStream(IAsyncResult asyncResult)
        {
            if (_readToEnd)
            {
                if (null != _responseStream)
                {
                    int _retVal = _responseStream.EndRead(asyncResult);

                    if (0 < _retVal)
                    {
                        _bytesRead += _retVal;
                    }

                    return _retVal;
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IAsyncResult BeginWriteDestinationSteam(int offset, int count, AsyncCallback callback)
        {
            if (null != _destinationStream && 0 < count)
            { 
                return _destinationStream.BeginWrite(_byteBuffer, offset, count, callback, this);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        public void EndWriteDestinationStream(IAsyncResult asyncResult)
        {
            if (null != _destinationStream)
            {
                _destinationStream.EndWrite(asyncResult);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void FlushBuffer()
        {
            _bytesRead  = 0;
            _byteBuffer = new byte[_bufferSize];
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (null != _sourceStream)
            {
                _sourceStream.Flush();
                _sourceStream.Close();
                _sourceStream.Dispose();
                _sourceStream = null;
            }

            if (null != _destinationStream)
            {
                _destinationStream.Flush();
                _destinationStream.Close();
                _destinationStream.Dispose();
                _destinationStream = null;
            }

            if (null != _requestStream)
            {
                _requestStream.Flush();
                _requestStream.Close();
                _requestStream.Dispose();
                _requestStream = null;
            }

            if (null != _responseStream)
            {
                _responseStream.Flush();
                _responseStream.Close();
                _responseStream.Dispose();
                _responseStream = null;
            }

            if (null != _response)
            {
                _response.Close();
                _response = null;
            }

            if (null != _request)
            {
                _request.Abort();
                _request = null;
            }
        }
        #endregion
    }
}
