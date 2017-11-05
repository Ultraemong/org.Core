using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

using org.Core.Collections;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class WebRequestHandler
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public WebRequestHandler()
        {
        }
        #endregion

        #region Events
        public event WebRequestFaultedEventHandler                      Faulted                     = null;

        public event WebRequestCompletedEventHandler                    WritingCompleted            = null;
        public event WebRequestCompletedEventHandler                    ReadingCompleted            = null;
        public event WebRequestCompletedEventHandler                    GettingComplated            = null;

        public event WebRequestReadingProgressChangedEventHandler       ReadingProgressChanged      = null;
        public event WebRequestWritingProgressChangedEventHandler       WritingProgressChanged      = null;
        #endregion

        #region Fields
        int                                                             _bytesRead                  = 0;
        long                                                            _totalBytesRead             = 0L;

        byte[]                                                          _byteBuffer                 = null;

        Stream                                                          _requestStream              = null;
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract WebRequest CreateRequest(Uri uri, WebRequestMethod requestMethod);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="requestMethod"></param>
        /// <param name="isAsync"></param>
        /// <returns></returns>
        protected WebRequestContext CreateRequestContext(Uri uri, WebRequestMethod requestMethod, bool isAsync = false)
        {
            var _request    = CreateRequest(uri, requestMethod);
            var _context    = new WebRequestContext(_request, requestMethod, isAsync);

            return _context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="requestMethod"></param>
        /// <param name="bufferSize"></param>
        /// <param name="sourceStream"></param>
        /// <param name="contentLength"></param>
        /// <returns></returns>
        protected WebRequestContext CreateRequestContext(Uri uri, WebRequestMethod requestMethod, int bufferSize, FileStream sourceStream, bool isAsync = false)
        {
            var _request    = CreateRequest(uri, requestMethod);
            var _context    = new WebRequestContext(_request, requestMethod, bufferSize, sourceStream, isAsync);

            _byteBuffer     = new byte[bufferSize];

            return _context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="requestMethod"></param>
        /// <param name="bufferSize"></param>
        /// <param name="destinationStream"></param>
        /// <param name="contentLength"></param>
        /// <param name="isAsync"></param>
        /// <returns></returns>
        protected WebRequestContext CreateRequestContext(Uri uri, WebRequestMethod requestMethod, int bufferSize, FileStream destinationStream, long contentLength, bool isAsync = false)
        {
            var _request    = CreateRequest(uri, requestMethod);
            var _context    = new WebRequestContext(_request, requestMethod, bufferSize, destinationStream, contentLength, isAsync);

            return _context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected virtual void HandleGetRequestStream(WebRequestContext requestContext)
        {
            if (!requestContext.IsInError)
            {
                try
                {
                    if (!requestContext.IsAsync)
                    {
                        _requestStream = requestContext.Request.GetRequestStream();
                        return;
                    }

                    requestContext.Request.BeginGetRequestStream(new AsyncCallback(BeginGetRequestStreamCallback), requestContext);
                }
                catch (Exception ex)
                {
                    requestContext.HandleThrownException(ex);

                    RaiseFaultedEvent(requestContext);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void BeginGetRequestStreamCallback(IAsyncResult asyncResult)
        {
            var _requestContext = asyncResult.AsyncState as WebRequestContext;

            if (!_requestContext.IsInError)
            {
                try
                {
                    _requestStream = _requestContext.Request.EndGetRequestStream(asyncResult);

                    RaiseGettingComplatedEvent(_requestContext);
                }
                catch (Exception ex)
                {
                    _requestContext.HandleThrownException(ex);

                    RaiseFaultedEvent(_requestContext);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected virtual void HandleWriteContentToRequestStream(WebRequestContext requestContext)
        {
            if (!requestContext.IsInError && null != requestContext.RequestState)
            {
                try
                {
                    if (!requestContext.IsAsync)
                    {
                        _bytesRead          = 0;
                        _totalBytesRead     = 0L;

                        _byteBuffer         = new byte[requestContext.BufferSize];

                        while (0 < (_bytesRead = requestContext.Content.Read(_byteBuffer, 0, _byteBuffer.Length)))
                        {
                            _totalBytesRead += _bytesRead;

                            requestContext.RequestState.SourceStream.Write(_byteBuffer, 0, _bytesRead);
                        }

                        return;
                    }

                    requestContext.BeginReadContent(new AsyncCallback(BeginReadContentCallback));
                }
                catch (Exception ex)
                {
                    requestContext.HandleThrownException(ex);

                    RaiseFaultedEvent(requestContext);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected virtual void HandleRequest(WebRequestContext requestContext)
        {
            if (requestContext.IsInError)
            {
                try
                {
                    if (!requestContext.IsAsync)
                    {
                        requestContext.RequestState = new WebRequestState(requestContext.Request.GetRequestStream());
                        return;
                    }

                    requestContext.Request.BeginGetRequestStream(new AsyncCallback(BeginGetRequestStreamCallback), requestContext);
                }
                catch (Exception ex)
                {
                    requestContext.HandleThrownException(ex);

                    RaiseFaultedEvent(requestContext);
                }
            }
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected virtual void HandleRequestStream(WebRequestContext requestContext)
        {
            if (!requestContext.IsInError && null != requestContext.RequestState)
            {
                try
                {
                    if (!requestContext.IsAsync)
                    {
                        while (0 < requestContext.ReadContent())
                        {
                            requestContext.WriteContent();
                        }

                        return;
                    }

                    requestContext.BeginReadContent(new AsyncCallback(BeginReadContentCallback));
                }
                catch (Exception ex)
                {
                    requestContext.HandleThrownException(ex);

                    RaiseFaultedEvent(requestContext);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        protected virtual void HandleRequestResponse(WebRequestContext requestContext)
        {
            if (!requestContext.IsInError)
            {
                try
                {
                    if (!requestContext.IsAsync)
                    {
                        requestContext.Response = requestContext.Request.GetResponse();
                        return;
                    }

                    requestContext.Request.BeginGetResponse(new AsyncCallback(BeginGetResponseCallback), requestContext);
                }
                catch (Exception ex)
                {
                    requestContext.HandleThrownException(ex);

                    RaiseFaultedEvent(requestContext);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void BeginGetResponseCallback(IAsyncResult asyncResult)
        {
            var _context = asyncResult.AsyncState as WebRequestContext;

            if (!_context.IsInError)
            {
                try
                {
                    _context.Response = _context.Request.EndGetResponse(asyncResult);

                    RaiseGettingComplatedEvent(_context);
                }
                catch (Exception ex)
                {
                    _context.HandleThrownException(ex);

                    RaiseFaultedEvent(_context);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        void RaiseGettingComplatedEvent(WebRequestContext requestContext)
        {
            var _argument = new WebRequestEventArgs(requestContext);

            if (null != GettingComplated)
                GettingComplated(this, _argument);

            OnGettingCompleted(_argument);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnGettingCompleted(WebRequestEventArgs e) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        protected virtual void HandleRequestResponseStream(WebRequestContext requestContext)
        {
            if (!requestContext.IsInError && null != requestContext.Response)
            {
                try
                {
                    requestContext.RequestState = new WebRequestState(requestContext.Response.GetResponseStream());
                    
                    if (!requestContext.IsAsync)
                    {
                        while (0 < requestContext.ReadContent())
                        {
                            requestContext.WriteContent();
                        }
                        return;
                    }

                    requestContext.BeginReadContent(new AsyncCallback(BeginReadContentCallback));
                }
                catch (Exception ex)
                {
                    requestContext.HandleThrownException(ex);

                    RaiseFaultedEvent(requestContext);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void BeginReadContentCallback(IAsyncResult asyncResult)
        {
            var _context = asyncResult.AsyncState as WebRequestContext;

            if (!_context.IsInError)
            {
                try
                {
                    var _bytesRead = _context.EndReadContent(asyncResult);

                    if (0 < _bytesRead)
                    {
                        switch (_context.RequestMethod)
                        {
                            case WebRequestMethod.UploadFile:
                            case WebRequestMethod.DownloadFile:
                            
                                _context.BeginWriteContent(new AsyncCallback(BeginWriteContentCallback));

                                break;

                            default:

                                RaiseReadingProgressChangedEvent(_context);

                                _context.BeginReadContent(new AsyncCallback(BeginReadContentCallback));

                                break;
                        }
                    }
                    else
                    {
                        RaiseReadingCompletedEvent(_context);

                        (_context as IDisposable).Dispose();
                    }
                }
                catch (Exception ex)
                {
                    _context.HandleThrownException(ex);

                    RaiseFaultedEvent(_context);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        void RaiseReadingCompletedEvent(WebRequestContext requestContext)
        {
            var _argument = new WebRequestEventArgs(requestContext);

            if (null != ReadingCompleted)
                ReadingCompleted(this, _argument);

            OnReadingCompleted(_argument);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        void RaiseReadingProgressChangedEvent(WebRequestContext requestContext)
        {
            var _argument = new WebRequestReadingProgressChangedEventArgs(requestContext, requestContext.RequestState.TotalBytesHandled, requestContext.ContentLength);

            if (null != ReadingProgressChanged)
                ReadingProgressChanged(this, _argument);

            OnReadingProgressChanged(_argument);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnReadingProgressChanged(WebRequestReadingProgressChangedEventArgs e) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnReadingCompleted(WebRequestEventArgs e) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncResult"></param>
        void BeginWriteContentCallback(IAsyncResult asyncResult)
        {
            var _context = asyncResult.AsyncState as WebRequestContext;

            if (!_context.IsInError)
            {
                try
                {
                    _context.EndWriteContent(asyncResult);

                    RaiseWritingProgressChanged(_context);

                    _context.BeginReadContent(new AsyncCallback(BeginReadContentCallback));
                }
                catch (Exception ex)
                {
                    _context.HandleThrownException(ex);

                    RaiseFaultedEvent(_context);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        void RaiseWritingProgressChanged(WebRequestContext requestContext)
        {
            var _argument = new WebRequestWritingProgressChangedEventArgs(requestContext, requestContext.RequestState.TotalBytesHandled, requestContext.ContentLength);

            if (null != WritingProgressChanged)
                WritingProgressChanged(this, _argument);

            OnWritingProgressChanged(_argument);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFaulted(WebRequestEventArgs e) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnWritingProgressChanged(WebRequestWritingProgressChangedEventArgs e) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        void RaiseFaultedEvent(WebRequestContext requestContext)
        {
            var _argument = new WebRequestEventArgs(requestContext);

            if (null != Faulted)
                Faulted(this, _argument);

            OnFaulted(_argument);
        }
        #endregion
    }
}
