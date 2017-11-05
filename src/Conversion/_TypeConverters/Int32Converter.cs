using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class Int32Converter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Int32Converter()
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
            var _int32 = int.MinValue;

            if (null != value)
            {
                if (int.TryParse(value.ToString(), out _int32))
                {
                    return _int32;
                }
            }

            return _int32;
        }
        #endregion
    }
}