using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class DateTimeConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DateTimeConverter()
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
            var _dateTime = DateTime.MinValue;

            if (null != value)
            {
                if (DateTime.TryParse(value.ToString(), out _dateTime))
                {
                    return _dateTime;
                }
            }

            return _dateTime;
        }
        #endregion
    }
}