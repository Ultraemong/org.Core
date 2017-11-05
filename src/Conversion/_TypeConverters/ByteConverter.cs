using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class ByteConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public ByteConverter()
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
            var _byte = byte.MinValue;

            if (null != value)
            {
                if (byte.TryParse(value.ToString(), out _byte))
                {
                    return _byte;
                }
            }

            return _byte;
        }
        #endregion
    }
}