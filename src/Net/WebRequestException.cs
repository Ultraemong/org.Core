using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Collections;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class WebRequestException : Exception, IExternalResult
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorText"></param>
        public WebRequestException(string errorText)
            : base(errorText)
        {   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorText"></param>
        /// <param name="errorCode"></param>
        public WebRequestException(string errorText, int errorCode)
            : base(errorText)
        {
            _errorCode = errorCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerException"></param>
        public WebRequestException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorText"></param>
        /// <param name="innerException"></param>
        public WebRequestException(string errorText, Exception innerException)
            : base(errorText, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="innerException"></param>
        public WebRequestException(int errorCode, Exception innerException)
            : base(innerException.Message, innerException)
        {
            _errorCode = errorCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <param name="innerException"></param>
        public WebRequestException(string errorText, int errorCode, Exception innerException)
            : base(errorText, innerException)
        {
            _errorCode = errorCode;
        }
        #endregion

        #region Fields
        int _errorCode = -1;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int ErrorCode
        {
            get
            {
                return _errorCode;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorText
        {
            get
            {
                return base.Message;
            }
        }
        #endregion
    }
}
