using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class DoubleConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DoubleConverter()
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
            var _double = double.MinValue;

            if (null != value)
            {
                if (double.TryParse(value.ToString(), out _double))
                {
                    return _double;
                }
            }

            return _double;
        }
        #endregion
    }
}