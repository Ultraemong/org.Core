using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class BooleanConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public BooleanConverter()
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
            var _boolean = false;

            if (null != value)
            {
                if (bool.TryParse(value.ToString(), out _boolean))
                {
                    return _boolean;
                }
            }

            return _boolean;
        }
        #endregion
    }
}