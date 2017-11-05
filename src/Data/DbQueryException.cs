using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Collections;
using org.Core.ServiceModel;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class DbQueryException : Exception, IExternalResult
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorText"></param>
        public DbQueryException(string errorText)
            : base(errorText)
        {   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorText"></param>
        /// <param name="errorCode"></param>
        public DbQueryException(string errorText, int errorCode)
            : base(errorText)
        {
            _errorCode = errorCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerException"></param>
        public DbQueryException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorText"></param>
        /// <param name="innerException"></param>
        public DbQueryException(string errorText, Exception innerException)
            : base(errorText, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="innerException"></param>
        public DbQueryException(int errorCode, Exception innerException)
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
        public DbQueryException(string errorText, int errorCode, Exception innerException)
            : base(errorText, innerException)
        {
            _errorCode = errorCode;
        }
        #endregion

        #region Fields
        readonly int _errorCode = -1;
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
