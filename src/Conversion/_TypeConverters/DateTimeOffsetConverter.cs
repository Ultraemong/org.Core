using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class DateTimeOffsetConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffsetConverter()
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
            var _dateTimeOffset = DateTimeOffset.MinValue;

            if (null != value)
            {
                if (DateTimeOffset.TryParse(value.ToString(), out _dateTimeOffset))
                {
                    return _dateTimeOffset;
                }
            }

            return _dateTimeOffset;
        }
        #endregion
    }
}