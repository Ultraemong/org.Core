using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace org.Core
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DataContract]
    public class OperationResult : IExternalResult
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public OperationResult()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorText"></param>
        /// <param name="errorCode"></param>
        public OperationResult(string errorText, int errorCode)
            : this()
        {
            _errorText = errorText;
            _errorCode = errorCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public OperationResult(IExternalResult result)
            : this(result.ErrorText, result.ErrorCode)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultCollection"></param>
        public OperationResult(IExternalResultCollection resultCollection)
            : this("A few exceptions were occurred.", int.MinValue)
        {
            if (!resultCollection.IsEmpty)
            {
                if (resultCollection.Count.Equals(1))
                    _errorCode = resultCollection[0].ErrorCode;
                
                _errorText = resultCollection.ToString();
            }
        }
        #endregion

        #region Fields
        [DataMember]
        readonly int        _errorCode  = 0;

        [DataMember]
        readonly string     _errorText  = null;
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
                return _errorText;
            }
        }
        #endregion

        #region Methods
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContract]
    public class OperationResult<T> : IExternalResult<T>
        where T : class
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public OperationResult(T result)
        {
            _result = result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorText"></param>
        /// <param name="errorCode"></param>
        /// <param name="result"></param>
        public OperationResult(string errorText, int errorCode, T result)
        {
            _result     = result;
            _errorText  = errorText;
            _errorCode  = errorCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="resultCollection"></param>
        public OperationResult(T result, IExternalResultCollection resultCollection)
            : this("A few exceptions were occurred.", int.MinValue, result)
        {
            if (!resultCollection.IsEmpty)
            {
                if (resultCollection.Count.Equals(1))
                    _errorCode = resultCollection[0].ErrorCode;

                _errorText = resultCollection.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public OperationResult(IExternalResult<T> result)
            : this(result.ErrorText, result.ErrorCode, result.Result)
        {
        }
        #endregion

        #region Fields
        [DataMember]
        readonly T          _result     = default(T);
        [DataMember]
        readonly int        _errorCode  = 0;
        [DataMember]
        readonly string     _errorText  = null;
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
                return _errorText;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public T Result
        {
            get
            {
                return _result;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static implicit operator OperationResult(OperationResult<T> result)
        {
            return new OperationResult(result);
        }
        #endregion
    }
}
