using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.Core.Cryptography
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class EncryptableAttribute : Attribute, IEncryptable
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public EncryptableAttribute()
        {   
        }
        #endregion

        #region Fields
        Type _cryptoTransformProvider = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string SaltKey
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Type CryptoTransformProvider
        {
            get
            {
                if (null == _cryptoTransformProvider)
                    _cryptoTransformProvider = typeof(RijndaelCryptoTransform);

                return _cryptoTransformProvider;
            }

            set
            {
                _cryptoTransformProvider = value;
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
