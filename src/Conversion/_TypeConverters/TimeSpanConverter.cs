using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class TimeSpanConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public TimeSpanConverter()
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
            var _timeSpan = TimeSpan.MinValue;

            if (null != value)
            {
                if (TimeSpan.TryParse(value.ToString(), out _timeSpan))
                {
                    return _timeSpan;
                }
            }

            return _timeSpan;
        }
        #endregion
    }
}