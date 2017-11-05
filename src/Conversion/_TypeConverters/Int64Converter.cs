using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class Int64Converter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Int64Converter()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Convert(object value)
        {
            var _int64 = long.MinValue;

            if (null != value)
            {
                if (long.TryParse(value.ToString(), out _int64))
                {
                    return _int64;
                }
            }

            return _int64;
        }
        #endregion
    }
}