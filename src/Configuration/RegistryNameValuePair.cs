using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

namespace org.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RegistryNameValuePair
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public RegistryNameValuePair(string name, object value)
        {
            _name       = name;
            _value      = value;
            _isDeleted  = (null == value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="valueType"></param>
        public RegistryNameValuePair(string name, object value, RegistryValueKind valueType)
            : this (name, value)
        {
            _valueType = valueType; 
        }
        #endregion

        #region Fields
        readonly string             _name       = null;
        readonly object             _value      = null;
        readonly RegistryValueKind  _valueType  = RegistryValueKind.String;

        bool                        _isDeleted  = false;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object Value
        {
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public RegistryValueKind ValueType
        {
            get
            {
                return _valueType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        internal void SetDeleted()
        {
            _isDeleted = true;
        }
        #endregion
    }
}
