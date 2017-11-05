using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class Int16Converter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Int16Converter()
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
            var _int16 = short.MinValue;

            if (null != value)
            {
                if (short.TryParse(value.ToString(), out _int16))
                {
                    return _int16;
                }
            }

            return _int16;
        }
        #endregion
    }
}