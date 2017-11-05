using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class DecimalConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DecimalConverter()
        {
        }
        #endregion

        #region Fields
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Convert(object value)
        {
            var _decimal = decimal.MinValue;
            
            if (null != value)
            {
                if (decimal.TryParse(value.ToString(), out _decimal))
                {
                    return _decimal;
                }
            }

            return _decimal;
        }
        #endregion
    }
}