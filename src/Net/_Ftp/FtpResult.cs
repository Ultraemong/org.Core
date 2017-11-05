using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Net.Ftp
{
    /// <summary>
    /// 
    /// </summary>
    public class FtpResult : IExternalResult
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public FtpResult()
        {
        }

        // <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        public FtpResult(FtpStatusCode errorCode, string errorText)
        {
            _errorCode  = errorCode;
            _errorText  = errorText;
        }
        #endregion

        #region Fields
        readonly FtpStatusCode  _errorCode  = FtpStatusCode.Undefined;
        readonly string         _errorText  = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public FtpStatusCode ErrorCode
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
                return _errorText;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        int IExternalResult.ErrorCode
        {
            get 
            {
                return (int)ErrorCode;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="excepion"></param>
        /// <returns></returns>
        public static FtpResult Parse(Exception excepion)
        {
            if (excepion is WebException)
            {
                return Parse(((WebException)excepion).Response as FtpWebResponse);
            }

            return new FtpResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static FtpResult Parse(FtpWebResponse response)
        {
            if (null != response)
            {
                return new FtpResult(response.StatusCode, response.StatusDescription);
            }

            return new FtpResult();
        }
        #endregion
    }
}
