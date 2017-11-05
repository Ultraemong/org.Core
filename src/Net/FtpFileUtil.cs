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
    public abstract class FtpFileUtil : INetworkFileServiceProvider
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileManager"></param>
        public FtpFileUtil(INetworkFileService fileManager)
            : base()
        {
            _fileService = fileManager;
        }
        #endregion

        #region Fields
        readonly INetworkFileService _fileService = null;
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
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public IExternalResult CreateDirectory(string directoryPath)
        {
            var _requestMethod  = WebRequestMethod.MakeDirectory;
            var _directoryName  = WebRequestHelpers.ReplacePathDelimiterToUriDelimiter(directoryPath);

            try
            {
                if (-1 == _directoryName.IndexOf("/"))
                {
                    var _uri            = CreateUniformResourceIdentifier(_directoryName);
                    var _request        = CreateRequest(_uri, _requestMethod);
                    var _requestResult  = GetRequestResult(_request.GetResponse(), _requestMethod);

                    _request.Abort();

                    return _requestResult;
                }
                else
                {
                    var _split          = _directoryName.Split('/');
                    var _current        = (string)null;
                
                    for (var i = 0; i < _split.Length; i++)
                    {
                        _current = (string.IsNullOrEmpty(_current)) 
                            ? _split[i] : string.Format("{0}/{1}", _current, _split[i]);

                        if (!ExistDirectory(_current))
                        {
                            var _uri            = CreateUniformResourceIdentifier(_current);
                            var _request        = CreateRequest(_uri, _requestMethod);
                            var _requestResult  = GetRequestResult(_request.GetResponse(), _requestMethod);

                            _request.Abort();

                            if (!_requestResult.ErrorCode.Equals(0))
                                throw new InvalidOperationException(_requestResult.ErrorText);
                        }
                    }

                    return new OperationResult();
                }
            }
            catch (Exception ex)
            {
                return new OperationResult(ex.Message, -1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public bool ExistDirectory(string directoryPath)
        {
            var _requestMethod  = WebRequestMethod.ExistDirectory;
            var _directoryName  = WebRequestHelpers.ReplacePathDelimiterToUriDelimiter(directoryPath);

            var _uri            = CreateUniformResourceIdentifier(_directoryName);
            var _request        = CreateRequest(_uri, _requestMethod);

            try
            {
                return GetRequestResult(_request.GetResponse(), _requestMethod).ErrorCode.Equals(0);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _request.Abort();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ExistFile(string filePath)
        {
            var _requestMethod  = WebRequestMethod.ExistDirectory;
            var _resourceName   = WebRequestHelpers.ReplacePathDelimiterToUriDelimiter(filePath);

            var _uri            = CreateUniformResourceIdentifier(_resourceName);
            var _request        = CreateRequest(_uri, _requestMethod);

            try
            {
                return GetRequestResult(_request.GetResponse(), _requestMethod).ErrorCode.Equals(0);
            }
            catch(Exception)
            {
                return false;
            }
            finally
            {
                _request.Abort();
            }
        }

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
        /// <param name="response"></param>
        /// <param name="requestMethod"></param>
        /// <returns></returns>
        protected virtual IExternalResult GetRequestResult(WebResponse response, WebRequestMethod requestMethod)
        {
            var _response = response as FtpWebResponse;
            
            if (null != _response)
            {
                switch (requestMethod)
                {
                    case WebRequestMethod.MakeDirectory:

                        if (_response.StatusCode.Equals(FtpStatusCode.PathnameCreated))
                            return new OperationResult(_response.StatusDescription, 0);

                        break;

                    case WebRequestMethod.ExistDirectory:

                        if (_response.StatusCode.Equals(FtpStatusCode.DataAlreadyOpen))
                            return new OperationResult(_response.StatusDescription, 0);

                        break;

                    case WebRequestMethod.ExistFile:

                        if (_response.StatusCode.Equals(FtpStatusCode.FileStatus))
                            return new OperationResult(_response.StatusDescription, 0);

                        break;

                    case WebRequestMethod.UploadFile:
                    case WebRequestMethod.DownloadFile:

                        if (_response.StatusCode.Equals(FtpStatusCode.ClosingData))
                            return new OperationResult(_response.StatusDescription, 0);

                        break;

                    default:
                        throw new NotSupportedException(string.Format("The '{0}' method is not supported.", requestMethod));

                }
            }

            throw new NullReferenceException("The response cannot be null.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        protected virtual WebRequest CreateRequest(Uri uri, WebRequestMethod requestMethod)
        {
            var _request            = FtpWebRequest.Create(uri) as FtpWebRequest;

            _request.KeepAlive      = true;
            _request.UsePassive     = false;
            _request.Method         = WebRequestHelpers.WebRequestMethodToFtpMethodString(requestMethod);
            _request.Credentials    = FileService.Credential;

            return _request;
        }
        #endregion
    }
}
